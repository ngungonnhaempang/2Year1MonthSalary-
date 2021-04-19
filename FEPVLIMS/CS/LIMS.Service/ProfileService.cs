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
    public class ProfileService : MarshalByRefObject, IProfile
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected static NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");
        private DB db = new DB("LIMS");

        public ProfileService()
        {
        }
        /// <summary>
        /// 没有使用在UI上控制了
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="sampleName"></param>
        /// <param name="lot_no"></param>
        /// <param name="perpertyName"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool ProfileInRange(string voucherID, string sampleName, string lot_no, string perpertyName, decimal result)
        {
            Console.WriteLine("ProfileService-ProfileInRange():" + DateTime.Now.ToString());
            try
            {
                int count = 4;
                object[] outParameters;
                gate.DbHelper.ExecuteStoredProcedureScalar("QC04_In_Grade_Range",
                    new string[] { "SampleName", "LOT_NO", "PropertyName", "Result", "VoucherID" },
                    new object[] { sampleName, lot_no, perpertyName, result, voucherID },
                    new string[] { "InRange" }, new DbType[] { DbType.Int32 }, out outParameters);
                count = (int)outParameters.ElementAt(0);

                Console.WriteLine("outParameters.ElementAt(0):" + count.ToString());
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
      
        /// <summary>
        /// 结果录入
        /// </summary>
        /// <param name="_ProfileList"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool Modify(List<Profile> _ProfileList, out string msg)
        {
            Console.WriteLine("ProfileDAL-Modify():" + DateTime.Now.ToString() + _ProfileList.Count());
            msg = "";
            //acQC.DbHelper.ExecuteNonQuery("DELETE Profile WHERE VoucherID = @VoucherID AND LEFT(VoucherID,1) <> 'R'", new object[] { _ProfileList[0].VoucherID });
            foreach (Profile _Profile in _ProfileList)
            {
                Console.WriteLine(_Profile.LOT_NO + "+" + _Profile.Result);
                try
                {
                    msg += gate.DbHelper.ExecuteStoredProcedure("QE31_In_Grade_UpdateProfile", new string[] { "VoucherID", "SampleName", "PropertyName", "Prec", "Result", "OriginalResult", "OverRange" },
                                                               new object[] { _Profile.VoucherID, _Profile.SampleName, _Profile.PropertyName, _Profile.Prec, _Profile.Result, _Profile.OriginalResult, _Profile.OverRange }).Tables[0].Rows[0][0].ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    log.Error(e);
                  
                    throw e;
                }
            }
            return true;
        }

        /// <summary>
        /// 录入结果
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>返回错误信息</returns>
        public string RecodeResult(Profile profile)
        {

            string msg = "";
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("QE31_In_Grade_UpdateProfile", new string[] { "VoucherID", "SampleName", "PropertyName", "Prec", "Result", "OriginalResult", "OverRange" },
                                                           new object[] { profile.VoucherID, profile.SampleName, profile.PropertyName, profile.Prec, profile.Result, profile.OriginalResult, profile.OverRange }).Tables[0];

                int item = 0;
                foreach (DataRow row in dt.Rows)
                {
                    msg = msg + row["Msg"].ToString();
                    item = int.Parse(row["Item"].ToString());
                }
                return msg;
               

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                log.Error(e);
               
                throw e;
            }
        }

        public Profile SelectProfile(string voucherID,string propertyName)
        {
            DataTable dt = gate.SelectDataSet(@"SELECT* FROM [Profile] WHERE VoucherID = @VoucherID and PropertyName=@PropertyName",
                              new object[] { voucherID, propertyName }).Tables[0];
            var item = 0;
            if (dt.Rows.Count > 0)
            {
                 item = int.Parse(dt.Rows[0]["Item"].ToString());
            }


            return db.Select<Profile>(new Profile { VoucherID = voucherID, Item = item });
          

           
        }
        public bool CreateProfile(Profile profile)
        {
            Console.WriteLine("ProfileDAL-CreateMaterial():" + DateTime.Now.ToString());
            try
            {
                return db.Insert(profile);
            }
            catch (Exception e)
            {
               
                
                throw new Exception(e.Message);
            }
        }

        public bool UpdateProfile(Profile profile)
        {
            Console.WriteLine("ProfileDAL-UpdateMaterial():" + DateTime.Now.ToString());
            try
            {
                return db.Update(profile);
            }
            catch (Exception e)
            {
               
               
                throw new Exception(e.Message);
            }
        }
        
        public bool DeleteProfile(Profile profile)
        {
            Console.WriteLine("ProfileDAL-DelMaterial():" + DateTime.Now.ToString());
            try
            {
                db.Delete(profile);
                return true;
            }
            catch (Exception ex)
            {
              
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public bool Dell(string voucherID, string propertyName)
        {

            return false;
        }

       
    }
}