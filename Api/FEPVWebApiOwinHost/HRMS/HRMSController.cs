using FEPVWebApiOwinHost.Filter;
using FEPVWebApiOwinHost.Models.HRMS;
using log4net;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FEPVWebApiOwinHost.HRMS
{
    [FilterIP]
    [RoutePrefix("api/Gate/HRMS")]
    public class HRMSController : ApiController
    {
        NBear.Data.Gateway oAC = new NBear.Data.Gateway("GATE");

        HelperBiz _helper = new HelperBiz();
        private NBear.Data.Gateway gateFEPVOA = new NBear.Data.Gateway("FEPVOA");
        private NBear.Data.Gateway gateHRM = new NBear.Data.Gateway("GATE");
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        BackgroundWorker _downloadbgwk = new BackgroundWorker();

        [Route("GetAttendance")]
        [HttpPost]
        public IHttpActionResult GetAttendance(Attendance_Query query)
        {
            try
            {
                string export = String.Empty;
                DataTable dt = oAC.ExecuteStoredProcedure(@"Get_Attendance", new string[] { "From", "To", "Status", "CardNumber", "EmployeeID", "Workplace" }, new object[] { query.From, query.To, query.Status, query.CardNumber, query.EmployeeID, query.Workplace }).Tables[0];
                //foreach (DataRow rows in dt.Rows)
                //{
                //    export += rows["Device"] + ";" + rows["CardNo"] + ";" + Convert.ToDateTime(rows["TimeSwipe"]).ToString("yyyyMMddHHmmss") + ";" + rows["Status"] == "OUT" ? "2" : "1";
                //}
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " " + ex.InnerException);
            }
        }

        [Route("GetAttendanceToExport")]
        [HttpPost]
        public IHttpActionResult GetAttendanceToExport(Attendance_Query query)
        {
            try
            {
                JsonObject json = new JsonObject();
                string export = String.Empty;
                DataTable dt = oAC.ExecuteStoredProcedure(@"Get_Attendance", new string[] { "From", "To", "Status", "CardNumber", "EmployeeID", "Workplace" }, new object[] { query.From, query.To, query.Status, query.CardNumber, query.EmployeeID, query.Workplace }).Tables[0];
                foreach (DataRow rows in dt.Rows)
                {
                    //string status = rows["Status"].ToString() == "OUT" ? "2" : "1";
                    //+ status+";"
                    export += rows["Device"].ToString().PadLeft(3, '0') + ";" + rows["CardNo"] + ";" + Convert.ToDateTime(rows["TimeSwipe"]).ToString("yyyyMMddHHmm") + ";" + Environment.NewLine;
                }
                json["Data"] = export;
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " " + ex.InnerException);
            }
        }

        [Route("GetAttendanceToExportMitaPro")]
        [HttpPost]
        public IHttpActionResult GetAttendanceToExportMitaPro(Attendance_Query query)
        {
            try
            {
                JsonObject json = new JsonObject();
                string export = String.Empty;
                DataTable dt = oAC.ExecuteStoredProcedure(@"Get_Attendance", new string[] { "From", "To", "Status", "CardNumber", "EmployeeID", "Workplace" }, new object[] { query.From, query.To, query.Status, query.CardNumber, query.EmployeeID, query.Workplace }).Tables[0];
                foreach (DataRow rows in dt.Rows)
                {
                    string status = rows["Status"].ToString() == "OUT" ? "2" : "1";
                    export += rows["Device"].ToString().PadLeft(3, '0') + rows["CardNo"] + Convert.ToDateTime(rows["TimeSwipe"]).ToString("yyyyMMddHHmm") + Environment.NewLine;
                }
                json["Data"] = export;
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " " + ex.InnerException);
            }
        }

        [Route("getEmployeeData")]
        [HttpGet]
        [Obsolete]
        public IHttpActionResult getEmployeeData(string EmployeeID, string EmployeeName, string CardNO, string WorkPlace)
        {
            try
            {
                DataTable dt = gateFEPVOA.ExecuteStoredProcedure(@"Get_Employees", new string[] { "EmployeeID", "CardNo", "WorkPlace", "EmployeeName" }, new object[] { EmployeeID, CardNO, WorkPlace, EmployeeName }).Tables[0];
                return Ok(dt);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }

        [Route("getDeviceData")]
        [HttpGet]
        [Obsolete]
        public IHttpActionResult getDeviceData()
        {
            try
            {
                string sqlQuery = @"SELECT * FROM TimeKeepperDevice";
                DataTable dt = gateHRM.SelectDataSet(sqlQuery, new object[] { }).Tables[0];
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }

        [Route("UpdateNDownloadNewEmployee")]
        [HttpGet]
        public int GetDataFromVnResourceService()
        {
            try
            {
                DbTransaction tran = gateFEPVOA.BeginTransaction();

                string EmployeeID = "";
                var UpdateFrom = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                var UpdateTO = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

                HRMSInterfaceDAL.Hr_SuZhou_Service.WebServiceHR _webService = new HRMSInterfaceDAL.Hr_SuZhou_Service.WebServiceHR();

                List<HRMSInterfaceDAL.Hr_SuZhou_Service.HREmpInfoBean> _profiles = _webService.GetListProfile(null, null, UpdateFrom, UpdateTO, null, "IT", "fepv123").ToList();
                //string _JsonProfiles = JsonConvert.SerializeObject(_listProfileData);
                //DataTable _dtProfiles = JsonConvert.DeserializeObject<DataTable>(_JsonProfiles);
                foreach (HRMSInterfaceDAL.Hr_SuZhou_Service.HREmpInfoBean _profile in _profiles)
                {
                    if (!string.IsNullOrEmpty(_profile.WorkNo))
                    {

                        if (_profile.WorkNo.Length > 6)
                        {
                            bool isExists = _profile.WorkNo.Contains("NN.");
                            if (!isExists)
                            {
                                EmployeeID = _profile.WorkNo.Substring(1, _profile.WorkNo.Length - 1);
                            }
                            else
                            {
                                EmployeeID = _profile.WorkNo;
                            }
                        }
                        else
                        {
                            EmployeeID = _profile.WorkNo;
                        }
                        string[] formats = {"dd/MM/yyyy", "dd-MMM-yyyy", "yyyy-MM-dd",
                          "dd-MM-yyyy", "M/d/yyyy","M/dd/yyyy", "dd MMM yyyy"};
                        string birthdayAfterConverted = DateTime.ParseExact(_profile.Birthday.Substring(0, _profile.Birthday.IndexOf(' ')).Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd");

                        FEPV_Employees_Data _FEPVED = new FEPV_Employees_Data();
                        _FEPVED.EmployeeID = "FEPV" + EmployeeID;
                        _FEPVED.EmployeeName = string.IsNullOrEmpty(_profile.NameVN.Trim()) ? (string.IsNullOrEmpty(_profile.NameEN) ? _profile.NameCN : _profile.NameEN) : _profile.NameVN;
                        _FEPVED.EmployeeName_Eng = string.IsNullOrEmpty(_profile.NameEN.Trim()) ? (string.IsNullOrEmpty(_profile.NameVN) ? _profile.NameCN : _profile.NameVN) : _profile.NameEN;
                        _FEPVED.CostCenter = _profile.CostCentreCode;
                        _FEPVED.CardNo = _profile.CardNo;
                        _FEPVED.State = Convert.ToBoolean(_profile.Status == "X" ? true : false);
                        _FEPVED.DepartmentName_CN = _profile.CostCentreNameCN;
                        _FEPVED.DepartmentName_EN = _profile.CostCentreName;
                        _FEPVED.PositionName = _profile.PositionName;
                        _FEPVED.JoinDate = Convert.ToDateTime(_profile.JoinDate);
                        _FEPVED.Gender = _profile.SexType;
                        _FEPVED.CompanyShortName = _profile.CompanyShortName;
                        _FEPVED.CompanyFullName = _profile.CompanyName;
                        _FEPVED.CompanyCode = _profile.CompanyCode;
                        _FEPVED.Birthday = Convert.ToDateTime(birthdayAfterConverted);
                        _FEPVED.LastUpdateDate = Convert.ToDateTime(string.IsNullOrEmpty(_profile.DateUpdate) ? DateTime.Now : Convert.ToDateTime(_profile.DateUpdate));
                        _FEPVED.Email = _profile.Email;
                        _FEPVED.Workplace = _profile.WorkPlaceName;
                        _FEPVED.NumberOfUpdate += 1;
                        string sql = @"SELECT * FROM FEPV_Employees_Data WHERE EmployeeID=@EmployeeID ";
                        DataTable dt = gateFEPVOA.SelectDataSet(sql, new object[] { _FEPVED.EmployeeID }).Tables[0];
                        if (dt.Rows.Count >= 1)
                        {
                            UpdateEmployee(_FEPVED);
                        }
                        else
                        {
                            InsertEmployee(_FEPVED);
                        }
                    }

                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                return 2;
            }
        }
        public bool UpdateEmployee(FEPV_Employees_Data emp)
        {
            System.Data.Common.DbTransaction tran = gateFEPVOA.BeginTransaction();
            try
            {
                string[] Columns = new string[] { "[EmployeeID]","[EmployeeName]","[EmployeeName_Eng]","[CostCenter]","[CardNo]","[State]","[DepartmentName_EN]"
                                ,"[DepartmentName_CN]","[Email]","[PositionName]","[JoinDate]","[Gender]","[CompanyShortName]","[CompanyFullName]","[CompanyCode]"
                                ,"[LastUpdateDate]","[Workplace]","[NumberOfUpdate]","[Birthday]" };
                object[] Values = new object[] {emp.EmployeeID,emp.EmployeeName,emp.EmployeeName_Eng,emp.CostCenter, emp.CardNo, emp.State, emp.DepartmentName_EN
                                ,emp.DepartmentName_CN,emp.Email,emp.PositionName,emp.JoinDate,emp.Gender,emp.CompanyShortName,emp.CompanyFullName,emp.CompanyCode
                                ,emp.LastUpdateDate,emp.Workplace,emp.NumberOfUpdate,emp.Birthday};
                gateFEPVOA.DbHelper.Update("FEPV_Employees_Data", Columns, Values, "EmployeeID = @EmployeeID2", new object[] { emp.EmployeeID }, tran);
                tran.Commit();
                tran.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tran.Rollback();
                throw new Exception(ex.Message);
            }

            return true;
        }

        public bool InsertEmployee(FEPV_Employees_Data emp)
        {
            System.Data.Common.DbTransaction tran = gateFEPVOA.BeginTransaction();
            try
            {
                string[] Columns = new string[] { "[EmployeeID]","[EmployeeName]","[EmployeeName_Eng]","[CostCenter]","[CardNo]","[State]","[DepartmentName_EN]"
                                ,"[DepartmentName_CN]","[Email]","[PositionName]","[JoinDate]","[Gender]","[CompanyShortName]","[CompanyFullName]","[CompanyCode]"
                                ,"[LastUpdateDate]","[Workplace]","[NumberOfUpdate]","[Birthday]" };
                object[] Values = new object[] {emp.EmployeeID,emp.EmployeeName,emp.EmployeeName_Eng,emp.CostCenter, emp.CardNo, emp.State, emp.DepartmentName_EN
                                ,emp.DepartmentName_CN,emp.Email,emp.PositionName,emp.JoinDate,emp.Gender,emp.CompanyShortName,emp.CompanyFullName,emp.CompanyCode
                                ,emp.LastUpdateDate,emp.Workplace,emp.NumberOfUpdate,emp.Birthday};
                gateFEPVOA.DbHelper.Insert("FEPV_Employees_Data", Columns, Values, tran, "EmployeeID");
                tran.Commit();
                tran.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tran.Rollback();
                throw new Exception(ex.Message);
            }

            return true;
        }

        [Route("GetWorkplaces")]
        [HttpGet]
        public IHttpActionResult GetWorkplaces(string language)
        {
            try
            {
                DataTable dt = gateHRM.ExecuteStoredProcedure(@"GetWorkplaces", new string[] { "lang" }, new object[] { language }).Tables[0];
                return Ok(dt);
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                return BadRequest(ex.Message + " " + ex.InnerException);
            }
        }
    }


    public class Attendance_Query
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Status { get; set; }
        public string CardNumber { get; set; }
        public string EmployeeID { get; set; }
        public string Workplace { get; set; }
    }
}
