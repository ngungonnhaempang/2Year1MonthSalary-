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
using log4net;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using FEPVWebApiOwinHost.Models.Meal;
using RestSharp;
using Shawoo.Core;
using FEPVWebApiOwinHost.Models;

namespace FEPVWebApiOwinHost
{
    [FEPVWebApiOwinHost.Filter.FilterIP]
    [RoutePrefix("api/Gate/OAController")]
    public class OAController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("FEPVOA");
        private DB db = new DB("FEPVOA");
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        [Route("Test")]
        [HttpGet]

        public IHttpActionResult Test(string UserID)
        {
            try
            {
                DataTable dt = gate.DbHelper.Select(@"SELECT * FROM MEAL_UserLogDetails", new object[] { }).Tables[0];

                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }
        }

        [Route("ShowData")]
        [HttpGet]

        public IHttpActionResult ShowData(string UserID, DateTime DateB, DateTime DateE, string Type)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("MEAL_ShowData",
                                    new string[] { "UserID", "DateB", "DateE", "Type" },
                                    new object[] { UserID, DateB, DateE, Type }).Tables[0];

                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }
        }

        [Route("ShowDataByDepartmentID")]
        [HttpGet]

        public IHttpActionResult ShowDataByDepartmentID(string UserID, DateTime DateB, DateTime DateE, string DepartmentID, bool IncludeOTUser)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("MEAL_ShowDataByDepartmentID",
                                    new string[] { "UserID", "DateB", "DateE", "DepartmentID", "IncludeOTUser" },
                                    new object[] { UserID, DateB, DateE, DepartmentID, IncludeOTUser }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }
        }
        [Route("MEAL_GetForeignEmployee")]
        [HttpGet]

        public IHttpActionResult MEAL_GetForeignEmployee(string EmployeeID, string CardNumber, string EmployeeName)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("MEAL_GetForeignEmployee",
                                    new string[] { "EmployeeID", "CardNumber", "EmployeeName" },
                                    new object[] { EmployeeID, CardNumber, EmployeeName }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }
        }

        [Route("ShowDataByDepartmentID_Details")]
        [HttpGet]

        public IHttpActionResult ShowDataByDepartmentID_Details(string UserID, string DepartmentID, DateTime DateB, DateTime DateE, string Type, bool IncludeOTUser)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("MEAL_ShowDataByDepartmentID_Details",
                                    new string[] { "UserID", "DepartmentID", "DateB", "DateE", "Type", "IncludeOTUser" },
                                    new object[] { UserID, DepartmentID, DateB, DateE, Type, IncludeOTUser }).Tables[0];
                Console.WriteLine(dt.Rows.Count);
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }
        }

        [Route("UploadUserHandle")]
        [HttpGet]

        //1111:10-02-12|
        public IHttpActionResult UploadUserHandle(string UserInfo)
        {
            int Added = 0, Error = 0, Modified = 0;
            try
            {
                foreach (string User in UserInfo.Split('|'))
                {
                    try
                    {
                        if ((int)gate.DbHelper.SelectScalar("SELECT COUNT(*) FROM EmployeeHandle WHERE EmployeeID = @EmployeeID AND Date = @Date AND Type = @Type", new object[] { User.Split(':')[0], Convert.ToDateTime(User.Split(':')[1]), User.Split(':')[2] }) > 0)
                        {

                            gate.DbHelper.ExecuteNonQuery("DELETE EmployeeHandle WHERE EmployeeID = @EmployeeID AND Date = @Date  AND Type = @Type ", new object[] { User.Split(':')[0], Convert.ToDateTime(User.Split(':')[1]), User.Split(':')[2] });
                            Modified++;
                        }
                        else { Added++; }
                        gate.DbHelper.ExecuteNonQuery(@"INSERT INTO EmployeeHandle VALUES(@EmployeeID,@Date, @Type, @Reason,@Stamp)", new object[] { User.Split(':')[0].PadLeft(6, '0'), Convert.ToDateTime(User.Split(':')[1]), User.Split(':')[2], User.Split(':')[3], DateTime.Now });
                    }
                    catch (Exception ex)
                    {
                        MSG_Info = ex.Message;
                        Error++;
                    }
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("Added");
                dt.Columns.Add("Modified");
                dt.Columns.Add("Error");
                DataRow dr = dt.NewRow();
                dr["Added"] = Added;
                dr["Modified"] = Modified;
                dr["Error"] = Error;
                dt.Rows.Add(dr);
                return Ok(dt);
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }
        }

        [Route("UploadExcelFile")]
        [HttpPost]
        public IHttpActionResult UploadExcelFile(EmployeeHandle detail)
        {
            JsonObject json = new JsonObject();
            var boo = false;
            try
            {


                //  DateTime conv = DateTime.FromOADate(double.Parse("43153"));
                foreach (EmployeeHandleDetail item in detail.empDetail)
                {
                    EmployeeHandleDetail empDetail = new EmployeeHandleDetail();
                    empDetail.DateSwipe = item.DateSwipe;
                    empDetail.EmployeeID_Old = item.EmployeeID_Old.PadLeft(6, '0');
                    // empDetail.DateSwipe = Convert.ToDateTime(DateTime.FromOADate(double.Parse(item.DateSwipe.ToString())));
                    empDetail.Type = item.Type;
                    empDetail.Reason = item.Reason;
                    empDetail.Stamp = DateTime.Now;
                    var data = db.Select<EmployeeHandleDetail>(new EmployeeHandleDetail { EmployeeID_Old = empDetail.EmployeeID_Old, DateSwipe = empDetail.DateSwipe, Type = empDetail.Type });
                    if (string.IsNullOrEmpty(data.Reason))
                    {
                        boo = db.Insert(empDetail);
                    }

                    //gate.DbHelper.ExecuteNonQuery(@"INSERT INTO EmployeeHandle VALUES(@EmployeeID,@Date, @Type, @Reason,@Stamp)", new object[] { item.EmployeeID_Old.ToString().Trim(),Convert.ToDateTime(DateTime.FromOADate(double.Parse(item.DateSwipe))), item.Type, item.Reason, DateTime.Now });
                    // var host = Dns.GetHostEntry(Dns.GetHostName());
                    //       var test = (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).ToList();
                    //string mac = "";
                    //foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                    //{

                    //    if (nic.OperationalStatus == OperationalStatus.Up && (!nic.Description.Contains("Virtual") && !nic.Description.Contains("Pseudo")))
                    //    {
                    //        if (nic.GetPhysicalAddress().ToString() != "")
                    //        {
                    //            mac = nic.GetPhysicalAddress().ToString();
                    //        }
                    //    }
                    //}

                }
                if (boo)
                {
                    json["Mess"] = "Upload Successfully";
                }


                return Ok(json);
            }
            catch (Exception e)
            {
                json["Error"] = e.Message;
                return Ok(json);
            }
        }
        [Route("Foreign_EmployeeFromUploadFile")]
        [HttpPost]
        public IHttpActionResult Foreign_EmployeeFromUploadFile(Foreign_EmployeeHandler data)
        {
            JsonObject json = new JsonObject();
            try
            {
                using (FEPVOAServiceContext _FEPVOAContext = new FEPVOAServiceContext())
                {
                   
                    string MSGError = String.Empty;
                    foreach (Foreign_Employee_CardHistory item in data.listForeign_Employee)
                    {

                        var _Foreign_Employeedata = _FEPVOAContext.Foreign_Employee.SingleOrDefault(m => m.CardNumber == item.CardNumber && (m.CardNumber != "" || m.CardNumber != null) && m.EmployeeID != item.EmployeeID);
                        if (_Foreign_Employeedata != null)
                        {
                            MSGError += item.EmployeeName + "-" + item.CardNumber + " was duplicated with " + _Foreign_Employeedata.EmployeeName + "-" + _Foreign_Employeedata.CardNumber + Environment.NewLine;
                        }
                        else
                        {
                            var _result = _FEPVOAContext.Foreign_Employee.SingleOrDefault(m => m.EmployeeID == item.EmployeeID);
                            if (_result != null)
                            {
                                _result.CardNumber = string.IsNullOrEmpty(item.CardNumber) ? "" : item.CardNumber.PadLeft(9, '0');
                                _result.DepartmentID = item.DepartmentID;
                                _result.EmployeeID = item.EmployeeID;
                                _result.EmployeeName = item.EmployeeName.ToUpper();
                                _result.LastUpdate = DateTime.Now;
                                _FEPVOAContext.Entry(_result).State = EntityState.Modified;
                                _FEPVOAContext.SaveChanges();
                            }
                            else
                            {
                                int lastestValue = 0;
                                string cardID = "";
                                if (item.EmployeeID.IndexOf("NN.") == -1)
                                {
                                    DataTable dt = gate.DbHelper.ExecuteStoredProcedure("CheckLastestCardID",
                                      new string[] { },
                                      new object[] { }).Tables[0];
                                    lastestValue = Convert.ToInt32(dt.Rows[0][0]);
                                    lastestValue++;
                                    cardID = "55" + lastestValue.ToString().PadLeft(4, '0');
                                }
                                else
                                {
                                    var index = item.EmployeeID.IndexOf(".") + 1;
                                    cardID = item.EmployeeID.Substring(index, item.EmployeeID.Length - index).PadLeft(6, '0');
                                }


                                _FEPVOAContext.Foreign_Employee.Add(new Foreign_Employee_CardHistory
                                {
                                    Area = "FG",
                                    CardID = cardID,
                                    CardNumber = string.IsNullOrEmpty(item.CardNumber) ? "" : item.CardNumber.PadLeft(9, '0'),
                                    DepartmentID = item.DepartmentID,
                                    Department_CN = "",
                                    Department_EN = "",
                                    EmployeeID = item.EmployeeID,
                                    EmployeeName = item.EmployeeName.ToUpper(),
                                    LastUpdate = DateTime.Now
                                });
                                _FEPVOAContext.SaveChanges();
                            }
                        }
                    }
                    gate.DbHelper.ExecuteStoredProcedure("CopyDataForeignEmployee",
                                    new string[] { },
                                    new object[] { });
                    gate.DbHelper.ExecuteStoredProcedure("AutoUpdateDepartmentSpec",
                                    new string[] { },
                                    new object[] { });

                    if (string.IsNullOrEmpty(MSGError))
                    {
                        json["MSG"] = "";
                    }
                    else
                    {
                        json["MSG"] = MSGError;
                    }
                }

                return Ok(json);
            }
            catch (Exception ex)
            {

                json["MSG"] = ex.Message;
                return Ok(json);
            }
        }


        [Route("GetData_From_HCM")]
        [HttpGet]
        public IHttpActionResult GetData_From_HCM()
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("GetData_From_HCM",
                                    new string[] { "UserID", },
                                    new object[] { "" }).Tables[0];
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
