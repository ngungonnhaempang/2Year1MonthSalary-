using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;
using Shawoo.Core;
using Shawoo.Common;

namespace LIMS.DAL
{
    public class RequisitionDAL
    {
        public RequisitionDAL()
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //FEPV LIMS
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        private DB db = new DB("LIMS");
        
        public bool CreateVoucher(DOCRequisition requisition)
        {
            Console.WriteLine("RequisitionDAL - CreateVoucherID():" + DateTime.Now.ToString());
            try
            {
                return db.Insert(requisition);
                
            }
            catch (Exception e)
            {
                log.Error(e);
                
                throw new Exception(e.Message);
            }
        }
        
        public string CreateVoucherID(string userID, string sampleName)
        {
            DateTime date = DateTime.Now;

            return acQC.DbHelper.ExecuteStoredProcedure("C_CreateDocVoucherID",
                new string[] { "UserID", "SampleName", "ProdDate", "DOCType" },
                new object[] { userID, sampleName, DateTime.Now, "R" }
                ).Tables[0].Rows[0][0].ToString();
        }
        /// <summary>
        /// Get Entrusted detial
        /// </summary>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        public DataTable GetDelegateDetailsByVoucherID(string voucherId)
        {
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("Q_GetDelegateDetailsByVoucherID",
                  new string[] { "VoucherID" },
                  new object[] { voucherId }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// create plan ,other add test project
        /// </summary>
        /// <param name="planAdd"></param>
        /// <returns></returns>
        public bool CreatePlanAdd(DOCPlanAdd planAdd)
        {
            try
            {
                return db.Insert(planAdd);
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }
        public DataTable GetReqVoucher(string userid, DateTime sendB, DateTime sendE, string owner, string voucherState)
        {
            DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_GetReq4Delegate",
                  new string[] { "UserID", "SendB", "SendE", "Owner", "VoucherState" },
                  new object[] { userid, sendB, sendE, owner, voucherState }).Tables[0];
            return dt;
        }

        public DOCRequisition GetVoucher(string userID, string voucherID)
        {
            Console.WriteLine("RequisitionDAL - GetVoucherData():" + DateTime.Now.ToString());

            try
            {
                return db.Select<DOCRequisition>(new DOCRequisition { VoucherID = voucherID });
            }
            catch (Exception e)
            {

                log.Error(e);
              
                throw new Exception(e.Message);
            }
        }

        public bool DeleteVoucher(string voucherID)
        {
            Console.WriteLine("RequisitionDAL - DeleteVoucher():" + DateTime.Now.ToString());
            try
            {
                return db.Delete(db.Select<DOCRequisition>(new DOCRequisition { VoucherID = voucherID }));
            }
            catch (Exception e)
            {
                log.Error(e);
             
                throw new Exception(e.Message);
            }
        }

        public DOCRequisition Select(string voucherId)
        {
            return db.Select<DOCRequisition>(new DOCRequisition { VoucherID = voucherId });
        }

        public bool Update(DOCRequisition req)
        {
            Console.WriteLine("AttributeService-UpdateAttribute():" + DateTime.Now.ToString());
            try
            {
                return db.Update(req);
            }
            catch (Exception e)
            {
              
              
                throw new Exception(e.Message);
            }
        }
    }
}