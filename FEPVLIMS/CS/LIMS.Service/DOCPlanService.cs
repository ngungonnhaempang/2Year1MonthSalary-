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

namespace LIMS.Service
{
    public class DOCPlanService : MarshalByRefObject, IDOCPlan
    {
        //FEPV LIMS
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        private DB db = new DB("LIMS");
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string GetAB(string sampleName)
        {
            DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_SamplesAB",
               new string[] { "AB", "SampleName" },
               new object[] { "", sampleName }).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["AB"].ToString();
            }

            return string.Empty;
        }

        public string CreateVoucherID(string sampleName, DateTime prodDate)
        {
            Console.WriteLine("DOCPlanDAL-CreateVoucherID():" + DateTime.Now.ToString());
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("C_CreateDocVoucherID",
                    new string[] { "UserID", "SampleName", "ProdDate", "DOCType" },
                    new object[] { Shawoo.GenuineChannels.GenuineUtility.CurrentSession["UID"].ToString(), sampleName, prodDate, "P" }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }

        public bool InsertPlan(DOCPlan doc, ref string VoucherID, out string msg)
        {
            Console.WriteLine("DOCPlanDAL-InsertPlan():" + DateTime.Now.ToString());
            VoucherID = string.Empty;
            msg = string.Empty;
            try
            {
                //1. VoucherID By SampleName and ProdDate
                VoucherID = CreateVoucherID(doc.SampleName, doc.ProdDate);
                //3.Create VoucherID
                doc.VoucherID = VoucherID;
                doc.State = "1";
                //得到等级的版本  成品一定要有等级规格
                string versionID = string.Empty;
                msg = CheckGradeVersion(doc.SampleName, doc.LOT_NO, out versionID);
                if (!string.IsNullOrEmpty(msg))
                {
                    return false;
                }
                Console.WriteLine(versionID);
                doc.GradeVersion = versionID ;
                doc.UserID = Shawoo.GenuineChannels.GenuineUtility.CurrentSession["UID"].ToString();
                return db.Insert(doc);

            }
            catch (Exception e)
            {
                log.Error(e);
              
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string CheckGradeVersion(string sampleName,string lotNo,out string gradeId)
        {
             gradeId = string.Empty;
            //是否外销的是否上传柜号 
            DataTable dt=acQC.DbHelper.ExecuteStoredProcedure("i_CheckGrade_Version",
                                               new string[] { "UserID","SampleName", "LOT_NO" },
                                               new object[] {"", sampleName, lotNo }).Tables[0];

           
            gradeId = dt.Rows[0]["VersionID"].ToString();
            return dt.Rows[0]["MSG"].ToString();
        
        }
        public bool DeletePlan(string voucherID)
        {
            Console.WriteLine("DOCPlanDAL-DeletePlan():" + DateTime.Now.ToString());
            return false;
            //try
            //{
            //    docConfCmd = @"UPDATE Plans SET State = 'X' WHERE State = '1' AND VoucherID = @VoucherID";

            //    return db.Delete(db.Select<DOCPlan>(new DOCPlan { VoucherID = voucherID }));
            //}
            //catch (Exception e)
            //{
            //    log.Error(e);
            //    Logger.Warnning(e);
            //    throw new Exception(e.Message);
            //}
        }

        /// <summary>
        /// delete this voucher ,it will show spc chart
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="HasChart"></param>
        /// <returns></returns>
        public bool DeletePlanWithNoChart(string voucherID)
        {
            Console.WriteLine("DocPlanService-DeletePlanWithNoChart():" + DateTime.Now.ToString());
            string cmd = @"UPDATE Plans SET HasChart = '0'
                           WHERE HasChart = '1' AND VoucherID = @VoucherID";

            try
            {
                acQC.ExecuteNonQuery(cmd, new object[] { voucherID });
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}