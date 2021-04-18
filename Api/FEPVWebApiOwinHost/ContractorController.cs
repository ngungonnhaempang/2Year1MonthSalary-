using FEPVWebApiOwinHost.Filter;
using FEPVWebApiOwinHost.Models;
using log4net;
using Microsoft.Office.Core;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OfficeExcel = Microsoft.Office.Interop.Excel;

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
                       , new object[] { status, DateTime.Now, status, idCard, voucherID });

                }
                else
                {
                    oAC.ExecuteNonQuery(@"UPDATE Contractors
                                            SET [Status] = @Status,
                                            [Stamp] = @Stamp
                                            WHERE IdCard = @IdCard
                                            AND VoucherID = @VoucherID"
                       , new object[] { status, DateTime.Now, idCard, voucherID });

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


        [Route("ContractorSaveStatusSuspend")]
        [HttpPut]
        public HttpResponseMessage ContractorSaveStatusSuspend(string voucherID, string status, string stardateCancel, string enddateCancel, string employeeID, string suspendReason, string suspendUser, string suspendDept)
        {
            try
            {
                oAC.ExecuteStoredProcedure("C_Suspend"
                            , new string[] { "EmployeeID", "VoucherID", "status", "StartdateCancel", "EnddateCancel", "reason", "SuspendUser", "SuspendDept" }
                            , new object[] { employeeID, voucherID, status, stardateCancel, enddateCancel, suspendReason, suspendUser, suspendDept });

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

        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(EmployeeList EmpList)
        {
            Console.WriteLine("Save");
            try
            {
                foreach (var contractortoDb in EmpList._employeeList)
                {
                    var contractorInDb = Db.Contractors.Find(contractortoDb.EmployeeID, contractortoDb.ContractorID);

                    if (contractorInDb != null)
                    {
                        contractorInDb.IdCard = contractortoDb.IdCard;
                        contractorInDb.ContractorID = contractortoDb.ContractorID;
                        contractorInDb.VoucherID = contractorInDb.VoucherID;
                        contractorInDb.Name = contractortoDb.Name;
                        contractorInDb.Mark = contractortoDb.Mark;
                        contractorInDb.Birthday = contractortoDb.Birthday;
                        contractorInDb.Sex = contractortoDb.Sex;
                        contractorInDb.Job = contractortoDb.Job;
                        contractorInDb.Region = contractortoDb.Region;
                        contractorInDb.Remark = contractortoDb.Remark;
                        contractorInDb.Phone = contractortoDb.Phone;

                        if (contractortoDb.Status == "W")
                        {
                            contractorInDb.IsUpload = 0;
                        }

                        if (contractortoDb.Status != "P")
                        {
                            contractorInDb.PreStatus = contractortoDb.Status;
                        }

                        if (contractortoDb.Status == "N" || contractortoDb.Status == "W" || contractortoDb.Status == "P" || contractortoDb.Status == "RT")
                        {
                            contractorInDb.Status = contractortoDb.Status;
                        }
                        else
                        {
                            contractorInDb.Status = contractortoDb.IsUpdate ? "Q" : "F";
                        }

                        contractorInDb.Stamp = DateTime.Now;
                        contractorInDb.UserId = contractorInDb.UserId;
                        contractorInDb.EmployeeID = contractorInDb.EmployeeID;
                        contractorInDb.ReasonReturn = null;
                        contractorInDb.InsuranceDuration = contractortoDb.InsuranceDuration;
                        contractorInDb.SafetyCerDuration = contractortoDb.SafetyCerDuration;
                        contractorInDb.IsForeign = contractortoDb.IsForeign;

                        if (contractortoDb.IsForeign == 1)
                        {
                            var Foreigner = Db.ContractorForeigners.Find(contractortoDb.EmployeeID);
                            if (Foreigner != null)
                            {
                                Foreigner.PassPort_Expiry = contractortoDb.PassPort_Expiry;
                                Foreigner.PassPort_Nationality = contractortoDb.PassPort_Nationality;
                                Foreigner.WorkPermit_No = contractortoDb.WorkPermit_No;
                                Foreigner.WorkPermit_Start = contractortoDb.WorkPermit_Start;
                                Foreigner.WorkPermit_End = contractortoDb.WorkPermit_End;
                                Foreigner.CategoryCard = contractortoDb.CategoryCard;
                                Foreigner.Card_Type = contractortoDb.Card_Type;
                                Foreigner.Card_No = contractortoDb.Card_No;
                                Foreigner.Card_Start = contractortoDb.Card_Start;
                                Foreigner.Card_End = contractortoDb.Card_End;

                                Db.Entry(Foreigner).State = EntityState.Modified;
                            }
                            else
                            {
                                var _Foreigner = new ContractorForeigner
                                {
                                    EmployeeID = contractortoDb.EmployeeID,
                                    PassPort_Expiry = contractortoDb.PassPort_Expiry,
                                    PassPort_Nationality = contractortoDb.PassPort_Nationality,
                                    WorkPermit_No = contractortoDb.WorkPermit_No,
                                    WorkPermit_Start = contractortoDb.WorkPermit_Start,
                                    WorkPermit_End = contractortoDb.WorkPermit_End,
                                    CategoryCard = contractortoDb.CategoryCard,
                                    Card_Type = contractortoDb.Card_Type,
                                    Card_No = contractortoDb.Card_No,
                                    Card_Start = contractortoDb.Card_Start,
                                    Card_End = contractortoDb.Card_End
                                };

                                Db.ContractorForeigners.Add(_Foreigner);
                            }
                        }
                        else
                        {
                            var Foreigner = Db.ContractorForeigners.Find(contractortoDb.EmployeeID);
                            if (Foreigner != null)
                            {
                                Db.Entry(Foreigner).State = EntityState.Deleted;
                            }
                        }

                        if (contractortoDb.FileName != null)
                        {
                            var File = Db.ContractorEmployeeFiles.Find(contractortoDb.VoucherID);
                            if (File != null)
                            {
                                File.VoucherID = contractortoDb.VoucherID;
                                File.FileName = contractortoDb.FileName;
                                File.Stamp = DateTime.Now;
                                File.CreateTime = File.CreateTime;
                                Db.Entry(File).State = EntityState.Modified;
                            }
                            else
                            {
                                var _file = new ContractorEmployeeFile
                                {
                                    VoucherID = contractorInDb.VoucherID,
                                    FileName = contractortoDb.FileName,
                                    Stamp = DateTime.Now,
                                    CreateTime = DateTime.Now
                                };

                                Db.ContractorEmployeeFiles.Add(_file);
                            }
                        }
                        else
                        {
                            var File = Db.ContractorEmployeeFiles.Find(contractortoDb.VoucherID);
                            if (File != null)
                            {
                                Db.Entry(File).State = EntityState.Deleted;
                            }
                        }

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
                            Mark = contractortoDb.Mark,
                            Birthday = contractortoDb.Birthday,
                            Sex = contractortoDb.Sex,
                            Job = contractortoDb.Job,
                            Region = contractortoDb.Region,
                            Remark = contractortoDb.Remark,
                            Status = contractortoDb.Status,
                            PreStatus = contractortoDb.Status,
                            Stamp = DateTime.Now,
                            UserId = contractortoDb.UserId,
                            InsuranceDuration = contractortoDb.InsuranceDuration,
                            SafetyCerDuration = contractortoDb.SafetyCerDuration,
                            IsForeign = contractortoDb.IsForeign,
                            Phone = contractortoDb.Phone
                        };

                        Db.Contractors.Add(contractorInDb);

                        var _biometric = new ContractorBiometric
                        {
                            EmployeeID = contractorInDb.EmployeeID,
                            CardNo = null,
                            FaceTmp = null
                        };

                        Db.ContractorBiometrics.Add(_biometric);

                        if (contractortoDb.IsForeign == 1)
                        {
                            var _Foreigner = new ContractorForeigner
                            {
                                EmployeeID = contractorInDb.EmployeeID,
                                PassPort_Expiry = contractortoDb.PassPort_Expiry,
                                PassPort_Nationality = contractortoDb.PassPort_Nationality,
                                WorkPermit_No = contractortoDb.WorkPermit_No,
                                WorkPermit_Start = contractortoDb.WorkPermit_Start,
                                WorkPermit_End = contractortoDb.WorkPermit_End,
                                CategoryCard = contractortoDb.CategoryCard,
                                Card_Type = contractortoDb.Card_Type,
                                Card_No = contractortoDb.Card_No,
                                Card_Start = contractortoDb.Card_Start,
                                Card_End = contractortoDb.Card_End
                            };

                            Db.ContractorForeigners.Add(_Foreigner);
                        }

                        if (contractortoDb.FileName != null)
                        {
                            var File = Db.ContractorEmployeeFiles.Find(contractortoDb.VoucherID);
                            if (File == null)
                            {
                                var _file = new ContractorEmployeeFile
                                {
                                    VoucherID = contractorInDb.VoucherID,
                                    FileName = contractortoDb.FileName,
                                    Stamp = DateTime.Now,
                                    CreateTime = DateTime.Now
                                };

                                Db.ContractorEmployeeFiles.Add(_file);
                            }
                        }


                    }
                    Db.SaveChanges();
                }

                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                Console.WriteLine(e.Message);
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
        [Route("GetContractors")]
        [HttpGet]
        public IHttpActionResult GetContractorsList(string VoucherID, string ContractorID, string Language, string Status, string UserId, string Name, string IdCard, string Region, string Fromdate, string Todate, string Classify)
        {
            try
            {
                string[] parameters = new string[] { "VoucherID", "ContractorID", "Language", "Status", "UserId", "Name", "IdCard", "Region", "Fromdate", "Todate", "Classify" };
                DataTable dt = oAC.ExecuteStoredProcedure("C_GetContractorByEmployer", parameters,
                new object[] { VoucherID, ContractorID, Language, Status, UserId, Name, IdCard, Region, Fromdate, Todate, Classify }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// finish process
        /// </summary>
        [Route("Done")]
        [HttpGet]
        public IHttpActionResult Done(string VoucherID, string updateDate, [FromUri] string[] idCard, string status)
        {
            string newVoucher = "";
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
                else if (status == "M" || status == "RT")
                {
                    if (status == "RT")
                    {
                        newVoucher = oAC.SelectScalar<string>("select dbo.fnContractorEmployeeID(@typeID)", new object[] { "Voucher" });
                    }
                    else
                    {
                        newVoucher = VoucherID;
                        status = "F";
                    }

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
                            , new object[] { status, DateTime.Now, status, newVoucher, arrID[1], arrID[0], VoucherID });
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
                            , new object[] { status, DateTime.Now, status, id, VoucherID });
                    }
                }
                else if (status == "training")
                {
                    foreach (var id in idCard)
                    {
                        oAC.ExecuteNonQuery(@"UPDATE Contractors 
                                              SET [Status] = @Status,
                                                  [Stamp] = @Stamp, 
                                                  TrainDate = @UpdateDate,
                                                  Mark = @mark,
                                                  [PreStatus] = @PreStatus 
                                              WHERE EmployeeID = @EmployeeID
                                              AND ContractorID = @ContractorID"
                          , new object[] { 'Q', DateTime.Now, updateDate, null, 'Q', id, VoucherID });

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
                                              WHERE EmployeeID = @EmployeeID
                                              AND ContractorID = @ContractorID"
                           , new object[] { status, DateTime.Now, updateDate, status, id, VoucherID });
                    }
                }
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }

            JsonObject json = new JsonObject();
            json["newVoucher"] = newVoucher;

            return Ok(json);

        }

        [Route("GetContractorsInfoByDept")]
        [HttpGet]
        public IHttpActionResult GetContractorsInfoByDept(string contractorName, string cType, string language, string departmentID, string status, string userid)
        {
            try
            {
                DataTable dt = oAC.DbHelper.ExecuteStoredProcedure("C_GetConQuaByEmployer"
                                       , new string[] { "Employer", "CType", "DepartmentID", "Language", "Status", "UserId" }
                                       , new object[] { contractorName, cType, departmentID, language, status, userid }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        /// <summary>
        /// Get a personal list of unconfirmed for deletion
        /// </summary>
        [Route("NotConfirm")]
        [HttpPut]
        public HttpResponseMessage NotConfirm(string ContractorID, [FromUri] string[] empID, string status)
        {
            try
            {
                if (status == "X")
                {
                    foreach (var id in empID)
                    {
                        oAC.ExecuteNonQuery(@"UPDATE Contractors 
                                            SET [Status] = @Status,
                                                [Stamp] = @Stamp,
                                                [PreStatus]= @PreStatus 
                                            WHERE EmployeeID = @EmployeeID 
                                            AND ContractorID = @ContractorID"
                            , new object[] { status, DateTime.Now, status, id, ContractorID });
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
        /// Send mail when suspension of contractors
        /// </summary>

        [Route("SuspendedMail")]
        [HttpPost]
        public IHttpActionResult SuspendedMailContractor(string EmployeeID, string mailKind)
        {
            try
            {
                oAC.ExecuteStoredProcedureNonQuery("C_@Daily_ContractorNoticeSuspended"
                    , new string[] { "EmployeeID", "Mailkind" }
                    , new object[] { EmployeeID, mailKind });

                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }

        }


        /// <summary>
        /// cancellation of individual suspension of contractors
        /// </summary>
        [Route("CancelSuspendEmployee")]
        [HttpPut]
        public HttpResponseMessage CancelSuspendEmployee(string voucherID, string idCard, string enddate, string employeeID, string status, string cancelUser)
        {
            try
            {
                if (Convert.ToDateTime(enddate) >= DateTime.Now)
                {
                    oAC.ExecuteNonQuery(@"UPDATE Contractors
                                        SET [Status] = @status,
                                            [Stamp] = @Stamp,
                                            [EnddateCancel] = @EnddateCancel,
                                            CancelSuspendUser = @cancelUser
                                        WHERE EmployeeID = @employeeID
                                        AND VoucherID=@VoucherID"
                        , new object[] { 'W', DateTime.Now, DateTime.Now, cancelUser, employeeID, voucherID });

                    if (status != "SC")
                    {
                        oAC.ExecuteNonQuery(@"UPDATE Contractors
                                        SET IsUpload = 0,
                                            IsDelete = null
                                        WHERE EmployeeID = @employeeID
                                        AND VoucherID=@VoucherID"
                        , new object[] { employeeID, voucherID });

                    }
                }
                else
                {
                    oAC.ExecuteNonQuery(@"UPDATE Contractors
                                        SET [Status] = @status,
                                            [Stamp] = @Stamp,
                                            [EnddateCancel] = @EnddateCancel,
                                            CancelSuspendUser = @cancelUser
                                        WHERE EmployeeID = @employeeID
                                        AND VoucherID=@VoucherID"
                        , new object[] { 'E', DateTime.Now, DateTime.Now, cancelUser, employeeID, voucherID });
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

        [Route("GetContractorInfo")]
        [HttpGet]
        public IHttpActionResult getContractorInfoByEmployeeID(string employeeId)
        {
            try
            {
                DataTable dtContractor = oAC.DbHelper.ExecuteStoredProcedure("HS_Q_Contractor_Image",
                new string[] { "UserID", "IdCard", "ContractorID", "EmployeeID" }, new object[] { "", "", "", employeeId }).Tables[0];
                var imagePath = Convert.ToBase64String((byte[])dtContractor.Rows[0]["Image"]);
                dtContractor.Columns.Add("stringImage", typeof(string));

                dtContractor.Rows[0]["stringImage"] = imagePath;
                return Ok(_helper.ConvertJson(dtContractor));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Count Contractor
        /// </summary>
        [Route("CountContractor")]
        [HttpGet]
        public IHttpActionResult CountContractor(string ContractorID)
        {
            try
            {
                DataTable dt = oAC.DbHelper.ExecuteStoredProcedure("C_CountContractor",
                        new string[] { "ContractorID" }, new object[] { ContractorID }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Send email when confirming entry date
        /// </summary>
        [Route("ConfirmdMail")]
        [HttpPost]
        public IHttpActionResult ConfirmdMail(string ContractorID, string VoucherID, string Mailkind, string Datetime, string ContractorName)
        {
            try
            {
                string path = "";
                if (Mailkind == "c_Appointment")
                {
                    // string folderPath = ConfigurationManager.AppSettings["pathExcelContractor"];

                    DataSet dsData = GetDataSet(ContractorID, Datetime);
                    path = ExportDataSetToExcel(dsData, "D:\\ContractorFile\\", Datetime, ContractorName);
                }

                oAC.ExecuteStoredProcedureNonQuery("[C_@Daily_SendNoticeConfirm]"
                , new string[] { "ContractorID", "VoucherID", "Mailkind", "Datetime", "pathExcel" }
                , new object[] { ContractorID, VoucherID, Mailkind, Datetime, path });

                if (File.Exists(path))
                {
                    Console.WriteLine("delete excel");
                    File.Delete(path);
                }

                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        private DataSet GetDataSet(string ContractorID, string AppointmentDate)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dtLog = new DataTable();
                dtLog.Columns.Add("CMND 身份證號碼");
                dtLog.Columns.Add("HỌ TÊN CÔNG NHÂN 工人姓名");
                dtLog.Columns.Add("GIỚI TÍNH 性別");
                dtLog.Columns.Add("NGÀY SINH 出生日期");
                dtLog.Columns.Add("CHỨC VỤ 職別");
                dtLog.Columns.Add("SỐ TIỀN 金額");
                dtLog.Columns.Add("CHỮ KÝ 簽名");

                var drLog = oAC.DbHelper.ExecuteStoredProcedure("C_ExportExcelCardPrice", new string[] { "ContractorID", "AppointmentDate" }, new object[] { ContractorID, AppointmentDate }).Tables[0];

                foreach (DataRow dr in drLog.Rows)
                {
                    dtLog.Rows.Add(dr.ItemArray);
                }

                dtLog.TableName = "Nhà thầu 承包商";
                var dtcopy = dtLog.Copy();
                ds.Tables.Add(dtcopy);

                return ds;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        string ExportDataSetToExcel(DataSet ds, string strPath, string AppointmentDate, string ContractorName)
        {
            try
            {
                string pathImg = strPath + "Logo.png";
                int inHeaderLength = 9, inColumn = 0, inRow = 0;
                System.Reflection.Missing Default = System.Reflection.Missing.Value;
                //Create Excel File
                strPath += ContractorName + "_" + AppointmentDate + ".xlsx";
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                OfficeExcel.Application excelApp = new OfficeExcel.Application();
                OfficeExcel.Workbook excelWorkBook = excelApp.Workbooks.Add(1);

                foreach (DataTable dtbl in ds.Tables)
                {
                    //Create Excel WorkSheet
                    OfficeExcel.Worksheet excelWorkSheet = (OfficeExcel.Worksheet)excelWorkBook.Sheets["Sheet1"];

                    excelWorkSheet.Name = dtbl.TableName;

                    //Insert logo company
                    var filePath = pathImg;
                    if (!File.Exists(filePath))
                    {
                        Resource1.Logo.Save(filePath);
                    }

                    excelWorkSheet.Shapes.AddPicture(filePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0, 200, 45);

                    //Write Column Name
                    for (int i = 0; i < dtbl.Columns.Count; i++)
                        excelWorkSheet.Cells[inHeaderLength, i + 1] = dtbl.Columns[i].ColumnName.ToUpper();

                    //Write Rows
                    for (int m = 0; m < dtbl.Rows.Count; m++)
                    {
                        for (int n = 0; n < dtbl.Columns.Count; n++)
                        {
                            inColumn = n + 1;
                            inRow = inHeaderLength + 1 + m;
                            excelWorkSheet.Cells[inRow, inColumn] = dtbl.Rows[m].ItemArray[n].ToString();
                        }
                    }

                    int EndRow = dtbl.Rows.Count + inHeaderLength;

                    excelWorkSheet.get_Range("A10", "G" + EndRow).HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignLeft;
                    excelWorkSheet.get_Range("A1", "G" + EndRow).Cells.Font.Name = "Times New Roman";

                    OfficeExcel.Range cellrange = excelWorkSheet.get_Range("A9", "G" + EndRow);
                    cellrange.Borders.LineStyle = OfficeExcel.XlLineStyle.xlContinuous;
                    cellrange.Borders.Weight = OfficeExcel.XlBorderWeight.xlThin;
                    cellrange.VerticalAlignment = OfficeExcel.XlVAlign.xlVAlignCenter;

                    //Excel Header
                    OfficeExcel.Range cellHeaderVN = excelWorkSheet.get_Range("A1", "G3");
                    cellHeaderVN.Merge(false);
                    cellHeaderVN.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;
                    cellHeaderVN.Font.Bold = OfficeExcel.XlBorderWeight.xlMedium;
                    cellHeaderVN.Font.Size = 18;
                    excelWorkSheet.Cells[1, 1] = "Phiếu đóng tiền làm thẻ cho công nhân nhà thầu ";

                    OfficeExcel.Range cellHeaderTW = excelWorkSheet.get_Range("A4", "G4");
                    cellHeaderTW.Merge(false);
                    cellHeaderTW.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;
                    cellHeaderTW.Font.Bold = OfficeExcel.XlBorderWeight.xlMedium;
                    cellHeaderTW.Font.Size = 18;
                    excelWorkSheet.Cells[4, 1] = "承包商工人建卡付款表";

                    //Contractor Name
                    OfficeExcel.Range cellContractorName = excelWorkSheet.get_Range("A6", "G6");
                    cellContractorName.Merge(false);
                    cellContractorName.Font.Bold = OfficeExcel.XlBorderWeight.xlMedium;
                    excelWorkSheet.Cells[6, 1] = "Tên nhà thầu 承包商名稱: " + ContractorName;

                    // export report date
                    OfficeExcel.Range cellHeaderAppDate = excelWorkSheet.get_Range("A7", "G7");
                    cellHeaderAppDate.Merge(false);
                    excelWorkSheet.Cells[7, 1] = "Ngày hẹn đăng ký thẻ 預計登記卡日期: " + AppointmentDate;

                    //Style table column names
                    OfficeExcel.Range cellHeader = excelWorkSheet.get_Range("A9", "G9");
                    cellHeader.Font.Bold = true;
                    cellHeader.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C5D9F1");
                    cellHeader.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;

                    //Total Price
                    OfficeExcel.Range cellTotalPrice = excelWorkSheet.get_Range("E" + EndRow, "F" + EndRow);
                    cellTotalPrice.Font.Color = System.Drawing.Color.Red;
                    cellTotalPrice.Font.Bold = true;

                    //Auto fit columns
                    excelWorkSheet.Columns.AutoFit();
                    excelWorkSheet.get_Range("G:G", System.Type.Missing).EntireColumn.ColumnWidth = 27;

                }

            //Set Defualt Page
            (excelWorkBook.Sheets[1] as OfficeExcel._Worksheet).Activate();

                excelWorkBook.SaveAs(strPath);
                excelWorkBook.Close();
                excelApp.Quit();
                return strPath;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }

        }

        [Route("UpdateMark")]
        [HttpGet]
        public IHttpActionResult UpdateMark(string ContractorID, string EmpID, string mark)
        {
            try
            {
                oAC.ExecuteNonQuery(@"UPDATE Contractors 
                                    SET Mark = @mark,
                                        [Stamp] = @Stamp
                                    WHERE EmployeeID = @EmployeeID
                                    AND ContractorID = @ContractorID"
                       , new object[] { mark, DateTime.Now, EmpID, ContractorID });

            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
            return Ok();
        }

        [Route("GetRole")]
        [HttpGet]
        public IHttpActionResult GetRole(string UserID)
        {
            try
            {
                var dtContractor = oAC.DbHelper.Select(@"SELECT Dept
                                                      FROM ContractorRole
                                                      WHERE UserID = @UserID
                                                      AND IsActive = 1"
                           , new object[] { UserID }).Tables[0];
                return Ok(_helper.ConvertJson(dtContractor));
            }

            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
