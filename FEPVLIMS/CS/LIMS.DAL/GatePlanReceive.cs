using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS.Contract;
using LIMS.Model;
using Newtonsoft.Json;
using Shawoo.Core;

namespace LIMS.DAL
{


    public class GatePlanReceive : IReceiveOperation
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private DB db = new DB("LIMS");
        protected static NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");


        public DataTable GetRecieve(string voucherID)
        {

            return null;
        }
       /// <summary>
       ///  是否这个计划已经建立进料检验
        ///得到这个 SAMPLEName
       /// 核查这个 料号是否有建立判断 
        ///条件成立创建计划
       /// </summary>
       /// <param name="recieve"></param>
       /// <param name="general"></param>
       /// <param name="msg"></param>
       /// <returns></returns>
        public bool InsertRecieve(DOCReceive recieve, ReceiveGeneral general, out string msg)
        {
            Console.WriteLine("gate InsertRecieve");
            msg = "Create failed";
            DataTable dt = gate.DbHelper.ExecuteStoredProcedure("QC02_IsCan_CreateReceive",
                                                          new string[] { "UserID", "PlanID", "LotNo","SampleName" },
                                                          new object[] { recieve.UserID, recieve.GatePlanID, recieve.LOT_NO, recieve.SampleName }).Tables[0];
            if (dt.Rows.Count < 0)
            {
                return false;
            }
            Console.WriteLine(dt.Rows.Count);
         
            msg = dt.Rows[0]["MSG"].ToString();

            Console.WriteLine(msg);
            if (!string.IsNullOrEmpty(msg))
                return false;

            string _sampleName = dt.Rows[0]["SampleName"].ToString();
            string _versionID = dt.Rows[0]["VersionID"].ToString();

            Console.WriteLine("VoucherID:"+recieve.VoucherID);
      
            recieve.UserID = DB.User;
            recieve.SampleName = _sampleName;
            recieve.GradeVersion = _versionID;
           // recieve.SheetDate = System.DateTime.Now;

            Console.WriteLine(JsonConvert.SerializeObject(recieve));
            return  db.Save(recieve);
               

                         


        }
    }
}
