using FEPVWebApiOwinHost.Filter;
using FEPVWebApiOwinHost.Models.InOutEastGate;
using log4net;
using NBear.Common.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FEPVWebApiOwinHost
{
    [FilterIP]
    [RoutePrefix("api/Gate/InOutEastGate")]
    public class InOutEastGateController : ApiController
    {
        protected readonly EastGateContext Db = new EastGateContext();
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("Beling");
        private HelperBiz _helper = new HelperBiz();

        [Route("SearchRegisterVoucher")]
        [HttpGet]
        public IHttpActionResult SearchRegisterVoucher(string VoucherID, string Name, string EmpID, string FromDate, string ToDate, string CostCenter, string Status, string Language, string User)
        {
            try
            {
                var dt = gate.ExecuteStoredProcedure("[EG_SearchRegisterVoucher]",
                     new string[] { "VoucherID", "Name", "EmpID", "FromDate", "ToDate", "CostCenter", "Status", "Language", "User" }
                     , new object[] { VoucherID, Name, EmpID, FromDate, ToDate, CostCenter, Status, Language, User }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("SaveEastGateVoucher")]
        [HttpPost]
        public IHttpActionResult SaveEastGateVoucher(EG_Voucher voucher)
        {
            Console.WriteLine("SaveEastGateVoucher");
            try
            {
                var VoucherInDB = Db.EastGateVouchers.Find(voucher.VoucherID);

                if (VoucherInDB != null)
                {
                    VoucherInDB.StartDate = voucher.StartDate;
                    VoucherInDB.EndDate = voucher.EndDate;
                    VoucherInDB.RegisterReason = voucher.RegisterReason;
                    VoucherInDB.ModifyDate = DateTime.Now;
                    VoucherInDB.StartDate = voucher.StartDate;
                    VoucherInDB.InternalNumber = voucher.InternalNumber;

                    var currentEmployee = Db.EastGateEmployees.Where(x => x.VoucherID == voucher.VoucherID);

                    if (currentEmployee != null)
                    {
                        Db.EastGateEmployees.RemoveRange(currentEmployee);
                        if (voucher.EmployeeList != null)
                        {
                            Db.EastGateEmployees.AddRange(voucher.EmployeeList);
                            voucher.EmployeeList = null;
                        }
                    }

                    Db.Entry(VoucherInDB).State = EntityState.Modified;
                }
                else
                {
                    voucher.VoucherID = gate.SelectScalar<string>("select dbo.fnContractorEmployeeID(@typeID)", new object[] { "EastGate" });
                    voucher.CreateDate = voucher.ModifyDate = DateTime.Now;

                    foreach (var item in voucher.EmployeeList)
                    {
                        item.VoucherID = voucher.VoucherID;
                    }

                    Db.EastGateVouchers.Add(voucher);

                    VoucherInDB = voucher;
                }

                Db.SaveChanges();

                var eastGateVOucher = new EG_Voucher();
                eastGateVOucher.VoucherID = VoucherInDB.VoucherID;
                return Ok(eastGateVOucher);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message);
            }
        }

        [Route("UpdateStatusVoucher")]
        [HttpPut]
        public IHttpActionResult UpdateStatusVoucher(string VoucherID, string status, string AppointmentDate)
        {
            try
            { 
                gate.ExecuteStoredProcedure("EG_UpdateStatusVoucher", new string[] { "VoucherID", "Status", "AppointmentDate" }
                                                                     ,new object[] {  VoucherID,   status,   AppointmentDate });
     
                JSONObject json = new JSONObject();
                json["Success"] = true;
                return Ok(json);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("EG_SendMail")]
        [HttpPost]
        public IHttpActionResult EG_SendMail(string flowname, string VoucherID, string FromUser, string MailKind)
        {
            try
            {
                gate.ExecuteStoredProcedureNonQuery("EG_@SendMailSubmit"
                , new string[] { "flowname", "VoucherID", "FromUser", "MailKind" }
                , new object[] { flowname, VoucherID, FromUser, MailKind });
                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }
    }
}
