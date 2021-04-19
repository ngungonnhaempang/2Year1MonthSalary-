using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LIMS.Contract;
using LIMS.Model;
using MIS.Utility;
using System.Data;
using Shawoo.Core;
using Shawoo.Common;
using System.ServiceModel;
using System.Data.Common;
using System.IO;

namespace LIMS.Service
{
    public class DOCRequisitionService : MarshalByRefObject, IDOCRequisition
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //FEPV LIMS
        protected static NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");
        DB db = new DB("LIMS");
        bool ChangeReqDocState(string voucherID, string state, string gradesVersionID)
        {
            string str = voucherID.Substring(0, 3);
            string sqlCommand = "";

            if (str == "RMX")
            {
                Console.WriteLine(str);
                sqlCommand = "UPDATE Requisition SET STATE = @STATE ,GradeVersion=@GradeVersion WHERE VoucherID = @VoucherID AND STATE='0'";
            }
            if (str == "MXQ")
            {
                Console.WriteLine(str);
                sqlCommand = @"UPDATE r SET [State] = @STATE,
                        GradeVersion= (SELECT id FROM GetMaxCurrentGradeVersion(r.SampleName,r.LOT_NO))
                        FROM Requisition r
                        WHERE [STATE]='0' AND VoucherID IN (SELECT VoucherID FROM ReqInDraft WHERE DraftID=@DraftID)";
                
            }


            DbTransaction tran = gate.BeginTransaction();
            try
            {
                Console.WriteLine("state:" + state + " voucherID:" + voucherID);
                gate.ExecuteNonQuery(sqlCommand, tran, new object[] { state, str == "RMX"? gradesVersionID: voucherID, voucherID });
                tran.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error(ex);
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        private string CheckGradeVersion(string sampleName, string lotNo)
        {
       
            //是否外销的是否上传柜号 
            DataTable dt = gate.ExecuteStoredProcedure("i_CheckGrade_Version",
                                               new string[] { "UserID", "SampleName", "LOT_NO" },
                                               new object[] { "", sampleName, lotNo }).Tables[0];


            if (dt.Rows.Count > 0)
            {
               return dt.Rows[0]["VersionID"].ToString();
            }
           throw new Exception("VersionID is Null");

        }

        private bool Accept(DataTable dt, string voucherID)
        {

            string sampleName = dt.Rows[0]["SampleName"].ToString();
            string lotNO = dt.Rows[0]["LOT_NO"].ToString();
            var GradesVersionID = CheckGradeVersion(sampleName, lotNO);

            log.Info("Accept:" + voucherID);

            Console.WriteLine("DocRequisitionService--Accept " + DateTime.Now.ToString());
            Console.WriteLine(voucherID);
            //GradeVersion
            //等级规格版本 需要的设定
            return ChangeReqDocState(voucherID, "1", GradesVersionID);
        }
        public bool Accept(string voucherID, out string msg)
        {
            msg = "";
            try
            {
                Console.WriteLine("DocRequisitionService--Accept " + DateTime.Now.ToString());
                Console.WriteLine(voucherID);
                DataTable dt=gate.SelectDataSet(@"SELECT * FROM Requisition where VoucherID=@VoucherID", new object[] {  voucherID }).Tables[0];
                if (dt.Rows.Count <= 0)
                {
                    Console.WriteLine("DraftID");
                    dt = gate.SelectDataSet(@"SELECT TOP 1 SampleName,r.LOT_NO  FROM Requisition r INNER JOIN ReqInDraft rd ON r.VoucherID=rd.VoucherID WHERE rd.DraftID=@VoucherID", new object[] { voucherID }).Tables[0];
                   
                }
                if (dt.Rows.Count > 0)
                {
                    return Accept(dt, voucherID);
                }
              
            }
            catch (Exception e)
            {
                msg = e.Message;
                log.Error(e);
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public bool Reject(string voucherID, out string msg)
        {
            log.Info("Reject:" + voucherID);
            
            Console.WriteLine("DOCRequisitionDAL--Reject " + DateTime.Now.ToString());
            msg = "";
            try
            {
                msg = "";
             //  判断是否能删除
                Console.WriteLine("DocRequisitionService--Reject " + DateTime.Now.ToString());
               Console.WriteLine(voucherID);
              return ChangeReqDocState(voucherID, "X","");
            }
            catch (Exception e)
            {
                msg = e.Message;
                log.Error(e);
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
