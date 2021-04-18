using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using QC.Utility;

namespace QC.Implementation
{
    [FilterIP]
    [RoutePrefix("api/LIMS/POLY21Controller")]
    public class POLY21Controller:ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        [Route("getPOLY21Material")]
        [HttpGet]
        public IHttpActionResult getPOLY21Material()
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("aspnet_i_queryPOLY21Material",
               new string[] {},
               new object[] {}).Tables[0];

                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }

        }

        [Route("getPOLY21REPORT")]
        [HttpGet]
        public IHttpActionResult getPOLY21REPORT(DateTime beginTime, DateTime endTime, string lot_No, int interval)
        {
            try
            {                
               // DataRow[] drs 
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("aspnet_i_XXX_DAY_REPORT_V1",
               new string[] { "B", "E", "LOT_NO", "Interval","SampleNO" },
               new object[] { beginTime, endTime, lot_No, interval,"S01020001" }).Tables[0];

                DataTable dt2 = dt.Clone();
                foreach (DataRow dr in dt.Select("","LOT_NO"))
                {
                    dt2.Rows.Add(dr.ItemArray);
                }
                return Ok(_helper.ConvertJson(dt2));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }

        }
        Exception MSG_Error
        {
            set
            {
                Console.WriteLine(value.Message);
                Loger.Error(value);
            }
        }

        string MSG_Info
        {
            set
            {
                Console.WriteLine(value);
                Loger.Info(value);
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
