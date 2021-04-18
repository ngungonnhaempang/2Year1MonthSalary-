using FEPVWebApiOwinHost.Filter;
using LIMS.Model;
using log4net;
using QC.DAL;
using RestSharp;
using System;
using System.Web.Http;

namespace FEPVWebApiOwinHost.QC
{
    [FilterIP]
    [RoutePrefix("api/LIMS/ISO")]
    public class ISOController : ApiController
    {
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        SampleService isoDAL = new SampleService();

        OtherQueryDAL otherISO = new OtherQueryDAL();
        HelperBiz _helper = new HelperBiz();

        /// <summary>
        /// Create Red And Yellow Voucher for
        /// </summary>
        [Route("CreateVoucher")]
        [HttpPost]
        public IHttpActionResult CreateRYVoucher(Report_ISO_Result entity)
        {
            var result = otherISO.CreateVoucher(entity);
            JsonObject json = new JsonObject();
            json["Success"] = result;
            Loger.Info(string.Format(@"ISOController/CreateRYVoucher/CreateVoucher:\n
{0} create RYVoucherID VoucherID {1} |  LOT_NO {2} | Colorlabel {3} | SampleName {4} ", entity.CreateBy, entity.VoucherID, entity.ColorLabel, entity.LOT_NO, entity.SampleName));
            return Ok(json);
        }

        [Route("GetRYVoucher")]
        [HttpGet]
        public IHttpActionResult GetRYVoucher(string voucherid)
        {
            var result = otherISO.GetRYVoucher(voucherid);
            JsonObject json = new JsonObject();
            json["Success"] = result;
            return Ok(json);
        }

        [Route("UpdateRYVoucher")]
        [HttpGet]
        public IHttpActionResult UpdateVoucher(string userID, string voucherID, string state, string status, string reason, string solution, string prevention, string remark)
        {
            bool result = otherISO.UpdateRYResult(userID, voucherID, state, status, reason, solution, prevention, remark);
            JsonObject json = new JsonObject();
            json["Success"] = result;

            return Ok(json);
        }



        [Route("UpdateSubmittedVoucher")]
        [HttpPost]
        public IHttpActionResult UpdateSubmittedVoucher(Report_ISO_Detail propertyNameList)
        {
            var result = otherISO.UpdateStatusVoucher(propertyNameList.VoucherID, propertyNameList.Status, propertyNameList.UserID, propertyNameList);
            JsonObject json = new JsonObject();
            json["Success"] = result;
            return Ok(json);
        }


        [Route("SendReminder")]
        [HttpGet]
        public IHttpActionResult SendReminder(string voucherID, string userid, string formkey)
        {
            var result = otherISO.RY_SendReminder(voucherID, userid, formkey);
            JsonObject json = new JsonObject();
            json["Success"] = result;
            return Ok(json);
        }
        [Route("DeleteVoucher")]
        [HttpPut]
        public IHttpActionResult DeleteVoucher(string VoucherID, string status,string userid )
        {
            var result = otherISO.UpdateStatusVoucher(VoucherID, status, userid, null);
            JsonObject json = new JsonObject();
            json["Success"] = result;
            return Ok(json);
        }
        [Route("DeleteVoucherByVoucherID")]
        [HttpPut]
        public IHttpActionResult DeleteVoucher(string VoucherID, string status)
        {
            var result = otherISO.UpdateStatusVoucher(VoucherID, status, null, null);
            JsonObject json = new JsonObject();
            json["Success"] = result;
            return Ok(json);
        }
        [Route("SearchRYVoucher")]
        [HttpGet]
        public IHttpActionResult SearchRYVoucher(string materialType,string voucherid, string userID, string sampleName, string LOT_NO, string status, string colorlabel, DateTime? B, DateTime? E, string Line)
        {
            JsonObject json = new JsonObject();
            try
            {
                var dt = otherISO.SearchRYVoucher(materialType, voucherid, userID, sampleName, LOT_NO, status, colorlabel, B, E, Line);
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }

        }

        [Route("GetDetailReport")]
        [HttpGet]
        public IHttpActionResult GetDetailReport(string voucherID)
        {
            JsonObject json = new JsonObject();
            try
            {
                var dt = otherISO.GetRYReportDetail(voucherID);
                return Ok(_helper.ConvertJson(dt));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }


        }
        [Route("GetAllUQReport")]
        [HttpGet]
        public IHttpActionResult GetAllUQReport(DateTime? B, DateTime? E, string sampleName, string Lot_no, string status, string colorlabel, string Line)
        {
            JsonObject json = new JsonObject();
            try
            {
                var dt = otherISO.Report_Q_RY_AllVoucher(B, E, sampleName, Lot_no, status, colorlabel, Line);
                return Ok(_helper.ConvertJson(dt));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }

        }

        [Route("DetailExport")]
        [HttpGet]
        public IHttpActionResult DetailExport(string voucherid, string UserID, string sampleName, string Lot_no, string status, string colorlabel, DateTime? B, DateTime? E, string Line)
        {
            JsonObject json = new JsonObject();
            try
            {
                var dt = otherISO.ISO_DetailExport(voucherid, UserID, sampleName, Lot_no, status, colorlabel, B, E, Line);
                return Ok(_helper.ConvertJson(dt));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }

        }




    }
   
}
