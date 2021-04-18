using FEPVWebApiOwinHost.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace FEPVWebApiOwinHost
{
    [FEPVWebApiOwinHost.Filter.UserFilter]
    [RoutePrefix("api/MIS/ShowContractor")]
    public class ShowContractorController : ApiController
    {
        protected readonly ContractorContext Db = new ContractorContext();
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        private NBear.Data.Gateway oAC = new NBear.Data.Gateway("Beling");

        private HelperBiz _helper = new HelperBiz();
        [Route("GetContractorInfo")]
        [HttpGet]
        public IHttpActionResult getContractorInfoByEmployeeID(string employeeId, int IsCompanyEmployee)
        {
            try
            {
                if (IsCompanyEmployee == 1)
                {
                    employeeId = employeeId.PadLeft(6, '0');
                }
                DataTable dtContractor = oAC.DbHelper.ExecuteStoredProcedure("HS_Q_Contractor_Image",
                new string[] { "UserID", "EmployeeID", "IsCompanyEmployee" }, new object[] { "", employeeId, IsCompanyEmployee }).Tables[0];

                if (dtContractor.Rows.Count > 0) {
                    var imagePath = Convert.ToBase64String((byte[])dtContractor.Rows[0]["Image"]);
                    dtContractor.Columns.Add("stringImage", typeof(string));

                    dtContractor.Rows[0]["stringImage"] = imagePath;
                }
               
                return Ok(_helper.ConvertJson(dtContractor));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("GetDeviceID")]
        [HttpGet]
        public IHttpActionResult getDeviceID(string ip)
        {
            try
            {
                DataTable dtdeviceIp = oAC.DbHelper.Select(@"SELECT DevicesID FROM Contractor_Devices_Timekeeper WHERE IP_Address = @IP",
                new object[] { ip }).Tables[0];

                return Ok(_helper.ConvertJson(dtdeviceIp));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("GetLog")]
        [HttpGet]
        public IHttpActionResult getLog(string EnrollNumber, int VerifyMode, string VerifyTime, string deviceIP)
        {
            try
            {
                oAC.DbHelper.ExecuteStoredProcedure("C_Timekeeper_Logs"
                        , new string[] { "EmployeeID", "VerifyStyle", "VerifyTime", "IP" }
                        , new object[] { EnrollNumber, VerifyMode, VerifyTime, deviceIP });
                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("GetLogEastGate")]
        [HttpGet]
        public IHttpActionResult getLogEastGate(string EnrollNumber, int VerifyMode, string VerifyTime, string deviceIP)
        {
            try
            {
                var employeeid = EnrollNumber.PadLeft(6, '0');
                oAC.DbHelper.ExecuteStoredProcedure("EG_Timekeeper_Logs"
                        , new string[] { "EmployeeID", "VerifyStyle", "VerifyTime", "IP" }
                        , new object[] { employeeid, VerifyMode, VerifyTime, deviceIP });
                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("SearchNotify")]
        [HttpGet]
        public IHttpActionResult searchNotify(string FromDate, string ToDate)
        {
            try
            {
                DataTable dtNotify = oAC.DbHelper.ExecuteStoredProcedure("C_SearchNotify"
                        , new string[] { "FromDate", "ToDate" }
                        , new object[] { FromDate, ToDate }).Tables[0];

                return Ok(_helper.ConvertJson(dtNotify));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("CreateNotify")]
        [HttpGet]
        public IHttpActionResult createNotify(string NotifyName, string Remark)
        {
            try
            {
                oAC.ExecuteNonQuery(@"INSERT INTO ContractorNotify (NotifyName, CreateDate, LastShowDate, Status, Remark)
                                      VALUES (@NotifyName, @CreateDate, @LastShowDate, @Status, @Remark )"
                            , new object[] { NotifyName, DateTime.Now, DateTime.Now, "InActive", Remark });
               
                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }


        [Route("UpdateNotify")]
        [HttpGet]
        public IHttpActionResult updateNotify(string NotifyName, string Remark)
        {
            try
            {
                oAC.ExecuteNonQuery(@"UPDATE ContractorNotify
			                          SET    Remark = @Remark
			                          WHERE NotifyName = @NotifyName"
                 , new object[] { Remark, NotifyName });
                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("PostNotify")]
        [HttpGet]
        public IHttpActionResult postNotify(string CombinedNotifyName)
        {
            try
            {
                oAC.ExecuteNonQuery(@"UPDATE ContractorNotify
			                          SET    Status = @NewStatus
			                          WHERE Status = @OldStatus"
                 , new object[] { "InActive", "Active" });

                string[] NotifyName = CombinedNotifyName.Split(',');
                foreach (var item in NotifyName)
                {
                    oAC.ExecuteNonQuery(@"UPDATE ContractorNotify
			                          SET    Status = @Status
				                           ,LastShowDate = @date	 
			                          WHERE NotifyName = @NotifyName"
                      , new object[] { "Active", DateTime.Now, item });
                }

                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("StopNotify")]
        [HttpGet]
        public IHttpActionResult stopNotify()
        {
            try
            {
                oAC.ExecuteNonQuery(@"UPDATE ContractorNotify
			                          SET    Status = @NewStatus
			                          WHERE Status = @OldStatus"
                  , new object[] { "InActive", "Active" });

                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }


        [Route("DeleteNotify")]
        [HttpGet]
        public IHttpActionResult deleteNotify(string CombinedNotifyName)
        {
            try
            {
                string[] NotifyName = CombinedNotifyName.Split(',');
                foreach (var item in NotifyName)
                {
                    oAC.ExecuteNonQuery(@"UPDATE ContractorNotify
			                          SET    Status = @Status	 
			                          WHERE NotifyName = @NotifyName"
                         , new object[] { "Deleted", item });
                }
                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("GetActiveNotify")]
        [HttpGet]
        public IHttpActionResult getActiveNotify()
        {
            try
            {
                var dtActive = oAC.SelectDataSet("SELECT NotifyName FROM ContractorNotify WHERE Status = @Status"
                  , new object[] { "Active" }).Tables[0];

                return Ok(_helper.ConvertJson(dtActive));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }
    }
}
