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
using Newtonsoft.Json;

namespace LIMS.Service
{
    public class DOCPlanAddService : MarshalByRefObject, IDOCPlanAdd
    {
        //FEPV LIMS
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        private DB db = new DB("LIMS");
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool InsertPlanByAdd(DOCPlan doc, int addID, out string voucherID)
        {
            Console.WriteLine("PlanAddService--InsertPlanByAdd " + DateTime.Now.ToString());
            bool result = false;

            string labAddr = "";
            string LabAddCommand = @"SELECT LabID FROM Samples where SampleName = @SampleName";

            try
            {
                labAddr = acQC.SelectScalar<string>(LabAddCommand, new object[] { doc.SampleName });
                voucherID = "P" + labAddr + DateTime.Now.ToString("yyMMdd");
            }
            catch
            {
                voucherID = "";
                return false;
            }
            //
            string planCount = "SELECT ISNULL(Max(substring(VoucherID,9,4))+1,1) FROM PLANS WHERE SUBSTRING(VoucherID,1,8) = @PRE";

            try
            {
                voucherID += (acQC.SelectScalar<int>(planCount, new object[] { voucherID })).ToString().PadLeft(4, '0');
           

            Console.WriteLine("VoucherID:" + voucherID);
           // doc.createType = 1;
         
            doc.VoucherID = voucherID;
            doc.PlanAddID = addID;
            doc.HasChart = "1";
                Console.WriteLine(JsonConvert.SerializeObject(doc));
                if (db.Insert(doc))
                {
                    acQC.DbHelper.ExecuteNonQuery("UPDATE PlanAdd SET State = '2' WHERE VoucherID = @VoucherID", new object[] { addID });
                    return true;

                }
               
                 return false;
               
            }
            catch (Exception ex)
            {
                log.Error(ex);
            
                throw new Exception(ex.Message);
            }
            
        }
        public bool DeletePlandAdd(string voucherID)
        {
            Console.WriteLine("DOCPlanAddDAL--DeletePlandAdd " + DateTime.Now);

            //核查是否能删除
            string cmd = @"Update PlanAdd SET state = 'X' WHERE VoucherID = @VoucherID and state='0'";
            try
            {
                acQC.ExecuteNonQuery(cmd, new object[] { voucherID });
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return true;


        }

        public bool RejectPlandAdd(string voucherID)
        {
            Console.WriteLine("DOCPlanAddDAL--RejectPlandAdd " + DateTime.Now);
            try
            {
                //核查是否能删除
                string cmd = @"Update PlanAdd SET state = '0' WHERE VoucherID = @VoucherID";
                try
                {
                    acQC.ExecuteNonQuery(cmd, new object[] { voucherID });
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    Console.WriteLine(ex.Message);
                    return false;
                }

                return true;
            
               
            }
            catch (Exception e)
            {
         
                throw e;
            }
        }

        /// <summary>
        ///  add Plan project  Create
        /// </summary>
        /// <param name="DOC"></param>
        /// <returns></returns>
        public bool InsertPlanAdd(DOCPlanAdd DOC)
        {
            Console.WriteLine("PlanAddService-InsertPlanAdd():" + DateTime.Now);
            //Get Currrent User
          
            try
            {
                DataTable dt = acQC.ExecuteStoredProcedure("i_CheckGrade_Version",
                                                new string[] { "UserID", "SampleName", "LOT_NO" },
                                                new object[] { "", DOC.SampleName, DOC.LOT_NO }).Tables[0];
                string msg = string.Empty;
                msg = dt.Rows[0]["MSG"].ToString();
                if (!string.IsNullOrEmpty(msg))
                {
                    throw new Exception(msg);
                   
                }
                DOC.GradeVersion =  new Guid( dt.Rows[0]["VersionID"].ToString()) ;
                DOC.ProdDate = System.DateTime.Now;
                DOC.State = "0";
                DOC.UserID = DB.User;
                log.Info(JsonConvert.SerializeObject(DOC));
                return db.Insert(DOC);
            }
            catch (Exception e)
            {
                log.Error(e);
              
                throw e;
            }
        }
    }
}