using System.Data.SqlClient;

using log4net;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Security;
using MongoDB.Driver.Builders;
using FEPVWebApiOwinHost.Models;
using System.Data;
using System.Net.Http;
using System.Net;
using FEPVWebApiOwinHost.Filter;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace FEPVWebApiOwinHost
{

    [FilterIP]
    public class HSSEController : ApiController
    {


        static List<MembershipUser> allMembershipUsers = Membership.GetAllUsers().Cast<MembershipUser>().ToList();

        static MongoCollection<ProcessLog> collection = new MongoClient(ConfigurationManager.AppSettings["mongoConnectionString"]).GetServer()
             .GetDatabase(ConfigurationManager.AppSettings["mongoDatabase"]).GetCollection<ProcessLog>(ConfigurationManager.AppSettings["mongoCollection4Form"]);



        private NBear.Data.Gateway gateIM = new NBear.Data.Gateway("IM");


        //   protected readonly SpecialTruckContext Db = new SpecialTruckContext();

        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");

        NBear.Data.Gateway oAC = new NBear.Data.Gateway("Beling");

        static MongoServer server = MongoServer.Create(ConfigurationManager.AppSettings["mongoConnectionString"]);

        static MongoDatabase mongoDatabase = server.GetDatabase(ConfigurationManager.AppSettings["mongoDatabase"]);

        static List<Employee> employees = new DbContext("GATE").Database.SqlQuery<Employee>(@"EXEC GetUsers").ToList();
        private HelperBiz _helper = new HelperBiz();
        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    Db.Dispose();

            //}
            base.Dispose(disposing);
        }
        [Route("api/HSSE/Test")]
        [HttpPost]
        public IHttpActionResult TestUser()
        {
            try
            {

                return Ok(DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok();
            }

        }

        [Route("api/HSSE/UploadFile")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadFileAsync()
        {
            if (!this.Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            MultipartMemoryStreamProvider provider = new MultipartMemoryStreamProvider();
            MultipartMemoryStreamProvider memoryStreamProvider = await this.Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(provider);
            foreach (HttpContent content in provider.Contents)
            {
                HttpContent file = content;
                var test = file.Headers.ContentDisposition.FileName;
                var aa = "\"HELLO\"";

                var bb = aa.Trim('"');
                string filename = file.Headers.ContentDisposition.FileName.Trim('"');
                byte[] buffer = await file.ReadAsByteArrayAsync();
                string path = @"C:\uploads";
                var taaa = Path.GetExtension(filename);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (File.Exists(path + @"\" + filename))
                {
                    System.IO.File.Move(path + @"\" + filename, path + @"\" + (filename.Replace(Path.GetExtension(filename), "") + @"_ver " + Guid.NewGuid()) + Path.GetExtension(filename));
                }
                System.IO.File.WriteAllBytes("C:\\\\uploads\\\\" + filename, buffer);
                filename = (string)null;
                buffer = (byte[])null;
                file = (HttpContent)null;
            }
            return (IHttpActionResult)this.Ok();
        }
        [Route("api/HSSE/RefreshUser")]
        [HttpGet]
        public bool RefreshUser()
        {
            using (var db = new DbContext("GATE"))
            {

                db.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
                employees = db.Database.SqlQuery<Employee>(@"EXEC GetUsers").ToList();

            }

            //using (var db = new DbContext("IM"))
            //{
            //    userRoleTCodes = db.Database.SqlQuery<UserRoleTCode>(@"SELECT * FROM vUserRoleTCode_HSSE_CC AS vurthc").ToList();
            //}

            return true;
        }

        /// <summary>
        /// 获得部门列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="ctype"></param>
        /// <returns></returns>
        [Route("api/HSSE/GetDepartments")]
        [HttpGet]
        public IHttpActionResult GetDepartments(string userid, string ctype)
        {
            DataTable dt = oAC.DbHelper.ExecuteStoredProcedure("GetDepartments",
                        new string[] { "UserID", "Ctype" }, new object[] { userid, ctype }).Tables[0];
            return Ok(_helper.ConvertJson(dt));
        }

        /// <summary>
        /// 获得这个部门的成员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/HSSE/GetEmployeesByDept/{id}")]
        [HttpGet]
        public IHttpActionResult GetEmployeesByDept(string id)
        {
            var result = employees.Where(e => e.DepartmentID == id).ToList();
            return Ok(result);
        }

        [Route("api/HSSE/GetCheckers/{id}")]
        [HttpGet]
        public IHttpActionResult GetCheckers(string id)
        {


            return Ok();
        }

        [Route("api/HSSE/GetEmployeeInfo/{id}")]
        [HttpGet]
        public IHttpActionResult GetEmployeeInfo(string id)
        {
            var linq = from e in employees
                       where e.EmployeeID == id.ToUpper().ToString()
                       select e;
            var result = linq.ToList().FirstOrDefault();
            var membershipUser = allMembershipUsers.FirstOrDefault(u => u.UserName == id);
            if (result != null && membershipUser != null)
            {
                result.Email = membershipUser.Email;
            }
            return Ok(result);
        }
        [HttpGet]
        public IHttpActionResult ValidateUser(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                NBear.Data.Gateway cas = new NBear.Data.Gateway("CAS");
                DataTable dt = cas.DbHelper.Select("SELECT  *  from View_LoginList where Token=@Token", new object[] { token }).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine("ValidateUser:" + token);

                    var result = new
                    {
                        token,

                        username = dt.Rows[0]["UserID"].ToString(),
                        nickname = dt.Rows[0]["UserName"].ToString(),
                        email = dt.Rows[0]["Email"].ToString()
                    };
                    return Ok(result);

                }
            }
            Console.WriteLine("NO ValidateUser:" + token);
            return Unauthorized();

        }





        [HttpGet]
        public IHttpActionResult ValidateUser(string username, string password)
        {

            Console.WriteLine("ValidateUser" + username);

            var validate = Membership.ValidateUser(username, password);

            if (validate)
            {
                //employees for HRMS Data
                if (username.ToLower().Contains("fepv"))
                {
                    username = username.ToUpper();
                }
                var user = employees.Find(e => e.EmployeeID == username);
                if (user != null)
                {
                    var result = new { username = user.EmployeeID, nickname = user.Name, email = user.Email };
                    return Ok(result);
                }
                else
                {
                    var result = new { username = username, nickname = "", email = "" };
                    return Ok(result);

                }
            }
            return Unauthorized();
        }





        [Route("api/HSSE/GetProcessLogs/{id}/{cId?}")]
        [HttpGet]
        public IHttpActionResult GetProcessLogs(string id, string cId = "")
        {
            IMongoQuery query;// = Query<ProcessLog>.EQ(e => e.ProcessInstanceId, id);
            if (cId == "")
            {
                query = Query<ProcessLog>.EQ(e => e.ProcessInstanceId, id);
            }
            else
            {
                var paras = new List<string> { id, cId };
                query = Query<ProcessLog>.In(e => e.ProcessInstanceId, paras);
            }


            var queryResult = collection.Find(query).ToList();
            foreach (var item in queryResult)
            {
                if (item.Key == "GateContractorQuaProcess")
                {
                    foreach (var itemHistoryField in item.HistoryField)
                    {
                        
                    }
                }
            }
            queryResult.ForEach(r =>
            {

                r.UserName = GetUserName(r.UserId);//employees.Find(e => e.EmployeeID == r.UserId) == null ? r.UserId : employees.Find(e => e.EmployeeID == r.UserId).Name;
                r.TaskName = r.TaskName ?? "起始表单";
                r.KeyName = r.KeyName ?? (queryResult.Find(rr => rr.KeyName != null) == null ? string.Empty : queryResult.Find(rr => rr.KeyName != null).KeyName);
                r.HistoryField.ForEach(h => h.Value = h.Value == "False" ? string.Empty : h.Value);
            });

            var result = new List<ProcessDTO>();

            foreach (var q in queryResult.OrderByDescending(o => o.Stamp).GroupBy(e => e.FormatStampMonthly))
            {
                var dto = new ProcessDTO();
                var firstOrDefault = queryResult.FirstOrDefault();
                if (firstOrDefault != null) dto.KeyName = firstOrDefault.KeyName;
                dto.Month = q.Key;
                dto.Logs = new List<ProcessLog>();
                foreach (var v in q)
                {
                    dto.Logs.Add(v);
                }
                result.Add(dto);
            }

            return Ok(result);
            //return Ok(queryResult.OrderBy(r => r.Stamp).ToList().GroupBy(e => e.FormatStampMonthly));
        }
        private string GetUserName(string userid)
        {
            if (userid.Contains("fepv"))
            {

                userid = userid.ToUpper();
            }
            return employees.Find(e => e.EmployeeID == userid) == null ? userid : employees.Find(e => e.EmployeeID == userid).Name;
        }
        [HttpGet]
        public bool CheckTCode(string username, string tcode)
        {

            DataTable o = gateIM.ExecuteStoredProcedure("GetTcode2UserName",
                new string[] { "UserID", "RoleName" }, new object[] { username, tcode }).Tables[0];
            if (o.Rows.Count > 0)
            {
                return true;

            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [Route("api/Gate/Checker/UpdatePassword")]
        [HttpPost]
        public IHttpActionResult UpdatePassword(UpdatePass pass)
        {
            string str = "Password modification failed";
            if (!string.IsNullOrEmpty(pass.username) && !string.IsNullOrEmpty(pass.newPassword) && !string.IsNullOrEmpty(pass.oldP))
            {

                var MembershipService = new AccountMembershipService();
                try
                {
                    Membership.ApplicationName = "MES";
                    if (MembershipService.ChangePassword(pass.username, pass.oldP, pass.newPassword))
                    {
                        str = "Password reset complete";
                    }
                    else
                    {
                        return BadRequest("Password modification failed");
                    }

                }
                catch (Exception eJob)
                {
                    var error = string.Format("Job错误，执行aspnet_Membership_SetPassword出错，错误信息{0}", eJob.Message);
                    var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        ReasonPhrase = error,
                        Content = new StringContent(error)
                    };

                    Loger.Error(eJob);
                    throw new HttpResponseException(resp);
                }
            }

            return Ok(str);

        }
        [Route("api/HSSE/Exception/")]
        [HttpGet]
        public IHttpActionResult Exception()
        {
            throw new Exception("哈哈哈哈，这是异常！！！！");
        }
    }
}
