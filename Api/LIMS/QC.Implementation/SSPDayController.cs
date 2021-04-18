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
    [RoutePrefix("api/LIMS/SSPDayController")]
    public class SSPDayController:ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        [Route("getSSPMaterial")]
        [HttpGet]
        public IHttpActionResult getSSPMaterial()
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("aspnet_i_querySSPMaterial",
               new string[] { },
               new object[] { }).Tables[0];

                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }

        }

        [Route("getSSPDayREPORT")]
        [HttpGet]
        public IHttpActionResult getSSPDayREPORT(DateTime beginTime, DateTime endTime, string lot_No, string line, int interval)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("aspnet_i_SSP_DAY_REPORT_V1",
               new string[] { "B", "E", "LOT_NO","LINE", "Interval" },
               new object[] { beginTime, endTime, lot_No,line, interval }).Tables[0];

                return Ok(_helper.ConvertJson(dt));
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
