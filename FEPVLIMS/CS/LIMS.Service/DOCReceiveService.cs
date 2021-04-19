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
using SAP.Middleware.Connector;
using LIMS.DAL;
using Newtonsoft.Json;

namespace LIMS.Service
{
    public class DOCReceiveService : MarshalByRefObject, IDOCReceive
    {
        public DOCReceiveService()
        {
          
        }
        private DB db = new DB("LIMS");
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        public DataTable GetReceive(string voucherID, string tablename) 
       {

           IReceiveOperation receiveDAL = Recieve_Facotry.CreateRecive(tablename);
           ReceiveOperation taskOperation = new ReceiveOperation(receiveDAL);
           return taskOperation.GetReceive(voucherID);
         
       }

       private string CreateVoucherIDByReceive()
       {
           string ReceCountCommand = "Select max(RIGHT(VoucherID,10))+1 FROM Receive";
           int count = 0;
           count = acQC.SelectScalar<int>(ReceCountCommand, new object[] { });
           string doc_no = "";
          return "S" + count.ToString().PadLeft(11, '0');
       }
       /// <summary>
        /// 一般车辆 物料无PTA ,判断是否有等级规则
        /// 多次过磅车辆 煤 EG 等
        /// 用户自定义的创建 原辅料的检查单，基本信息在 RecieveGeneral
        /// table
       /// </summary>
       /// <param name="Receive"></param>
       /// <param name="Doc"></param>
       /// <param name="msg"></param>
       /// <returns></returns>
       public bool InsertReceive(DOCReceive receive, ReceiveGeneral general, out string msg)
       {
           string TableName = "MIGO";
        
           if (!string.IsNullOrEmpty(receive.TableName))
           {
               Console.WriteLine("receive" + receive.TableName);
               string voucherid = CreateVoucherIDByReceive();
               receive.VoucherID = voucherid;
               Console.WriteLine(receive.VoucherID + receive.TableName);
               TableName = receive.TableName;
           }
           IReceiveOperation receiveDAL = Recieve_Facotry.CreateRecive(TableName);

           ReceiveOperation taskOperation = new ReceiveOperation(receiveDAL);
           return taskOperation.InsertRecieve(receive, general, out msg);
       }


       public bool CreateWRGGProfile(ReceiveMIGODraf[] wrgs, ReceiveGeneral general, out string msg)
       {
           MIGOReceive receive = new MIGOReceive();
           return receive.CreateWRGGProfile(wrgs, general,out msg);
       }

        /// <summary>
        /// 创建需要检验的收货计划
        /// </summary>
        /// <param name="wrgg"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
       public bool CreateMIGO(ReceiveWRGG wrgg, out string msg)
       {
           MIGOReceive receive = new MIGOReceive();
           return receive.CreateMIGO(wrgg, out msg);
       }

        /// <summary>
       /// 删除检验的收货计划
        /// </summary>
        /// <param name="wrgg"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
       public bool DeleteMIGO(string voucherID, out string msg)
       {
           MIGOReceive receive = new MIGOReceive();
           return receive.DeleteMIGO(voucherID, out msg);
       }
        /// <summary>
       /// 删除 创建的检验批
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="reason"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteReceive(string voucherID, string reason, out string msg)
        {
            try
            {
                msg = "";
                Console.WriteLine("DOCReceiveDAL ):" + voucherID + DateTime.Now.ToString());
                try
                {
                    string doctype = voucherID.Substring(0, 2);
                    if (doctype == "SP")
                    {
                        log.Info("DeleteReceive:" + voucherID + ":" + reason);
                        acQC.ExecuteNonQuery(@"Update ReceiveGeneral set State='X' ,Remark=@Reason WHERE VoucherID = @VoucherID and State='1'", new object[] { reason, voucherID });

                     return 1    ==
                         (int)acQC.DbHelper.SelectScalar("SELECT Count(VoucherID) FROM ReceiveGeneral WHERE VoucherID = @VoucherID and State='X'",
                       new object[] { voucherID });
                    }
                    else
                    {
                        log.Info("DeleteReceive:" + voucherID + ":" + reason);
                        acQC.ExecuteNonQuery(@"Update Receive set State='X' ,Remark=@Reason WHERE VoucherID = @VoucherID and State='1'", new object[] { reason, voucherID });
                        return 1 ==
                      (int)acQC.DbHelper.SelectScalar("SELECT Count(VoucherID) FROM Receive WHERE VoucherID = @VoucherID and State='X'",
                    new object[] { voucherID });

                    }
                   
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    msg = ex.Message;
                }
              
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return false;
        }
    }
}
