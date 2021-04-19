using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS.Model;
using Shawoo.Core;
using Newtonsoft.Json;
using System.Data.Common;

namespace LIMS.DAL
{
    public class MIGOReceive : IReceiveOperation
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private DB db = new DB("LIMS");
        protected static NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");



        public DataTable GetRecieve(string voucherID)
        {
            DataTable dt = gate.DbHelper.ExecuteStoredProcedure("MMJY_GetReceiveGeneralByVouID",
                    new string[] { "UserID", "VoucherID" },
                    new object[] { DB.User, voucherID }).Tables[0];
            return dt;
        }

        public string CreateVoucherID(string sampleName, DateTime prodDate)
        {
            Console.WriteLine("MIGOReceive-CreateVoucherID()--" + DateTime.Now.ToString());
            try
            {
                return gate.DbHelper.ExecuteStoredProcedure("C_CreateDocVoucherID",
                    new string[] { "UserID", "SampleName", "ProdDate", "DOCType" },
                    new object[] { DB.User, sampleName, prodDate, "SP" }).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 创建混样的检验批
        /// </summary>
        /// <param name="wrgg"></param>
        /// <returns></returns>
        public bool CreateWRGGProfile(ReceiveMIGODraf[] wrgs, ReceiveGeneral general, out string msg)
        {
             msg = string.Empty;
            try
            {
                Console.WriteLine("--MIGOReceive InsertRecieve()--" + DateTime.Now.ToString());
				#region Find VersionID base On Material
				DataTable dt = gate.DbHelper.ExecuteStoredProcedure("MMJY_GetVersionID",
                                    new string[] { "UserID", "LotNo", "SampleName" },
                                    new object[] { DB.User, general.LOT_NO, general.SampleName }).Tables[0];
                if (dt.Rows.Count < 0)
                {
                    return false;
                }
                Console.WriteLine(dt.Rows.Count);
                msg = dt.Rows[0]["MSG"].ToString();
                Console.WriteLine(msg);
                if (!string.IsNullOrEmpty(msg))
                    return false;

                string _VersionID = dt.Rows[0]["VersionID"].ToString();

                #endregion
                string _VoucherID = CreateVoucherID(general.SampleName, DateTime.Now);
                Console.WriteLine("VoucherID:" + _VoucherID);
                if (string.IsNullOrEmpty(_VoucherID))
                {
                    msg = "CreateVoucherID failed";
                    return false;
                }
      
                Console.WriteLine("VoucherID:" + _VoucherID);
                if (string.IsNullOrEmpty(_VoucherID))
                {
                    msg = "CreateVoucherID failed";
                    return false;
                }

                general.VoucherID = _VoucherID;
                general.UserID = DB.User;
                general.GradeVersion = _VersionID;
            //    general.SheetDate = DateTime.Now;
               // general.Grade = "";
                // general.State = "1";
                general.Stamp = DateTime.Now;
                Console.WriteLine(JsonConvert.SerializeObject(general));
                if (db.Save(general))
                {
                   
                      int i = 0;
                      foreach (ReceiveMIGODraf wrg in wrgs)
                      {
                          ReceiveMIGODraf wrgg = new ReceiveMIGODraf();
                          wrgg.VoucherID = wrg.VoucherID;
                          wrgg.RVoucherID = _VoucherID;
                          wrgg.UserId = DB.User;
                          wrgg.Stamp = System.DateTime.Now;
                        
                          db.Save(wrgg);
                          i++;
                      }
                      if (i == wrgs.Length)
                      {
                          return true;
                      }
                      else {
                          msg = "Save  failed";
                          return false;
                      }
                }
                else {
                    msg = "save ReceiveGeneral failed";
                    return false;
                }
              
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }
        public bool DeleteMIGO(string voucherID,out string msg)
        {
            msg = string.Empty;
            try
            {
                Console.WriteLine("--MIGOReceive DeleteMIGO()--" +voucherID+ DateTime.Now.ToString());
                //核查是否能删除
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("MMJY_DeleteWRGG",
                                               new string[] { "UserID", "VoucherID" },
                                               new object[] { DB.User, voucherID }).Tables[0];

                msg= dt.Rows[0]["MSG"].ToString();
                if(!string.IsNullOrEmpty(msg))
                {
                    return false;
                }

                log.Info("DeleteMIGO");
                return true;
               
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }

        public bool CreateMIGO(ReceiveWRGG wrgg,out string msg)
        {
           string _VoucherID = Guid.NewGuid().ToString();
           msg = "Create failed";
            try
            {
                Console.WriteLine("--MIGOReceive InsertRecieve()--" + DateTime.Now.ToString());
                msg = "Create failed";

                Console.WriteLine(JsonConvert.SerializeObject(wrgg));
                int countgrade = gate.SelectScalar<int>("SELECT COUNT(*) FROM ReceiveWRGG WHERE EBELN = @EBELN AND EBELP = @EBELP AND ImportBatch=@ImportBatch " +
					"AND SampleName = @SampleName AND LOT_NO = @LOT_NO AND  Status<>'X'",
                                new object[] { wrgg.EBELN, wrgg.EBELP, wrgg.ImportBatch,wrgg.SampleName,wrgg.LOT_NO });
                   if (countgrade > 0)
                   {
                       msg = "IS Exsit!";
                       return false;
                   }
                   wrgg.VoucherID = _VoucherID;
                   wrgg.Stamp = System.DateTime.Now;
                   wrgg.UserId = DB.User;
                 return db.Save(wrgg);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 建立进料检验
        /// </summary>
        public bool InsertRecieve(DOCReceive recieve, ReceiveGeneral general, out string msg)
        {
            try
            {
                Console.WriteLine("--MIGOReceive InsertRecieve()--" + DateTime.Now.ToString());
                msg = "Create failed";
                //根据物料，样品查找等级版本
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("MMJY_GetVersionID",
                                    new string[] { "UserID", "LotNo", "SampleName" },
                                    new object[] { DB.User, general.LOT_NO, general.SampleName }).Tables[0];
                if (dt.Rows.Count < 0)
                {
                    return false;
                }
                Console.WriteLine(dt.Rows.Count);
                //
                msg = dt.Rows[0]["MSG"].ToString();
                Console.WriteLine(msg);
                if (!string.IsNullOrEmpty(msg))
                    return false;

                string _VersionID = dt.Rows[0]["VersionID"].ToString();

                //CreateVoucherID
             
                string _VoucherID = CreateVoucherID(general.SampleName, DateTime.Now);
                Console.WriteLine("VoucherID:" + _VoucherID);
                if (string.IsNullOrEmpty(_VoucherID))
                {
                    msg = "CreateVoucherID failed";
                    return false;
                }

                general.VoucherID = _VoucherID;
                general.UserID = DB.User;
                general.GradeVersion = _VersionID;
                general.SheetDate = DateTime.Now;
                general.Grade = "";
                general.Stamp = DateTime.Now;
                general.DateOfSample =Convert.ToDateTime( DateTime.Now.ToString("yyyy-MM-dd"));
               // general.State = "1";

                Console.WriteLine(JsonConvert.SerializeObject(general));
                return db.Save(general);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }
    }
}
