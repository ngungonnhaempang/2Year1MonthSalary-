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
using System.Data.Entity.Infrastructure;
using RestSharp;
using FEPVWebApiOwinHost.Filter;

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
                                    ,new string[] { "Kind", "Language" }
                                    ,new object[] { kind, language }).Tables[0];
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
                                    ,new string[] { "Language" }
                                    ,new object[] { language }).Tables[0];
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
                                    ,new string[] { "Language" }
                                    ,new object[] { language }).Tables[0];
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
                                    ,new string[] { "Language" }
                                    ,new object[] { language }).Tables[0];
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
            DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetConQuaByEmployerId"
                                       ,new string[] { "ContractorID", "Language" }
                                       ,new object[] { contractorID, language }).Tables[0];
            Console.WriteLine(dt.Rows.Count);
            return Ok(_helper.ConvertJson(dt));
        }

        [Route("GetContractorUpdate")]
        [HttpGet]
        public IHttpActionResult GetContractorUpdate(string VoucherID, string language)
        {
            DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetContractorUpdate"
                                       ,new string[] { "VoucherID", "Language" }
                                       ,new object[] { VoucherID, language }).Tables[0];
            Console.WriteLine(dt.Rows.Count);
            return Ok(_helper.ConvertJson(dt));
        }

        [Route("GetContractorCheck")]
        [HttpGet]
        public IHttpActionResult GetContractorCheck(string voucherID, string language)
        {
            DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetContractorCheck"
                                       ,new string[] { "VoucherID", "Language" }
                                       ,new object[] { voucherID, language }).Tables[0];
            Console.WriteLine(dt.Rows.Count);
            return Ok(_helper.ConvertJson(dt));
        }

        [Route("GetContractorConfirm")]
        [HttpGet]
        public IHttpActionResult GetContractorConfirm(string voucherID, string language)
        {
            DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetContractorConfirm"
                                        ,new string[] { "VoucherID", "Language" }
                                        ,new object[] { voucherID, language }).Tables[0];
            Console.WriteLine(dt.Rows.Count);
            return Ok(_helper.ConvertJson(dt));
        }

       
        /// <summary>
        /// Search
        /// <summary>
        [Route("Get")]
        [HttpGet]
        public IHttpActionResult GetContractorQualification(string contractorName, string cType, string language, string departmentID, string status, string userid)
        {
            DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetConQuaByEmployer"
                                          ,new string[] { "Employer", "CType", "DepartmentID", "Language", "Status", "UserId" }
                                          ,new object[] { contractorName, cType, departmentID, language, status, userid }).Tables[0];
            return Ok(_helper.ConvertJson(dt));
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
        /// create and edit contractors
       /// </summary>
       /// <param name="contractorName"></param>
       /// <returns></returns>
        [Route("Get")]
        [HttpGet]
        public IHttpActionResult GetByname(string contractorName)
        {
            try
            {
                var contractorQualification = Db.ContractorQualifications.Where(c => c.ContractorName == contractorName).OrderByDescending(c => c.Stamp).ToList();
                if (contractorQualification.Count > 0)
                {
                    var valid = contractorQualification.FindAll(c => c.isvalid == 1).ToList();
                    if (valid.Count > 1)//两个有效，系统错误--Hai lỗi hệ thống hợp lệ                    
                    {
                        return BadRequest(string.Format("{0} Duplicate information, please call managers", contractorName));
                    }
                    else if (valid.Count == 1)
                    {

                        var contractorQua = Db.ContractorQualifications.Find(contractorQualification[0].ContractorID);

                        return Ok(contractorQua);

                    }
                    var newcq = contractorQualification[0];
                    // newcq.QualificationFiles = Db.ContractorQualificationFiles.Where(c => c.EmployerId == newcq.ContractorID).ToList();
                    // newcq.ContractorID = "";

                    //newcq.Status = "";
                    ///Console.WriteLine(newcq.QualificationFiles.Count);
                    //   newcq.QualificationFiles = newcq.QualificationFiles;
                    // newcq.isvalid = 0;

                    return Ok(newcq);
                }
                var cq = new ContractorQualification
                {
                    ContractorName = contractorName
                };
                return Ok(cq);

            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        private string UpdateContractorInfo(ContractorQualification cq, out string contractorID)
        {
            contractorID = cq.ContractorID.ToString();
            try
            {
                if (!string.IsNullOrEmpty(cq.ContractorID.ToString()))
                {
                    var contractorQualification = Db.ContractorQualifications.Where(c => c.ContractorName == cq.ContractorName && c.isvalid == 1).ToList();
                    if (contractorQualification.Count >= 1)
                    {
                        if (contractorQualification.Count == 1)
                        {
                            if (contractorID != contractorQualification[0].ContractorID)
                            {
                                return "ContractorID is being created,ContractorID  isn't the same!";
                            }
                        }
                        else
                        {
                            return "ContractorID  already exists";
                        }
                    }
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
                else
                {
                    return "ContractorID is Null!";
                }
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
            Console.WriteLine("Save：" + DateTime.Now.ToString());
            try
            {
                var Contractors = Db.ContractorQualifications.Where(c => c.ContractorName == cq.ContractorName.ToUpper() && c.isvalid == 1).ToList();
                if (Contractors.Count <= 0)
                {
                    string type = "Contractor";
                    cq.ContractorID = gate.SelectScalar<string>("select dbo.fnContractorEmployeeID(@typeID)", new object[] { type });
                    cq.ContractorName = cq.ContractorName.ToUpper();
                    cq.Stamp = DateTime.Now;
                    cq.Status = "N";
                    cq.ContractorFile = cq.ContractorFile;
                    cq.EmailContractor = cq.EmailContractor;
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

        //=============================================================================================================
        [Route("ConQuaSaveStatus")]
        [HttpPut]
        public HttpResponseMessage ConQuaSaveStatus(string contractorID, string status)
        {
            if (string.IsNullOrEmpty(contractorID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "ContractorID is empty！");
            try
            {
                if (status == "X")
                {
                    gate.ExecuteNonQuery(@"UPDATE ContractorQualification
                                            SET [Status] = @Status,
                                                Isvalid = 0,
                                                [Stamp] = @Stamp
                                            WHERE ContractorID = @ContractorID"
                        ,new object[] { status, DateTime.Now, contractorID }); 
                }
                else
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
        /// <param name="status"></param>
        /// <param name="contractorID"></param>
        /// <param name="stardateCancel"></param>
        /// <param name="enddateCancel"></param>
        /// <returns></returns>
        [Route("ConQuaSaveStatusSuspend")]
        [HttpPut]
        public HttpResponseMessage ConQuaSaveStatusSuspended(string status, string contractorID, string stardateCancel, string enddateCancel)
        {
            if (string.IsNullOrEmpty(contractorID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "ContractorID is empty！");
            try
            {
                if (status == "SC")
                {
                    gate.ExecuteNonQuery(@"UPDATE ContractorQualification
                                            SET [Status] = @Status,
                                            [Stamp] = @Stamp,
                                            [StartdateCancel] = @StartdateCancel,
                                            [EnddateCancel] = @EnddateCancel
                                            WHERE ContractorID = @ContractorID"
                    , new object[] { status, DateTime.Now, stardateCancel, enddateCancel, contractorID });
                }             
                else 
                {
                    DataTable _userList;
                    if (status == "PC") {
                         _userList = gate.ExecuteStoredProcedure("C_DeleteContractor"
                                         ,new string[] { "status", "ContractorID", "Stamp", "startDate", "endDate" }
                                         , new object[] { status, contractorID, DateTime.Now, DateTime.Now, null }).Tables[0];
                    }
                    else
                    {
                         _userList = gate.ExecuteStoredProcedure("C_DeleteContractor"
                                             ,new string[] { "status", "ContractorID", "Stamp", "startDate", "endDate" }
                                             ,new object[] { status, contractorID, DateTime.Now, stardateCancel, enddateCancel }).Tables[0];
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
       
        [Route("GetConQuaByEmployer")]
        [HttpGet]
        public IHttpActionResult Get(string ContractorName, string cType, string departmentID, string language)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetConQuaByEmployer",
                    new string[] { "ContractorName", "CType", "DepartmentID", "Language" },
                    new object[] { ContractorName, cType, departmentID, language }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }


        [Route("Delete")]
        [HttpPut]
        public IHttpActionResult InvalidConQua(string contractorID, string userId)
        {
            var cq = Db.ContractorQualifications.Find(contractorID);
            if (cq != null)
            {
                if (cq.Status == "F")
                {
                    var contractors = Db.Contractors.Where(c => c.ContractorID == contractorID).ToList();
                    if (contractors.Count > 0)
                    {
                        return BadRequest("ConQua_Delete_Failed");
                    }
                    else
                    {
                        cq.Status = "X";
                        cq.Stamp = System.DateTime.Now;
                    }
                }
                else
                {
                    return BadRequest("Delete_Checking_Msg");
                }
            }

            return Ok();
        }
        
        //[Route("CardLogsDetail")]
        //[HttpGet]
        //public IHttpActionResult QueryCardLogDetail(DateTime date, string contractorID, string employer)
        //{
        //    try
        //    {
        //        DataTable cardLogs = gate.DbHelper.ExecuteStoredProcedure("C_StatisLogs",
        //            new string[] { "Date", "Employer" },
        //            new object[] { date, employer }).Tables[0];               
        //        return Ok(_helper.ConvertJson(cardLogs));
        //    }
        //    catch (Exception e)
        //    {
        //        Loger.Error(e);
        //        return BadRequest(e.Message);
        //    }
        //}

        ///// <summary>
        ///// 统计承揽商进出厂的次数 -- Đếm số lần nhà thầu vào và rời nhà máy
        ///// </summary>
        ///// <param name="date">查询的天</param>
        ///// <param name="employerId">No Using</param>
        ///// <param name="employer">承揽商名称</param>
        ///// <returns></returns>
        ////[Route("CardLogs")]
        ////[HttpGet]
        ////public IHttpActionResult QueryAllCardLog(DateTime date, string contractorID, string contractorName)
        ////{
        ////    try
        ////    {
        ////        DataTable cardLogs = gate.DbHelper.ExecuteStoredProcedure("C_StatisLogs",
        ////            new string[] { "Date", "Employer" },
        ////            new object[] { date, employer }).Tables[0];
        ////        //得到所有的日志-- Nhận tất cả các bản ghi
        ////        List<CardLogs> list = _helper.ConvertList<CardLogs>(cardLogs);
        ////        List<string> personList = new List<string>();

        ////        foreach (var card in list)
        ////        {

        ////            personList.Add(card.ContractorID);
        ////        }
        ////        //得到日志的承揽商公司名称 -- Tên công ty nhà thầu để có được nhật ký
        ////        var newpersonList = personList.Distinct().ToList();
        ////        List<ContractorStaticsDto> conlist = new List<ContractorStaticsDto>();
        ////        foreach (var _employerId in newpersonList)
        ////        {
        ////            var con = new ContractorStaticsDto();
        ////            DataTable emdt = GetLogdepartmentByEmployerid(_employerId);
        ////            if (emdt.Rows.Count > 0)
        ////            {
        ////                con.DepartmentID = emdt.Rows[0]["DepartmentID"].ToString();
        ////                con.ContractorName = emdt.Rows[0]["ContractorName"].ToString();
        ////            }
        ////            else
        ////            {

        ////                con.DepartmentID = "";
        ////                con.Empolyer = "";
        ////            }
        ////            //  = GetLogdepartmentByEmployerid(contractor.EmployerId);
        ////            // }
        ////            //  con.Empolyer = contractor.Employer;
        ////            con.DateForm = date.ToString("yyyy-MM-dd");
        ////            if (!string.IsNullOrEmpty(con.ContractorName))
        ////            {
        ////                con.PeopleCount = GetValidCard(_employerId); //得到有效卡 -- Nhận thẻ hợp lệ
        ////            }
        ////            var cardlist = list.Where(a => a.ContractorID == _employerId).ToList();
        ////            con.FactoryCount = GetFactoryCountByCardList(cardlist);//在厂内 -- Trong nhà máy
        ////            con.InvialCount = GetExceptionCountByCardList(cardlist);  //无进有出的是异常 多少笔 -- Không có gì trong và ngoài là bất thường
        ////            con.InCount = cardlist.Where(a => a.Operate == 2 && a.ContractorID == _employerId).GroupBy(l => l.ContractorID).Count();
        ////            //con.InCount = cardlist.Where(a => a.Operate == 2 && a.EmployerId == _employerId).GroupBy(.Count();
        ////            con.OutCount = GetCountOutByCardList(cardlist);//cardlist.Where(a => a.Operate == 1 && a.EmployerId == _employerId).Count();
        ////            conlist.Add(con);

        ////        }
        ////        return Ok(conlist);
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        Loger.Error(e);
        ////        return BadRequest(e.Message);
        ////    }


        ////}



        ////private DataTable GetLogdepartmentByEmployerid(string contractorID)
        ////{
        ////    DataTable dt = gate.DbHelper.Select("select DepartmentID,ContractorName  from [dbo].[ContractorQualification] where ContractorID=@ContractorID",
        ////            new object[] { contractorID }).Tables[0];
        ////    return dt;
        ////    // return "";
        ////}
        ///// <summary>
        ///// 得到这个承揽商的有效的卡 -- Nhận thẻ hợp lệ cho nhà thầu này
        ///// </summary>
        ///// <param name="employer"></param>
        ///// <returns></returns>
        //private int GetValidCard(string contractorID)
        //{
        //    DataTable dt = gate.DbHelper.ExecuteStoredProcedure("C_GetValidCardByEmployer",
        //                new string[] { "ContractorID" }, new object[] { contractorID }).Tables[0];

        //    return dt.Rows.Count;
        //}
        ///// <summary>
        ///// 得到这个承揽商的异常出厂记录 (包商没有进厂刷卡，可是又出厂的刷卡) 
        ///// -- Lấy hồ sơ nhà máy bất thường của nhà thầu này (nhà thầu không vào nhà máy để quẹt thẻ, nhưng lại quẹt nhà máy)
        ///// </summary>
        ///// <param name="cardlist"></param>
        ///// <returns></returns>
        //private int GetExceptionCountByCardList(List<CardLogs> cardlist)
        //{
        //    List<string> cList = new List<string>();
        //    foreach (var card in cardlist)
        //    {
        //        cList.Add(card.ContractorID);
        //    }
        //    var newpersonList = cList.Distinct().ToList();
        //    int inCounts = 0;
        //    foreach (var id in newpersonList)
        //    {
        //        //这个包商没有进厂刷卡，可是又出厂的刷卡 --Nhà thầu này đã không vào nhà máy để quẹt thẻ, nhưng nhà máy lại quẹt
        //        var logsList = cardlist.Where(a => a.ContractorID == id).ToList();
        //        var inCount = logsList.Where(a => a.Operate == 2).Count();
        //        var outCount = logsList.Where(a => a.Operate == 1).Count();
        //        if (inCount <= 0 && outCount > 0)
        //        {

        //            inCounts++;
        //        }

        //    }
        //    return inCounts;
        //}

        ///// <summary>
        ///// 得到承揽商进厂或者出厂的 记录（进的只要有进人的统计，出的看最后的是出）
        ///// Nhận hồ sơ của nhà thầu vào nhà máy hoặc rời khỏi nhà máy
        ///// </summary>
        ///// <param name="cardlist"></param>
        ///// <returns></returns>
        //private int GetCountOutByCardList(List<CardLogs> cardlist)
        //{
        //    List<string> cList = new List<string>();
        //    foreach (var card in cardlist)
        //    {
        //        cList.Add(card.ContractorID);
        //    }
        //    var newpersonList = cList.Distinct().ToList();
        //    int OutCount = 0;
        //    foreach (var id in newpersonList)
        //    {
        //        //如果最后一笔的刷卡是1 为在出厂，否则出厂 --Nếu lần quẹt thẻ cuối cùng là 1 thì đó là ra nhà máy, nếu không thì là ra nhà máy ??
        //        var operate = cardlist.Where(a => a.ContractorID == id).OrderByDescending(c => c.OperateTime).First().Operate;
        //        if (operate == 1)
        //        {
        //            OutCount++;
        //        }

        //    }

        //    return OutCount;

        //}

        ///// <summary>
        ///// 得到承揽商在厂内的人数列表 (最后一笔是进厂没有出场)
        ///// Lấy danh sách số lượng nhà thầu trong nhà máy (mục cuối cùng là không có mục nhập vào nhà máy)
        ///// </summary>
        ///// <param name="cardlist"></param>
        ///// <returns></returns>
        //private int GetFactoryCountByCardList(List<CardLogs> cardlist)
        //{

        //    List<string> cList = new List<string>();
        //    foreach (var card in cardlist)
        //    {
        //        cList.Add(card.ContractorID);
        //    }
        //    var newpersonList = cList.Distinct().ToList();
        //    int inCount = 0;
        //    foreach (var id in newpersonList)
        //    {
        //        //如果最后一笔的刷卡是2 为在厂内，否则出厂 --Nếu lần quẹt thẻ cuối cùng là 2 trong nhà máy, nếu không hãy rời khỏi nhà máy
        //        var operate = cardlist.Where(a => a.ContractorID == id).OrderByDescending(c => c.OperateTime).First().Operate;
        //        if (operate == 2)
        //        {
        //            inCount++;
        //        }

        //    }

        //    return inCount;
        //}

        /// <summary>
        /// Send mail when suspended
        /// </summary>
        /// <param name="ContractorID"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        [Route("SuspendedMail")]
        [HttpPost]
        public IHttpActionResult SuspendedMailConQua(string ContractorID, string Reason)
        {
            {
                var entity = Db.ContractorQualifications.First(x => x.ContractorID == ContractorID);
                if (entity.Status == "C" || entity.Status == "SC")
                {
                    gate.ExecuteStoredProcedureNonQuery("C_@Daily_ConQuaNoticeSuspended",
                        new string[] { "ContractorID", "FromUser", "Kinds", "FormKey", "Mailkind", "Reason" }
                        , new object[] { ContractorID, null, null, null, "c_suspended",Reason });
                }
                else if (entity.Status == "PC")
                {
                    gate.ExecuteStoredProcedureNonQuery("C_@Daily_ConQuaNoticeSuspended",
                        new string[] { "ContractorID", "FromUser", "Kinds", "FormKey", "Mailkind", "Reason" }
                        , new object[] { ContractorID, null, null, null, "c_suspendedTemporary",Reason });
                }
                else
                {
                    gate.ExecuteStoredProcedureNonQuery("C_@Daily_ConQuaNoticeSuspended",
                        new string[] { "ContractorID", "FromUser", "Kinds", "FormKey", "Mailkind", "Reason" }
                        , new object[] { ContractorID, null, null, null, "c_Cancelsuspended", null });
                }


                Db.SaveChanges();
                return Ok();
            }
           
        }

        /// <summary>
        /// send mail when renewing the contractor
        /// </summary>
        /// <param name="ContractorID"></param>
        /// <returns></returns>
        [Route("ExtendMail")]
        [HttpPost]
        public IHttpActionResult ExtendMail(string ContractorID)
        {
          
                    gate.ExecuteStoredProcedureNonQuery("C_@Daily_SendNoticeExtension",
                        new string[] { "ContractorID", "FromUser", "Kinds", "FormKey", "Mailkind" }
                        , new object[] { ContractorID, null, null, null, "c_extend"});

                Db.SaveChanges();
                return Ok();
            }

        /// <summary>
        /// Cancellation of suspension of individual contractors when canceling suspension of contractors
        /// </summary>
        /// <param name="contractorID"></param>
        /// <param name="enddate"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Route("CancelSuspendContractor")]
        [HttpPut]
        public HttpResponseMessage CancelSuspendContractor(string contractorID, string enddate, string status)
        {
            try
            {
                if (Convert.ToDateTime(enddate) >= DateTime.Now)
                {
                    gate.ExecuteNonQuery(@"UPDATE ContractorQualification
                                        SET [Status] = @status,
                                            [Stamp] = @Stamp,
                                            [EnddateCancel] = @EnddateCancel
                                        WHERE ContractorID =@contractorID"
                        ,new object[] { 'Q', DateTime.Now, DateTime.Now, contractorID });

                    if (status == "PC" || status == "C")
                    {
                        var data = gate.DbHelper.ExecuteStoredProcedure("[C_CancelSuspended]"
                                        ,new string[] { "contractorID" }
                                        ,new object[] { contractorID}).Tables[0];

                    }
                }
                else
                {
                    gate.ExecuteNonQuery(@"UPDATE ContractorQualification
                                        SET [Status] = @status,
                                            Isvalid = 0,
                                            [Stamp] = @Stamp,
                                            [EnddateCancel] = @EnddateCancel
                                        WHERE ContractorID = @contractorID"
                        , new object[] { 'E', DateTime.Now, DateTime.Now, contractorID });
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
