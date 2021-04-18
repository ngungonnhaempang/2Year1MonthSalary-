using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using FEPVWebApiOwinHost.Models.Gate;
using FEPVWebApiOwinHost.Models;
using log4net;
using System.Data.Common;
using FEPVWebApiOwinHost.Filter;

namespace FEPVWebApiOwinHost
{

    [FilterIP]
    [RoutePrefix("api/Gate/Checker")]
    public class GateCheckerController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("Beling");
        protected readonly GateContext _GateContext = new GateContext();
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();


        /// <summary>
        /// 得到经办人，如安环的承揽商  、总务的
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        [Route("GetCheckerByKind")]
        [HttpGet]
        public IHttpActionResult GetCheckerByKind(string kind)
        {


            Console.WriteLine("GetCheckerByKind：" + DateTime.Now.ToString());
            DataTable dt = gate.ExecuteStoredProcedure("OS_GetCheckerByKind", new string[] { "Kind" }, new object[] { kind }).Tables[0];

            DataTable tb = new DataTable();
            tb.Columns.Add("Person");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i]["Person"].ToString()))
                {
                    string pStrings = dt.Rows[i]["Person"].ToString().Replace('|', ',');
                    DataRow row = tb.NewRow();
                    row["Person"] = pStrings;
                    tb.Rows.Add(row);
                }
            }
            return Ok(_helper.ConvertJson(tb));
        }

        [Route("GetCheckersByLevel")]
        [HttpGet]
        /// <summary>
        /// 根据签核等级获取所有需要签核的人
        /// </summary>
        /// <param name="owner"></pGetCheckersByLevelaram>
        /// <returns></returns>
        public IHttpActionResult GetDefCheckersByOwner(string owner, string fLowKey,string kinds,DateTime?  checkDate)
        {

            Console.WriteLine("GetDefCheckersByOwner：" + DateTime.Now.ToString());
            DataTable dt = gate.ExecuteStoredProcedure("OS_GetCheckers", new string[] { "EmployeeID", "FLowKey", "Kinds", "CheckDate" }, new object[] { owner, fLowKey, kinds, checkDate }).Tables[0];

            DataTable tb = new DataTable();
            tb.Columns.Add("Person");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i]["Person"].ToString()))
                {
                    string pStrings = dt.Rows[i]["Person"].ToString().Replace('|', ',');
                    DataRow row = tb.NewRow();
                    row["Person"] = pStrings;
                    tb.Rows.Add(row);
                }
            }
            return Ok(_helper.ConvertJson(tb));
        }
        [Route("OS_CheckerOverGrade")]
        [HttpGet]
        /// <summary>
        /// 根据签核等级获取所有需要签核的人
        /// </summary>
        /// <param name="owner"></pGetCheckersByLevelaram>
        /// <returns></returns>
        public IHttpActionResult OS_CheckerOverGrade(string owner,  string kinds,string QCNode)
        {

            Console.WriteLine("GetDefCheckersByOwner：" + DateTime.Now.ToString());
            DataTable dt = gate.ExecuteStoredProcedure("OS_CheckerOverGrade", new string[] { "EmployeeID",  "Kinds", "QCNode" }, new object[] { owner, kinds, QCNode }).Tables[0];

            DataTable tb = new DataTable();
            tb.Columns.Add("Person");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i]["Person"].ToString()))
                {
                    string pStrings = dt.Rows[i]["Person"].ToString().Replace('|', ',');
                    DataRow row = tb.NewRow();
                    row["Person"] = pStrings;
                    tb.Rows.Add(row);
                }
            }
            return Ok(_helper.ConvertJson(tb));
        }

        /// <summary>
        /// 根据单据号获取签核等级
        /// </summary>
        /// <param name="goodsType"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        [Route("GetCheckLevel")]
        [HttpGet]
        public IHttpActionResult GetCheckLevel(string voucherId)
        {
            Console.WriteLine("GetCheckLevel：" + DateTime.Now.ToString());
            string msg = gate.ExecuteStoredProcedure("OS_GetCheckLevel", new string[] { "VoucherID" }, new object[] { voucherId }).Tables[0].Rows[0][0].ToString();

            Console.WriteLine("Level：" + msg);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("msg", msg);
            return Ok(values);
        }

        /// <summary>
        /// 判断是否正常工作日
        /// </summary>
        /// <param name="goodsType"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        [Route("IsWorkDay")]
        [HttpGet]
        public IHttpActionResult IsWorkDay(DateTime day)
        {
            string msg = gate.ExecuteStoredProcedure("OS_IsWorkDay", new string[] { "ExpectIn" }, new object[] { day }).Tables[0].Rows[0][0].ToString();

            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("msg", msg);
            return Ok(values);
        }

        [Route("GateQueryStatus")]
        [HttpGet]
        public IHttpActionResult GateQueryStatus(string ctype, string language, string flag)
        {
            DataTable table = this.gate.ExecuteStoredProcedure("G_GetGateQueryStatus", 
                new string[] { "Ctype", "Language", "Flag" }, new object[] { ctype, language, flag }).Tables[0];
            return this.Ok<DataTable>(table);
        }

      
    }
}
