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
    public class ProfileDAL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");
        private DB db = new DB("LIMS");

        public bool CreateProfile(Profile profile)
        {
            Console.WriteLine("ProfileDAL - CreateProfile():" + DateTime.Now.ToString());
            try
            {
                return db.Insert(profile);
            }
            catch (Exception e)
            {
                log.Error(e);
             
                throw new Exception(e.Message);
            }
        }

        public bool UpdateProfile(Profile profile)
        {
            Console.WriteLine("ProfileDAL - UpdateProfile():" + DateTime.Now.ToString());
            try
            {
                return db.Insert(profile);
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }

        public bool DeleteProfile(Profile profile)
        {
            Console.WriteLine("ProfileDAL - DeleteProfile():" + DateTime.Now.ToString());
            try
            {
                return db.Insert(profile);
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }

        public DataTable GetProfileList(string voucherID)
        {
            Console.WriteLine("ProfileDAL - GetProfileList():" + DateTime.Now.ToString());
            try
            {
                return acQC.DbHelper.Select("SELECT [VoucherID] ,[SampleName], [PropertyName] from Profile WHERE VoucherID = @VoucherID",
                                                new object[] { voucherID }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }

        public string GetGradesVersion(string SampleName, string LOT_NO)
        {
         
            DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("i_CheckGrade_Version",
                                            new string[] { "UserID", "SampleName", "LOT_NO" },
                                            new object[] { "", SampleName, LOT_NO }).Tables[0];


            return dt.Rows[0]["VersionID"].ToString();
            
        }
        public string RecodeResult(Profile proflie)
        {
            DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("QE31_In_Grade_UpdateProfile",
                                            new string[] { "UserID", "VoucherID", "SampleName", "PropertyName", "Prec", "Result", "OriginalResult", "OverRange" },
                                            new object[] { "", proflie.VoucherID, proflie.SampleName,proflie.PropertyName,proflie.Prec,proflie.Result,proflie.OriginalResult,proflie.OverRange }).Tables[0];
            return dt.Rows[0]["Msg"].ToString();
        }
    }
}