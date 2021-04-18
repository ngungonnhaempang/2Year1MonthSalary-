using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using FEPVWebApiOwinHost.Models;
using log4net;
using RestSharp;
using FEPVWebApiOwinHost.Filter;
using System.IO;
using System.Web.Configuration;
using OfficeExcel = Microsoft.Office.Interop.Excel;
using System.Configuration;
using Microsoft.Office.Core;


namespace FEPVWebApiOwinHost
{

    [FilterIP]
    [RoutePrefix("api/Gate/ConQua")]
    public class ContractorInfoController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("Beling");
        protected readonly ContractorContext Db = new ContractorContext();
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        [Route("GetContractorKind")]
        public IHttpActionResult GetContractorKind(string kind, string language)
        {
            try
            {
                DataTable dt = gate.ExecuteStoredProcedure("C_Get_ContractorPara"
                                    , new string[] { "Kind", "Language" }
                                    , new object[] { kind, language }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = e.Message,
                    Content = new StringContent(e.Message)
                };
                Loger.Error(e);
                throw new HttpResponseException(resp);
            }
        }

        [Route("GetContractorRegion")]
        public IHttpActionResult GetContractorRegion(string language)
        {
            try
            {
                DataTable dt = gate.ExecuteStoredProcedure("C_Get_ContractorRegion"
                                    , new string[] { "Language" }
                                    , new object[] { language }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = e.Message,
                    Content = new StringContent(e.Message)
                };
                Loger.Error(e);
                throw new HttpResponseException(resp);
            }
        }

        [Route("GetContractorJob")]
        public IHttpActionResult GetContractorJob(string language)
        {
            try
            {
                DataTable dt = gate.ExecuteStoredProcedure("C_Get_ContractorJob"
                                    , new string[] { "Language" }
                                    , new object[] { language }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = e.Message,
                    Content = new StringContent(e.Message)
                };
                Loger.Error(e);
                throw new HttpResponseException(resp);
            }
        }

        [Route("GetContractorDepartment")]
        public IHttpActionResult GetContractorDepartment(string language)
        {
            try
            {
                DataTable dt = gate.ExecuteStoredProcedure("[C_Get_ContractorDepartMent]"
                                    , new string[] { "Language" }
                                    , new object[] { language }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = e.Message,
                    Content = new StringContent(e.Message)
                };
                Loger.Error(e);
                throw new HttpResponseException(resp);
            }
        }

        /// <summary>
        /// Find by ID
        /// <summary>
        [Route("Get")]
        [HttpGet]
        public IHttpActionResult GetContractorQualification(string contractorID, string language)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetConQuaByEmployerId"
                                       , new string[] { "ContractorID", "Language" }
                                       , new object[] { contractorID, language }).Tables[0];
                Console.WriteLine(dt.Rows.Count);
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }


        [Route("GetContractorUpdate")]
        [HttpGet]
        public IHttpActionResult GetContractorUpdate(string VoucherID, string IdCard, string language)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetContractorUpdate"
                                      , new string[] { "VoucherID", "IdCard", "Language" }
                                      , new object[] { VoucherID, IdCard, language }).Tables[0];
                Console.WriteLine(dt.Rows.Count);
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }


        [Route("GetContractorCheck")]
        [HttpGet]
        public IHttpActionResult GetContractorCheck(string voucherID, string language)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetContractorCheck"
                                       , new string[] { "VoucherID", "Language" }
                                       , new object[] { voucherID, language }).Tables[0];
                Console.WriteLine(dt.Rows.Count);
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }


        [Route("GetContractorConfirm")]
        [HttpGet]
        public IHttpActionResult GetContractorConfirm(string ContractorID, string language)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetContractorConfirm"
                , new string[] { "ContractorID", "Language" }
                , new object[] { ContractorID, language }).Tables[0];
                Console.WriteLine(dt.Rows.Count);
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }


        /// <summary>
        /// Search
        /// <summary>
        [Route("Get")]
        [HttpGet]
        public IHttpActionResult GetContractorQualification(string contractorName, string cType, string language, string departmentID, string status, string userid)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetConQuaByEmployer"
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


        [Route("Get")]
        [HttpGet]
        public IHttpActionResult GetById(string contractorID)
        {
            try
            {
                var contractorQualification = Db.ContractorQualifications.Find(contractorID);
                return Ok(contractorQualification);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        /// <summary>
        /// check duplicate contractor's name 
        /// </summary>
        [Route("Get")]
        [HttpGet]
        public IHttpActionResult GetByname(string contractorID, string contractorName)
        {
            try
            {
                var cq = new ContractorQualification();

                if (!string.IsNullOrEmpty(contractorID))
                {
                    var contractor = Db.ContractorQualifications.Find(contractorID);
                    if (contractor != null)
                    {
                        if (contractor.ContractorName != contractorName)
                        {
                            cq.ContractorID = CheckContractorName(contractorName);
                        }
                    }
                    else
                    {
                        return BadRequest("Contractor not exist");
                    }
                }
                else
                {
                    cq.ContractorID = CheckContractorName(contractorName);
                }
                return Ok(cq);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        private string CheckContractorName(string contractorName)
        {
            try
            {
                string _contractorID = null;
                var contractorQualification = Db.ContractorQualifications.Where(c => c.ContractorName == contractorName).OrderByDescending(c => c.Stamp).ToList();
                if (contractorQualification.Count > 0)
                {
                    var valid = contractorQualification.FindAll(c => c.isvalid == 1).ToList();
                    if (valid.Count >= 1)
                    {
                        var contractorQua = Db.ContractorQualifications.Find(contractorQualification[0].ContractorID);
                        _contractorID = contractorQua.ContractorID;

                    }
                }
                return _contractorID;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return e.Message + e.StackTrace;
            }
        }

        private string UpdateContractorInfo(ContractorQualification cq, out string contractorID)
        {
            contractorID = cq.ContractorID.ToString();
            try
            {
                var _ContractorQualification = Db.ContractorQualifications.Find(cq.ContractorID);
                if (_ContractorQualification != null)
                {
                    _ContractorQualification.ContractorID = cq.ContractorID;
                    _ContractorQualification.ContractorName = cq.ContractorName.ToUpper();
                    _ContractorQualification.CType = cq.CType;
                    _ContractorQualification.isvalid = cq.isvalid;
                    _ContractorQualification.Remark = cq.Remark;
                    _ContractorQualification.DepartmentID = cq.DepartmentID;
                    _ContractorQualification.UserID = cq.UserID;
                    _ContractorQualification.AccDate = cq.AccDate;
                    _ContractorQualification.Status = cq.Status;
                    _ContractorQualification.Rcode = cq.Rcode;
                    _ContractorQualification.Type = cq.Type;
                    _ContractorQualification.Stamp = DateTime.Now;
                    _ContractorQualification.Region = cq.Region;
                    _ContractorQualification.StartDate = cq.StartDate;
                    _ContractorQualification.EndDate = cq.EndDate;
                    _ContractorQualification.ContractorByEmloyee = cq.ContractorByEmloyee;
                    _ContractorQualification.InternalNumber = cq.InternalNumber;
                    _ContractorQualification.Email = cq.Email;
                    _ContractorQualification.ContractorFile = cq.ContractorFile;
                    _ContractorQualification.EmailContractor = cq.EmailContractor;
                    _ContractorQualification.EmailSafetyContractor = cq.EmailSafetyContractor;

                    Db.Entry(_ContractorQualification).State = EntityState.Modified;
                }
                else
                {
                    return "ContractorID is not exist!";
                }
                Db.SaveChanges();
                JsonObject json = new JsonObject();
                json["contractorID"] = cq.ContractorID;
                contractorID = json.ToString();
                return "";
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return e.Message + e.StackTrace;
            }
        }

        private string CreateContractorInfo(ContractorQualification cq, out string contractorID)
        {
            contractorID = string.Empty;
            Console.WriteLine("Create a new contractor：" + cq.ContractorName + " _ " + DateTime.Now.ToString());
            try
            {
                var Contractors = Db.ContractorQualifications.Where(c => c.ContractorName == cq.ContractorName.ToUpper() && c.isvalid == 1).ToList();
                if (Contractors.Count <= 0)
                {
                    cq.ContractorID = gate.SelectScalar<string>("select dbo.fnContractorEmployeeID(@typeID)", new object[] { "Contractor" });
                    cq.ContractorName = cq.ContractorName.ToUpper();
                    cq.Stamp = DateTime.Now;
                    cq.Status = "N";
                    Db.ContractorQualifications.Add(cq);
                }
                else
                {
                    return string.Format("{0} is exsit", cq.ContractorName);
                }
                Db.SaveChanges();
                JsonObject json = new JsonObject();
                json["contractorID"] = cq.ContractorID;
                contractorID = json.ToString();
                return "";
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return e.Message + e.StackTrace;
            }
        }

        [Route("Get")]
        [HttpPost]
        public IHttpActionResult Save(ContractorQualification cq)
        {
            if (string.IsNullOrEmpty(cq.ContractorID))
            {
                string contractorID = "";
                string errmsg = CreateContractorInfo(cq, out contractorID);
                if (string.IsNullOrEmpty(errmsg))
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    JsonObject json = new JsonObject();
                    json["contractorID"] = contractorID;
                    return Ok(json);
                }
                else
                {
                    return BadRequest(errmsg);
                }
            }
            else
            {
                string contractorID = cq.ContractorID;
                string errmsg = UpdateContractorInfo(cq, out contractorID);
                if (string.IsNullOrEmpty(errmsg))
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    JsonObject json = new JsonObject();
                    json["contractorID"] = contractorID;

                    return Ok(json);
                }
                else
                {
                    return BadRequest(errmsg);
                }
            }
        }


        [Route("ConQuaSaveStatus")]
        [HttpPut]
        public HttpResponseMessage ConQuaSaveStatus(string employerId, string status)
        {
            string contractorID = employerId;
            if (string.IsNullOrEmpty(contractorID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "ContractorID is empty！");
            try
            {
                if (status != "X")
                {
                    gate.ExecuteNonQuery(@"UPDATE ContractorQualification
                                            SET [Status] = @Status,
                                            [Stamp] = @Stamp
                                            WHERE ContractorID = @ContractorID"
                       , new object[] { status, DateTime.Now, contractorID });
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
        /// Change status when canceling contractor
        /// </summary>
        [Route("ConQuaSaveStatusSuspend")]
        [HttpPut]
        public HttpResponseMessage ConQuaSaveStatusSuspended(string status, string contractorID, string stardateCancel, string enddateCancel, string suspendReason, string suspendUser)
        {
            if (string.IsNullOrEmpty(contractorID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "ContractorID is empty！");
            try
            {
                if (status == "PC")
                {
                    gate.ExecuteStoredProcedure("C_DeleteContractor"
                                   , new string[] { "status", "ContractorID", "startDate", "endDate", "reason", "user" }
                                   , new object[] { status, contractorID, DateTime.Now, null, suspendReason, suspendUser });
                }
                else
                {
                    gate.ExecuteStoredProcedure("C_DeleteContractor"
                                        , new string[] { "status", "ContractorID", "startDate", "endDate", "reason", "user" }
                                        , new object[] { status, contractorID, stardateCancel, enddateCancel, suspendReason, suspendUser });
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

        [Route("Delete")]
        [HttpPut]
        public IHttpActionResult InvalidConQua(string contractorID)
        {
            var cq = Db.ContractorQualifications.Find(contractorID);
            if (cq != null)
            {
                cq.Status = "X";
                cq.isvalid = 0;
                cq.Stamp = System.DateTime.Now;
                Db.Entry(cq).State = EntityState.Modified;
                Db.SaveChanges();
            }

            return Ok();
        }


        /// <summary>
        /// Send mail when suspended
        /// </summary>
        [Route("SuspendedMail")]
        [HttpPost]
        public IHttpActionResult SuspendedMailConQua(string ContractorID, string mailKind)
        {
            try
            {
                gate.ExecuteStoredProcedureNonQuery("C_@Daily_ConQuaNoticeSuspended"
                , new string[] { "ContractorID", "Mailkind" }
                , new object[] { ContractorID, mailKind });
                Db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }


        /// <summary>
        /// send mail when renewing the contractor
        /// </summary>
        [Route("ExtendMail")]
        [HttpPost]
        public IHttpActionResult ExtendMail(string ContractorID)
        {
            gate.ExecuteStoredProcedureNonQuery("C_@Daily_SendNoticeRenew"
                     , new string[] { "ContractorID", "Mailkind" }
                     , new object[] { ContractorID, "c_renew" });

            Db.SaveChanges();
            return Ok();
        }


        [Route("CancelSuspendContractor")]
        [HttpPut]
        public HttpResponseMessage CancelSuspendContractor(string contractorID, string enddate, string status, string cancelUser)
        {
            try
            {
                if (Convert.ToDateTime(enddate) >= DateTime.Now)
                {
                    gate.DbHelper.ExecuteStoredProcedure("[C_CancelSuspended]"
                                   , new string[] { "contractorID", "user", "status" }
                                   , new object[] { contractorID, cancelUser, status });


                }
                else
                {
                    gate.ExecuteNonQuery(@"UPDATE ContractorQualification
                                        SET [Status] = @status,
                                            Isvalid = 0,
                                            [Stamp] = @Stamp,
                                            [EnddateCancel] = @EnddateCancel,
                                            CancelSuspendUser = @cancelUser
                                        WHERE ContractorID = @contractorID"
                        , new object[] { 'E', DateTime.Now, DateTime.Now, cancelUser, contractorID });
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

        [Route("SendMailSubmit")]
        [HttpPost]
        public IHttpActionResult SendMailSubmit(string flowname, string IdVoucher, string FromUser, string MailKind)
        {
            try
            {
                gate.ExecuteStoredProcedureNonQuery("C_@Daily_SendContractorNotice"
                       , new string[] { "flowname", "VoucherID", "FromUser", "MailKind" }
                       , new object[] { flowname, IdVoucher, FromUser, MailKind });

                return Ok();
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("UploadFile")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadFileAsync()
        {
            List<string> returnedFileName = new List<string>();
            try
            {
                Console.WriteLine("Uploadfile:" + DateTime.Now);
                if (!this.Request.Content.IsMimeMultipartContent())
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                MultipartMemoryStreamProvider provider = new MultipartMemoryStreamProvider();
                MultipartMemoryStreamProvider memoryStreamProvider = await this.Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(provider);
                string _FilePath = WebConfigurationManager.AppSettings["ContractorFolder"];
                foreach (HttpContent content in provider.Contents)
                {
                    HttpContent file = content; //provider Content loop
                    string filename = string.Format("{0}-{1}"
                            , DateTime.Now.ToString("yyMMddHHmmss")
                            , file.Headers.ContentDisposition.FileName.Trim('"'));
                    returnedFileName.Add(filename);
                    byte[] buffer = await file.ReadAsByteArrayAsync();
                    if (!Directory.Exists(_FilePath))
                    {
                        Directory.CreateDirectory(_FilePath);
                    }
                    System.IO.File.WriteAllBytes(_FilePath + "\\" + filename, buffer);
                    filename = (string)null;
                    buffer = (byte[])null;
                    file = (HttpContent)null;
                }
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return (IHttpActionResult)this.Ok(e.Message);

            }
            return (IHttpActionResult)this.Ok(returnedFileName);

        }


        [Route("DeleteFile")]
        [HttpDelete]
        public IHttpActionResult DeleteFile(string filename)
        {
            Console.WriteLine("DeleteFile:" + filename + "-" + DateTime.Now);
            string _FilePath = WebConfigurationManager.AppSettings["ContractorFolder"];
            Console.WriteLine("configurationAppSettings.GetValue:" + _FilePath);
            try
            {
                string pathFile = Path.Combine(_FilePath, filename);
                Console.WriteLine("delete-pathFile:" + pathFile);
                Console.WriteLine(File.Exists(pathFile));
                if (File.Exists(pathFile))
                {
                    File.Delete(pathFile);
                    Console.WriteLine("The file name {0} is deleted!", filename);
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok();
        }

        [Route("SearchLog")]
        [HttpGet]
        public IHttpActionResult SearchLog(string Contractor, string IdCard, string startdate, string enddate, string Region, string EmpName, string vendor)
        {
            try
            {
                DataTable dt = gate.ExecuteStoredProcedure("EGT3_Logs"
                                    , new string[] { "UserID", "Contractor", "IdCard", "startdate", "enddate", "Gate", "EmpName", "vendor" }
                                    , new object[] { "", Contractor, IdCard, startdate, enddate, Region, EmpName, vendor }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("SearchStatistic")]
        [HttpGet]
        public IHttpActionResult SearchStatistic(string Contractor, string startdate, string enddate)
        {
            try
            {
                DataTable dt = gate.ExecuteStoredProcedure("C_StatisticLogs"
                                    , new string[] { "Contractor", "startdate", "enddate" }
                                    , new object[] { Contractor, startdate, enddate }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("ExportLog")]
        [HttpGet]
        public IHttpActionResult ExportLog(string Contractor, string IdCard, string startdate, string enddate, string Region, string RegionName, string EmpName, string vendor)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dtLog = new DataTable();
                DataTable drLog = new DataTable();
                string fileName = "";
                string folderPath = "D:\\ContractorFile\\";
                Dictionary<string, string> returnedFileName = new Dictionary<string, string>();

                dtLog.Columns.Add("次數 STT");
                dtLog.Columns.Add("承包商名称 TÊN NHÀ THẦU");
                dtLog.Columns.Add("EmployeeID");
                dtLog.Columns.Add("身份證號碼 SỐ CMND");
                dtLog.Columns.Add("工人姓名 HỌ TÊN CÔNG NHÂN");
                dtLog.Columns.Add("進出廠時間 NHẬT KÝ QUẸT THẺ");
                dtLog.Columns.Add("進廠次數 SỐ LẦN VÀO");
                dtLog.Columns.Add("出場次數 SỐ LẦN RA");
                dtLog.Columns.Add("異常 BẤT THƯỜNG");
                dtLog.Columns.Add("備註 GHI CHÚ");

                drLog = gate.ExecuteStoredProcedure("EGT3_Logs"
                                    , new string[] { "UserID", "Contractor", "IdCard", "startdate", "enddate", "Gate", "EmpName", "vendor" }
                                    , new object[] { "", Contractor, IdCard, startdate, enddate, Region, EmpName, vendor }).Tables[0];

                foreach (DataRow dr in drLog.Rows)
                {
                    dtLog.Rows.Add(dr.ItemArray);
                }

                dtLog.Columns.RemoveAt(2);
                dtLog.TableName = "Báo cáo - 報表";
                var dtcopy = dtLog.Copy();
                ds.Tables.Add(dtcopy);

                if (vendor == null)
                {
                    vendor = "";
                }

                if (dtLog.Rows.Count > 0)
                {
                    fileName = ExportLogToExcel(ds, folderPath, startdate, enddate, RegionName, vendor);
                }
                returnedFileName.Add("FileName", fileName);

                return Ok(returnedFileName);

            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }


        }


        public string ExportLogToExcel(DataSet ds, string strPath, string startdate, string enddate, string Region, string vendor)
        {
            try
            {
                string pathImg = strPath + "Logo.png";
                int inHeaderLength = 9, inColumn = 0, inRow = 0;
                System.Reflection.Missing Default = System.Reflection.Missing.Value;
                //Create Excel File
                var filename = "";
                if (startdate == enddate)
                {
                    filename = "BAO_CAO_CHAM_CONG_NHA_THAU_" + startdate + ".xlsx";
                }
                else
                {
                    filename = "BAO_CAO_CHAM_CONG_NHA_THAU_" + startdate + "_" + enddate + ".xlsx";
                }

                strPath += filename;

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
                    if (File.Exists(filePath))
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
                    excelWorkSheet.get_Range("B10", "E" + EndRow).HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignLeft;

                    excelWorkSheet.get_Range("A10", "A" + EndRow).HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;

                    excelWorkSheet.get_Range("F10", "I" + EndRow).HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;

                    excelWorkSheet.get_Range("A1", "I" + EndRow).Cells.Font.Name = "Times New Roman";

                    OfficeExcel.Range cellrange = excelWorkSheet.get_Range("A9", "I" + EndRow);
                    cellrange.Borders.LineStyle = OfficeExcel.XlLineStyle.xlContinuous;
                    cellrange.Borders.Weight = OfficeExcel.XlBorderWeight.xlThin;
                    cellrange.VerticalAlignment = OfficeExcel.XlVAlign.xlVAlignCenter;

                    //Excel Header
                    OfficeExcel.Range cellHeaderTW = excelWorkSheet.get_Range("A1", "I3");
                    cellHeaderTW.Merge(false);
                    cellHeaderTW.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;
                    cellHeaderTW.Font.Bold = OfficeExcel.XlBorderWeight.xlMedium;
                    cellHeaderTW.Font.Size = 26;
                    excelWorkSheet.Cells[1, 1] = "包商出勤紀錄報表";



                    OfficeExcel.Range cellHeaderVN = excelWorkSheet.get_Range("A4", "I4");
                    cellHeaderVN.Merge(false);
                    cellHeaderVN.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;
                    cellHeaderVN.Font.Bold = OfficeExcel.XlBorderWeight.xlMedium;
                    cellHeaderVN.Font.Size = 26;
                    excelWorkSheet.Cells[4, 1] = "BÁO CÁO CHẤM CÔNG NHÀ THẦU";

                    //timekeeping date
                    var date = "";
                    if (enddate == startdate)
                    {
                        date = startdate;
                    }
                    else
                    {
                        date = "日期 Từ ngày " + startdate + " 到 đến ngày " + enddate;
                    }

                    excelWorkSheet.get_Range("A5", "B5").Merge(false);
                    excelWorkSheet.Cells[5, 1] = "日期 Ngày chấm công:   " + date;

                    // export report date
                    OfficeExcel.Range cellHeaderDateEx = excelWorkSheet.get_Range("G5", "I5");
                    cellHeaderDateEx.Merge(false);
                    cellHeaderDateEx.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignRight;
                    excelWorkSheet.Cells[5, 7] = "創建報表日期 Ngày xuất báo cáo:   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                    // gate
                    excelWorkSheet.get_Range("A6", "B6").Merge(false);
                    excelWorkSheet.Cells[6, 1] = "施工區域 Khu vực thi công:   " + Region;

                    //total number of people
                    excelWorkSheet.get_Range("A7", "B7").Merge(false);
                    excelWorkSheet.Cells[7, 1] = "總數 Tổng số người:   " + dtbl.Rows.Count;

                    // set vendor 
                    OfficeExcel.Range cellVendor = excelWorkSheet.get_Range("A8", "B8");
                    cellVendor.Merge(false);
                    cellVendor.Font.Color = System.Drawing.Color.Red;
                    if (vendor == "3")
                    {
                        excelWorkSheet.Cells[8, 1] = "* 供應商 Nhà cung cấp";
                    }
                    else excelWorkSheet.Cells[8, 1] = "";

                    //Style table column names
                    OfficeExcel.Range cellHeader = excelWorkSheet.get_Range("A9", "I9");
                    cellHeader.Font.Bold = true;
                    cellHeader.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C5D9F1");
                    cellHeader.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;

                    //Auto fit columns
                    excelWorkSheet.Columns.AutoFit();
                    excelWorkSheet.get_Range("E:E", System.Type.Missing).EntireColumn.ColumnWidth = 36;
                    excelWorkSheet.get_Range("E:E", System.Type.Missing).EntireColumn.WrapText = true;
                    excelWorkSheet.get_Range("I:I", System.Type.Missing).EntireColumn.ColumnWidth = 27;

                }

            //Set Defualt Page
            (excelWorkBook.Sheets[1] as OfficeExcel._Worksheet).Activate();

                excelWorkBook.SaveAs(strPath);
                excelWorkBook.Close();
                excelApp.Quit();
                return filename;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }

        }


        [Route("ExportStatistic")]
        [HttpGet]
        public IHttpActionResult ExportStatistic(string Contractor, string startdate, string enddate)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dtLog = new DataTable();
                DataTable drLog = new DataTable();
                string fileName = "";
                string folderPath = "D:\\ContractorFile\\";
                Dictionary<string, string> returnedFileName = new Dictionary<string, string>();

                dtLog.Columns.Add("次數 STT");
                dtLog.Columns.Add("承包商 NHÀ THẦU");
                dtLog.Columns.Add("部門 BỘ PHẬN PHÁT THẦU");
                dtLog.Columns.Add("辦識別卡總人數 TỔNG SỐ THẺ ĐÃ ĐĂNG KÝ");
                dtLog.Columns.Add("進出廠人數 SỐ LƯỢNG NHÂN VIÊN RA VÀO XƯỞNG");

                drLog = gate.ExecuteStoredProcedure("C_StatisticLogs"
                                    , new string[] { "Contractor", "startdate", "enddate" }
                                    , new object[] { Contractor, startdate, enddate }).Tables[0];

                for (int i = 0; i < drLog.Columns.Count; i++)
                {
                    if (i >= 5)
                    {
                        dtLog.Columns.Add(drLog.Columns[i].ColumnName);
                    }
                }

                foreach (DataRow dr in drLog.Rows)
                {
                    for (int i = 5; i < dr.ItemArray.Length; i++)
                    {

                        if (dr.ItemArray[i] == DBNull.Value)
                        {
                            object[] rowArray = new object[dr.ItemArray.Length] ;
                            
                            for (int index = 0; index < dr.ItemArray.Length; index++)
                            {
                                rowArray[index] = dr.ItemArray[index];
                                rowArray[i] = 0;
                                
                                dr.ItemArray = rowArray;
                            }
                        }
                    }
                    dtLog.Rows.Add(dr.ItemArray);
                }

                dtLog.TableName = "Thống kê - 統計";
                var dtcopy = dtLog.Copy();
                ds.Tables.Add(dtcopy);


                if (dtLog.Rows.Count > 0)
                {
                    fileName = ExportStatisticToExcel(ds, folderPath, startdate, enddate);
                }
                returnedFileName.Add("FileName", fileName);
                return Ok(returnedFileName);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        public string ExportStatisticToExcel(DataSet ds, string strPath, string startdate, string enddate)
        {
            try
            {
                string pathImg = strPath + "Logo.png";
                int inHeaderLength = 7, inColumn = 0, inRow = 0;
                System.Reflection.Missing Default = System.Reflection.Missing.Value;
                //Create Excel File
                var filename = "";
                if (startdate == enddate)
                {
                    filename = "THONG_KE_NHA_THAU_" + startdate + ".xlsx";
                }
                else
                {
                    filename = "THONG_KE_NHA_THAU_" + startdate + "_" + enddate + ".xlsx";
                }

                strPath += filename;

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

                    excelWorkSheet.Shapes.AddPicture(filePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0, 150, 45);

                    int TotalColumn = dtbl.Columns.Count;

                    ////Write Column Name
                    var index = 0;
                    for (int i = 0; i < dtbl.Columns.Count; i++)
                    {
                        if (i == 5)
                        {
                            TotalColumn++;

                            excelWorkSheet.Cells[inHeaderLength + 1, i + 1] = dtbl.Columns[i].ColumnName.ToUpper();
                            excelWorkSheet.get_Range(excelWorkSheet.Cells[inHeaderLength + 1, i + 1], excelWorkSheet.Cells[inHeaderLength + 1, i + 2]).Merge(false);
                            excelWorkSheet.Cells[inHeaderLength + 2, i + 1] = "一日施工申請 Số CNV thi công 1 ngày";
                            excelWorkSheet.Cells[inHeaderLength + 2, i + 2] = "刷卡 Số CNV quẹt thẻ";
                            index = i + 2;
                        }
                        else if (i > 5 && i < dtbl.Columns.Count)
                        {
                            TotalColumn++;
                            if (i == dtbl.Columns.Count - 1)
                            {
                                excelWorkSheet.Cells[inHeaderLength + 1, index + 1] = "統計員工進出廠最多的日期 Thống kê ngày có CNV ra vào nhiều nhất";
                                var cellstatistic = excelWorkSheet.get_Range(excelWorkSheet.Cells[inHeaderLength + 1, index + 1], excelWorkSheet.Cells[inHeaderLength + 1, index + 3]);
                                cellstatistic.Merge(false);
                                cellstatistic.WrapText = true;

                            }
                            else
                            {
                                excelWorkSheet.Cells[inHeaderLength + 1, index + 1] = dtbl.Columns[i].ColumnName.ToUpper();
                                excelWorkSheet.get_Range(excelWorkSheet.Cells[inHeaderLength + 1, index + 1], excelWorkSheet.Cells[inHeaderLength + 1, index + 2]).Merge(false);
                            }
                            excelWorkSheet.Cells[inHeaderLength + 2, index + 1] = "一日施工申請 Số CNV thi công 1 ngày";
                            excelWorkSheet.Cells[inHeaderLength + 2, index + 2] = "刷卡 Số CNV quẹt thẻ";
                            index = index + 2;
                        }
                        else
                        {
                            excelWorkSheet.Cells[inHeaderLength, i + 1] = dtbl.Columns[i].ColumnName.ToUpper();
                        }

                        if (index == TotalColumn)
                        {
                            excelWorkSheet.Cells[inHeaderLength + 2, TotalColumn + 1] = "合計 Tổng CNV";
                            excelWorkSheet.Cells[inHeaderLength, TotalColumn + 2] = "備註 GHI CHÚ";
                        }
                    }


                    //Write Rows
                    for (int m = 0; m < dtbl.Rows.Count; m++)
                    {
                        index = 0;
                        for (int n = 0; n < dtbl.Columns.Count; n++)
                        {
                            inRow = 10 + m;
                            inColumn = n + 1;
                            if (n == 5)
                            {
                                excelWorkSheet.Cells[inRow, inColumn + 1] = dtbl.Rows[m].ItemArray[n].ToString();
                                index = n + 2;
                            }
                            else if (n > 5 && n < TotalColumn)
                            {
                                excelWorkSheet.Cells[inRow, index + 2] = dtbl.Rows[m].ItemArray[n].ToString();
                                index = index + 2;
                            }
                            else
                            {
                                excelWorkSheet.Cells[inRow, inColumn] = dtbl.Rows[m].ItemArray[n].ToString();
                            }

                        }
                    }

                    int EndRow = dtbl.Rows.Count + inHeaderLength + 2;

                    excelWorkSheet.Columns.AutoFit();

                    excelWorkSheet.get_Range("B10", "C" + EndRow).HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignLeft;

                    excelWorkSheet.get_Range("A10", "A" + EndRow).HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;

                    excelWorkSheet.get_Range("D10", excelWorkSheet.Cells[EndRow, TotalColumn + 2]).HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;

                    excelWorkSheet.get_Range("A1", excelWorkSheet.Cells[EndRow, TotalColumn + 2]).Cells.Font.Name = "Times New Roman";

                    OfficeExcel.Range cellrange = excelWorkSheet.get_Range("A7", excelWorkSheet.Cells[EndRow, TotalColumn + 2]);
                    cellrange.Borders.LineStyle = OfficeExcel.XlLineStyle.xlContinuous;
                    cellrange.Borders.Weight = OfficeExcel.XlBorderWeight.xlThin;
                    cellrange.VerticalAlignment = OfficeExcel.XlVAlign.xlVAlignCenter;

                    //Excel Header
                    OfficeExcel.Range cellHeaderTW = excelWorkSheet.get_Range("A1", excelWorkSheet.Cells[3, TotalColumn + 2]);
                    cellHeaderTW.Merge(false);
                    cellHeaderTW.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;
                    cellHeaderTW.Font.Bold = OfficeExcel.XlBorderWeight.xlMedium;
                    cellHeaderTW.Font.Size = 18;
                    excelWorkSheet.Cells[1, 1] = "承包商統計";

                    OfficeExcel.Range cellHeaderVN = excelWorkSheet.get_Range("A4", excelWorkSheet.Cells[4, TotalColumn + 2]);
                    cellHeaderVN.Merge(false);
                    cellHeaderVN.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;
                    cellHeaderVN.Font.Bold = OfficeExcel.XlBorderWeight.xlMedium;
                    cellHeaderVN.Font.Size = 18;
                    excelWorkSheet.Cells[4, 1] = "THỐNG KÊ NHÀ THẦU";

                    //statistic date
                    var date = "";
                    if (enddate == startdate)
                    {
                        date = startdate;
                    }
                    else
                    {
                        date = "日期 Từ ngày " + startdate + " 至 đến ngày " + enddate;
                    }

                    excelWorkSheet.get_Range("A5", "C5").Merge(false);
                    excelWorkSheet.Cells[5, 1] = "統計日期 Ngày thống kê:   " + date;

                    //Style table column names
                    OfficeExcel.Range cellHeader = excelWorkSheet.get_Range("A7", excelWorkSheet.Cells[9, TotalColumn + 2]);
                    cellHeader.Font.Bold = true;
                    cellHeader.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C5D9F1"); //blue
                    cellHeader.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;

                    //Stt
                    OfficeExcel.Range cellNo = excelWorkSheet.get_Range("A7", "A9");
                    cellNo.Merge(false);

                    //Contractor Name
                    OfficeExcel.Range cellContractorName = excelWorkSheet.get_Range("B7", "B9");
                    cellContractorName.Merge(false);

                    //Dept
                    OfficeExcel.Range cellDept = excelWorkSheet.get_Range("C7", "C9");
                    cellDept.Merge(false);

                    //issue card count
                    OfficeExcel.Range cellIssueCard = excelWorkSheet.get_Range("D7", "D9");
                    cellIssueCard.Merge(false);

                    //InOutCount
                    OfficeExcel.Range cellInOutCount = excelWorkSheet.get_Range("E7", "E9");
                    cellInOutCount.Merge(false);

                    //InOut by date
                    OfficeExcel.Range cellEmpInOut = excelWorkSheet.get_Range("F7", excelWorkSheet.Cells[7, TotalColumn + 1]);
                    cellEmpInOut.Merge(false);
                    excelWorkSheet.Cells[7, 6] = "每日承包商入廠人數 SỐ LƯỢNG NHÂN VIÊN NHÀ THẦU VÀO XƯỞNG HÀNG NGÀY";
                    cellEmpInOut.EntireRow.RowHeight = 30;

                    //verify Date
                    OfficeExcel.Range cellVerifyDate = excelWorkSheet.get_Range(excelWorkSheet.Cells[8, 6], excelWorkSheet.Cells[8, TotalColumn + 1]);
                    cellVerifyDate.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FCD5B4"); // orange
                    cellVerifyDate.EntireRow.RowHeight = 35;
                    cellVerifyDate.EntireColumn.ColumnWidth = 15;

                    var cellTotal = excelWorkSheet.Cells[8, TotalColumn + 1];
                    excelWorkSheet.get_Range(cellTotal, cellTotal).EntireColumn.ColumnWidth = 17;

                    //detail in out by date
                    OfficeExcel.Range celldetail = excelWorkSheet.get_Range(excelWorkSheet.Cells[9, 6], excelWorkSheet.Cells[9, TotalColumn + 1]);
                    celldetail.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#D8E4BC"); // green
                    celldetail.EntireRow.RowHeight = 42;

                    OfficeExcel.Range cellred = excelWorkSheet.get_Range(excelWorkSheet.Cells[10, TotalColumn - 1], excelWorkSheet.Cells[EndRow, TotalColumn + 1]);
                    cellred.Font.Color = System.Drawing.Color.Red;

                    //Remark
                    OfficeExcel.Range cellRemark = excelWorkSheet.get_Range(excelWorkSheet.Cells[7, TotalColumn + 2], excelWorkSheet.Cells[9, TotalColumn + 2]);
                    cellRemark.Merge(false);
                    cellRemark.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C5D9F1"); // blue
                    cellRemark.EntireColumn.ColumnWidth = 24;

                    //column width

                    excelWorkSheet.get_Range("D:D", System.Type.Missing).EntireColumn.ColumnWidth = 17;
                    excelWorkSheet.get_Range("D:D", System.Type.Missing).EntireColumn.WrapText = true;

                    excelWorkSheet.get_Range("E:E", System.Type.Missing).EntireColumn.ColumnWidth = 13;
                    excelWorkSheet.get_Range("E:E", System.Type.Missing).EntireColumn.WrapText = true;


                    for (int i = 6; i <= TotalColumn + 1; i++)
                    {
                        var cell = excelWorkSheet.get_Range(excelWorkSheet.Cells[9, i], excelWorkSheet.Cells[9, i]);
                        cell.WrapText = true;
                    }

                }

            //Set Defualt Page
            (excelWorkBook.Sheets[1] as OfficeExcel._Worksheet).Activate();
                excelWorkBook.SaveAs(strPath);
                excelWorkBook.Close();
                excelApp.Quit();
                return filename;
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
