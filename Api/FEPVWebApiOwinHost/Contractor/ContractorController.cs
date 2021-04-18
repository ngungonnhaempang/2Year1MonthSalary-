using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Http;
using FEPVWebApiOwinHost.Models;
using log4net;
using System.Data;
using System.Collections.Generic;
using FEPVWebApiOwinHost.Filter;
using System.Net.Http;
using System.Net;
using System.Collections;

namespace FEPVWebApiOwinHost
{

    [FilterIP]
    [RoutePrefix("api/Gate/Contractor")]
    public class ContractorController : ApiController
    {
        protected readonly ContractorContext Db = new ContractorContext();
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        private NBear.Data.Gateway oAC = new NBear.Data.Gateway("Beling");

        private HelperBiz _helper = new HelperBiz();


        [Route("GetEmployee")]
        [HttpGet]
        public IHttpActionResult GetEmployee(string DepartmentID)
        {
            try
            {
                var dt = oAC.ExecuteStoredProcedure("C_Get_HRUserInfo",
                     new string[] { "DepartmentID" }, new object[] { DepartmentID }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetDepartment")]
        [HttpGet]
        public IHttpActionResult GetDataDepartment(string EmployeeID)
        {
            try
            {
                var dt = oAC.ExecuteStoredProcedure("GetDataDepartment",
                     new string[] { "EmployeeID" }, new object[] { EmployeeID }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetAllEmployeeInContractor")]
        [HttpGet]
        public IHttpActionResult GetDataContractor(string VoucherID, string ContractorID, string Language)
        {
            try
            {
                var dt = oAC.ExecuteStoredProcedure("C_GetAllEmployeeInContractor",
                     new string[] { "VoucherID", "ContractorID", "Language" }, new object[] { VoucherID, ContractorID, Language }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetContractorByIdCard")]
        [HttpGet]
        public IHttpActionResult GetContractorByIdCard(string IdCard, string ContractorID, string Language)
        {
            try
            {
                var dt = oAC.ExecuteStoredProcedure("C_GetContractorByIdCard"
                            , new string[] { "IdCard", "ContractorID", "Language" }
                            , new object[] { IdCard, ContractorID, Language }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("ContractorQuaSaveStatus")]
        [HttpPut]
        public HttpResponseMessage ContractorQuaSaveStatus(string voucherID, string idCard, string status)
        {
            if (string.IsNullOrEmpty(idCard))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "IdCard is empty！");
            try
            {
                if (status == "X")
                {
                    oAC.ExecuteNonQuery(@"UPDATE Contractors
                                            SET [Status] = @Status,
                                            [Stamp] = @Stamp,
                                            [PreStatus] = @PreStatus
                                            WHERE IdCard = @IdCard
                                            AND VoucherID = @VoucherID"
                        ,new object[] { status, DateTime.Now,'X', idCard, voucherID });
                }
                else if (status == "F")
                {
                    oAC.ExecuteNonQuery(@"UPDATE Contractors
                                        SET [Status] = @Status,
                                        [Stamp] = @Stamp,
                                        [PreStatus] = @PreStatus
                                        WHERE VoucherID = @VoucherID"
                        ,new object[] { status, DateTime.Now,'F', voucherID });
                }

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message + e.StackTrace);
            }
        }

        /// <summary>
        /// State changes when individual suspension of contractor
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="idCard"></param>
        /// <param name="status"></param>
        /// <param name="stardateCancel"></param>
        /// <param name="enddateCancel"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [Route("ContractorSaveStatusSuspend")]
        [HttpPut]
        public HttpResponseMessage ContractorSaveStatusSuspend(string voucherID, string idCard, string status, string stardateCancel, string enddateCancel, string employeeID)
        {
            if (string.IsNullOrEmpty(idCard))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "IdCard is empty！");
            try
            {
                if (status == "SC")
                {
                    oAC.ExecuteNonQuery(@"UPDATE Contractors
                                                SET [Status] = @Status,
                                                [Stamp] = @Stamp,
                                                [StartdateCancel] = @StartdateCancel,
                                                [EnddateCancel] = @EnddateCancel
                                                WHERE IdCard = @IdCard
                                                AND VoucherID=@VoucherID"
                        , new object[] { status, DateTime.Now, stardateCancel, enddateCancel, idCard, voucherID });
                }
                else
                {
                    if (status == "PC" || status == "O")
                    {
                        oAC.ExecuteNonQuery(@"UPDATE Contractors
                                            SET [Status] = @Status,
                                            [Stamp] = @Stamp,
                                            [StartdateCancel] = @StartdateCancel,
                                            [EnddateCancel] = @EnddateCancel
                                            WHERE IdCard = @IdCard
                                            AND VoucherID=@VoucherID"
                            , new object[] { status, DateTime.Now, DateTime.Now, null, idCard, voucherID });
                    }
                    else
                    {
                        oAC.ExecuteNonQuery(@"UPDATE Contractors
                                            SET [Status] = @Status,
                                            [Stamp] = @Stamp,
                                            [StartdateCancel] = @StartdateCancel,
                                            [EnddateCancel] = @EnddateCancel
                                            WHERE IdCard = @IdCard
                                            AND VoucherID=@VoucherID"
                            , new object[] { status, DateTime.Now, stardateCancel, enddateCancel, idCard, voucherID });
                    }

                    oAC.ExecuteNonQuery(@"UPDATE ContractorBiometric
                                                SET IsValid=0
                                                WHERE EmployeeID = @EmployeeID"
                        , new object[] { employeeID });

                }

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message + e.StackTrace);
            }
        }

        [Route("Get")]
        [HttpGet]
        public IHttpActionResult Get(string ContractorID, string Language)
        {
            Console.WriteLine("GETLan");
            try
            {
                DataTable dt = oAC.ExecuteStoredProcedure("GD_HS_GetContractorByIdCardEmployerID",
                   new string[] { "ContractorID", "Language" }, new object[] { ContractorID, Language }).Tables[0];

                return Ok(dt);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

//        [Route("ImportRemove")]
//        [HttpPost]
//        public IHttpActionResult ImportRemove(string IdCard, string employerId, string userid)
//        {

//            var contractorInDb = Db.Contractors.Find(IdCard, employerId);
//            if (contractorInDb.Status == "I" && contractorInDb.UserId == userid)
//            {

//                Db.Contractors.Remove(contractorInDb);
//                Db.SaveChanges();

//                return Ok();
//            }
//            else
//            {
//                return BadRequest(IdCard + ": Remove is failed,Status=I");
//            }

//        }

//        [Route("ImportSave")]
//        [HttpPost]
//        public IHttpActionResult ImportSave(ContractorImport xls)
//        {
//            string errMsg = string.Empty;
//            foreach (var xl in xls.Contractorxls)
//            {
//                var cons = Db.ContractorQualifications.Where(c => c.ContractorName == xl.Employer && c.Status == "Q").ToList();
//                if (cons.Count == 1)
//                {

//                    var contractorQua = new Contractor();
//                    var contractorInDb = Db.Contractors.Find(xl.IdCard, cons[0].ContractorID);

//                    if (contractorInDb == null)
//                    {
//                        contractorQua.ContractorID = cons[0].ContractorID;
//                        contractorQua.IdCard = xl.IdCard;
//                        contractorQua.Name = xl.Name;
//                        contractorQua.Phone = xl.Phone;
//                        contractorQua.Remark = xl.Remark;
//                        contractorQua.TrainDate = xl.TrainDate;
//                        contractorQua.AppointmentDate = Convert.ToDateTime(xl.AppointmentDate);
//                        contractorQua.Status = "I";
//                        contractorQua.Stamp = System.DateTime.Now;
//                        contractorQua.UserId = xls.userid;
//                        Db.Contractors.Add(contractorQua);
//                        Db.SaveChanges();
//                        oAC.ExecuteNonQuery(@"INSERT INTO ContracotorImportLog 
//                                                   ([IdCard],[ContractorID],[Stamp],[UserId])
//                                               VALUES
//                                                   (@IdCard ,@ContractorID ,@Stamp ,@UserId)"
//                           , null, new object[] { xl.IdCard,cons[0].ContractorID,System.DateTime.Now,xls.userid
//                                          });

//                    }
//                    else
//                    {
//                        errMsg = errMsg + xl.IdCard + ",";
//                        // return BadRequest(xl.IdCard + ":The contractor is already exist");

//                    }
//                }
//                else
//                {
//                    return BadRequest("The contractor is not  exist");

//                }
//            }

//            if (!string.IsNullOrEmpty(errMsg))
//            {
//                return BadRequest(errMsg + " already exist!");

//            }
//            else
//            {
//                return Ok();

//            }
//        }

        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(ContractorDto contractortoDb)
        {
            Console.WriteLine("Save");
            try
            {
                var contractorInDb = Db.Contractors.Find(contractortoDb.EmployeeID, contractortoDb.ContractorID);

                //update
                if (contractorInDb != null)
                {
                    contractorInDb.IdCard = contractortoDb.IdCard;
                    contractorInDb.ContractorID = contractortoDb.ContractorID;
                    contractorInDb.VoucherID = contractorInDb.VoucherID;
                    contractorInDb.Name = contractortoDb.Name;
                    contractorInDb.Phone = contractortoDb.Phone;
                    contractorInDb.Birthday = contractortoDb.Birthday;
                    contractorInDb.Sex = contractortoDb.Sex;
                    contractorInDb.Job = contractortoDb.Job;
                    contractorInDb.Region = contractortoDb.Region;
                    contractorInDb.Remark = contractortoDb.Remark;
                    if (contractortoDb.Status == "N" || contractortoDb.Status == "X")
                    {
                        contractorInDb.Status = contractortoDb.Status;
                    }
                    else
                    {
                        contractorInDb.Status = contractortoDb.IsUpdate ? "Q" : "F";
                    }
                    contractorInDb.PreStatus = contractortoDb.Status;
                    contractorInDb.Stamp = DateTime.Now;
                    contractorInDb.UserId = contractortoDb.UserId;
                    contractorInDb.TrainDate = contractortoDb.TrainDate;
                    contractorInDb.AppointmentDate = contractortoDb.AppointmentDate;
                    contractorInDb.EmployeeID = contractorInDb.EmployeeID;
                    contractorInDb.ReasonReturn = null;
                    Db.Entry(contractorInDb).State = EntityState.Modified;
                }
                else
                {
                    contractorInDb = new Contractor
                    {
                        VoucherID = contractortoDb.VoucherID,
                        EmployeeID = oAC.SelectScalar<string>("select dbo.fnContractorEmployeeID(@typeID)", new object[] { "Employee" }),
                        IdCard = contractortoDb.IdCard,
                        Name = contractortoDb.Name,
                        ContractorID = contractortoDb.ContractorID,
                        Phone = contractortoDb.Phone,
                        Birthday = contractortoDb.Birthday,
                        Sex = contractortoDb.Sex,
                        Job = contractortoDb.Job,
                        Region = contractortoDb.Region,
                        Remark = contractortoDb.Remark,
                        Status = contractortoDb.Status,
                        PreStatus = contractortoDb.Status,
                        Stamp = DateTime.Now,
                        UserId = contractortoDb.UserId,
                        TrainDate = contractortoDb.TrainDate,
                        AppointmentDate = contractortoDb.AppointmentDate
                    };

                    Db.Contractors.Add(contractorInDb);

                    var _biometric = new ContractorBiometric
                    {
                        EmployeeID = contractorInDb.EmployeeID,
                        CardNo = null,
                        FaceTmp = null,
                        IsValid = 0
                    };
                    Db.ContractorBiometrics.Add(_biometric);
                }

                Db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message);
            }
        }

        [Route("GetVoucherID")]
        [HttpGet]
        public IHttpActionResult GetVoucherID()
        {
            Console.WriteLine("create voucher id");
            ArrayList voucherId = new ArrayList();
            Dictionary<string, string> id = new Dictionary<string, string>();
            string voucher = oAC.SelectScalar<string>("select dbo.fnContractorEmployeeID(@typeID)", new object[] { "Voucher" });


            id.Add("voucherid", voucher);
            voucherId.Add(id);
            return Ok(voucherId);

        }

        /// <summary>
        /// Search
       /// </summary>
        /// <param name="VoucherID"></param>
        /// <param name="ContractorID"></param>
        /// <param name="Language"></param>
        /// <param name="Status"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("GetContractors")]
        [HttpGet]
        public IHttpActionResult GetContractorsList(string VoucherID, string ContractorID, string Language, string Status, string UserId)
        {
            try
            {
                object[] outParameters = null;
                string[] parameters = new string[] { "VoucherID", "ContractorID", "Language", "Status", "UserId" };
                DataTable dt = oAC.ExecuteStoredProcedure("C_GetContractorByEmployer", parameters,
                    new object[] { VoucherID, ContractorID, Language, Status, UserId }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// finish saving browser approval
        /// </summary>
        /// <param name="VoucherID"></param>
        /// <param name="updateDate"></param>
        /// <param name="idCard"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Route("Done")]
        [HttpGet]
        public IHttpActionResult Done(string VoucherID, string updateDate, [FromUri] string[] idCard, string status)
        {
            if (VoucherID.Contains("\"")) VoucherID = VoucherID.Replace("\"", "");
            try
            {
                if (status == "Q")
                {
                    foreach (var id in idCard)
                    {
                        oAC.ExecuteNonQuery(@"UPDATE Contractors 
                                              SET [Status] = @Status,
                                                  [Stamp] = @Stamp, 
                                                  [TrainDate] = @trainDate, 
                                                  [PreStatus] = @PreStatus 
                                              WHERE IdCard = @IdCard 
                                              AND VoucherID = @VoucherID"
                            , new object[] { status, DateTime.Now, updateDate, status, id, VoucherID });
                    }
                }
                else if (status == "N")
                {
                    string newVoucher = oAC.SelectScalar<string>("select dbo.fnContractorEmployeeID(@typeID)", new object[] { "Voucher" });

                    foreach (var id in idCard)
                    {
                        string[] arrID = id.Split('|');
                        oAC.ExecuteNonQuery(@"UPDATE Contractors 
                                              SET [Status] = @Status,
                                                  [Stamp] = @Stamp, 
                                                  [PreStatus] = @PreStatus,
                                                  [VoucherID]=@newVoucher,
                                                  [ReasonReturn]=@ReasonReturn
                                              WHERE IdCard = @IdCard 
                                              AND VoucherID = @VoucherID"
                            ,new object[] { status, DateTime.Now, status, newVoucher, arrID[1], arrID[0], VoucherID });
                    }
                }
                else if (status == "F")
                {
                    foreach (var id in idCard)
                    {
                        oAC.ExecuteNonQuery(@"UPDATE Contractors 
                                              SET [Status] = @Status,
                                                  [Stamp] = @Stamp, 
                                                  [PreStatus] = @PreStatus 
                                              WHERE IdCard = @IdCard 
                                              AND VoucherID = @VoucherID"
                            ,new object[] { status, DateTime.Now, status, id, VoucherID });
                    }
                }
                else
                {
                    foreach (var id in idCard)
                    {
                         oAC.ExecuteNonQuery(@"UPDATE Contractors 
                                              SET [Status] = @Status,
                                                  [Stamp] = @Stamp, 
                                                  AppointmentDate = @UpdateDate,
                                                  [PreStatus] = @PreStatus 
                                              WHERE IdCard = @IdCard 
                                              AND VoucherID = @VoucherID"
                            ,new object[] { status, DateTime.Now, updateDate, status, id, VoucherID });
                    }
                }
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
            return Ok();
        }

        [Route("GetContractorsInfo")]
        [HttpGet]
        public IHttpActionResult GetContractorsInfo(string contractorID)
        {
            Console.WriteLine("contractorID:" + contractorID);
            DataTable dt = oAC.DbHelper.ExecuteStoredProcedure("C_Get_ContractorEmployer",
                        new string[] { "contractorID" }, new object[] { contractorID }).Tables[0];
            return Ok(_helper.ConvertJson(dt));

        }

        /// <summary>
        /// Get a personal list of unconfirmed bidders for deletion
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="idCard"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Route("NotConfirm")]
        [HttpPut]
        public HttpResponseMessage NotConfirm(string voucherID, [FromUri] string[] idCard, string status)
        {
            try
            {
                if (status == "X")
                {
                    foreach (var id in idCard)
                    {
                        oAC.ExecuteNonQuery(@"UPDATE Contractors 
                                            SET [Status] = @Status,
                                                [Stamp] = @Stamp,
                                                [PreStatus]= @PreStatus 
                                            WHERE IdCard = @IdCard 
                                            AND VoucherID = @VoucherID"
                            , new object[] { status, DateTime.Now, status, id, voucherID });
                    }
                }

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message + e.StackTrace);
            }
        }

        /// <summary>
        /// Send email when confirming entry date
        /// </summary>
        /// <param name="ContractorID"></param>
        /// <returns></returns>
        [Route("ConfirmdMail")]
        [HttpPost]
        public IHttpActionResult ConfirmdMail(string ContractorID)
        {

            var entity = Db.Contractors.First(x => x.ContractorID == ContractorID);
            if (entity.Status == "S")
            {
                oAC.ExecuteStoredProcedureNonQuery("C_@Daily_SendNoticeExpired"
                    , new string[] { "ContractorID", "FromUser", "Kinds", "FormKey", "Mailkind"}
                    , new object[] { ContractorID, null, null, null, "c_Appointment" });
            }

            Db.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Send mail when suspension of contractors
        /// </summary>
        /// <param name="IdCard"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        [Route("SuspendedMail")]
        [HttpPost]
        public IHttpActionResult SuspendedMailContractor(string IdCard, string Reason)
        {
            
                var entity = Db.Contractors.First(x => x.IdCard == IdCard);
                if (entity.Status == "C" || entity.Status == "SC")
                {
                    oAC.ExecuteStoredProcedureNonQuery("C_@Daily_ContractorNoticeSuspended"
                        , new string[] { "IdCard", "FromUser", "Kinds", "FormKey", "Mailkind", "Reason" }
                        , new object[] { IdCard, null, null, null, "c_suspended", Reason });
                }
                else if (entity.Status == "PC")
                {
                    oAC.ExecuteStoredProcedureNonQuery("C_@Daily_ContractorNoticeSuspended"
                        , new string[] { "IdCard", "FromUser", "Kinds", "FormKey", "Mailkind", "Reason" }
                        , new object[] { IdCard, null, null, null, "c_suspendedTemporary", Reason });
                }
                else
                {
                    oAC.ExecuteStoredProcedureNonQuery("C_@Daily_ContractorNoticeSuspended"
                        , new string[] { "IdCard", "FromUser", "Kinds", "FormKey", "Mailkind", "Reason" }
                        , new object[] { IdCard, null, null, null, "c_Cancelsuspended", null });
                }

                Db.SaveChanges();
                return Ok();
            

        }

        /// <summary>
        /// cancellation of individual suspension of contractors
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="idCard"></param>
        /// <param name="enddate"></param>
        /// <param name="employeeID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Route("CancelSuspendEmployee")]
        [HttpPut]
        public HttpResponseMessage CancelSuspendEmployee(string voucherID, string idCard, string enddate, string employeeID, string status)
        {
            try
            {
                if (Convert.ToDateTime(enddate) >= DateTime.Now)
                {
                    oAC.ExecuteNonQuery(@"UPDATE Contractors
                                        SET [Status] = @status,
                                            [Stamp] = @Stamp,
                                            [EnddateCancel] = @EnddateCancel
                                        WHERE IdCard = @IdCard
                                        AND VoucherID=@VoucherID"
                        ,new object[] { 'W', DateTime.Now, DateTime.Now, idCard, voucherID });

                    if (status == "PC" || status == "C")
                    {
                        oAC.ExecuteNonQuery(@"UPDATE ContractorBiometric 
                                            SET IsValid=1 
                                            WHERE EmployeeID = @EmployeeID"
                            ,new object[] { employeeID });

                    }
                }
                else
                {
                    oAC.ExecuteNonQuery(@"UPDATE Contractors
                                        SET [Status] = @status,
                                            [Stamp] = @Stamp,
                                            [EnddateCancel] = @EnddateCancel
                                        WHERE IdCard = @IdCard
                                        AND VoucherID=@VoucherID"
                        ,new object[] { 'E', DateTime.Now, DateTime.Now, idCard, voucherID });
                }
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message + e.StackTrace);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
