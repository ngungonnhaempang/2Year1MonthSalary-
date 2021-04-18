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

namespace FEPVWebApiOwinHost
{
     [FEPVWebApiOwinHost.Filter.FilterIP]
    [RoutePrefix("api/Gate/Guest")]
    public class GuestController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("Beling");
        protected readonly GateContext _GateContext = new GateContext();
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        #region Guest

        [Route("EmployeeName")]
        [HttpGet]
        /// <summary>
        /// 根据工号得到员工姓名
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public IHttpActionResult GetNameByEmployeeID(string UserID, string EmployeeID)
        {
            DataTable o = gate.ExecuteStoredProcedure("FK_HS_GetEmployeeNameByEmployeeID",
                  new string[] { "UserID", "EmployeeID" }, new object[] { null, EmployeeID }).Tables[0];

            return Ok(o);
        }

        [Route("EmployeeInfo")]
        [HttpGet]
        /// <summary>
        /// 根据姓名得到员工信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public IHttpActionResult GetEmployeeInfoByName(string UserID, string Name)
        {
            DataTable o = gate.ExecuteStoredProcedure("FK_HS_GetEmployeeInfoByName",
                  new string[] { "UserID", "Name" }, new object[] { null, Name }).Tables[0];

            return Ok(o);
        } 

        [Route("GetGuestByVoucherID")]
        [HttpGet]
        /// <summary>
        /// 根据单号获得访客进出厂信息
        /// </summary>
        /// <param name="voucherId"></param>
        /// <returns></returns>
        public IHttpActionResult GetGuestByVoucherID(string VoucherID, string language)
        {
            try
            {
                List<Guest> list = GetGuest(VoucherID, language);
                return Ok(list);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        List<Guest> GetGuest(string voucherId, string language) 
        {
            try
            {
                List<Guest> list = new List<Guest>();
                DataTable dt = gate.ExecuteStoredProcedure("FK_HS_GetGuestByVoucherID",
                    new string[] { "VoucherID", "Language" }, new object[] { voucherId, language }).Tables[0];

                list = _helper.ConvertList<Guest>(dt);
                foreach (var item in list)
                {
                    item.GuestItems = GetSubGuestList(item.VoucherID);
                }
                return list;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据VoucherID获取访客姓名，卡号等信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<GuestItem> GetSubGuestList(string VoucherID)
        {
            List<GuestItem> list = null;
            DataTable dt = gate.ExecuteStoredProcedure("FK_HS_GetSubGuestList",
                new string[] { "VoucherID" }, new object[] { VoucherID }).Tables[0];
            if (dt.Rows.Count > 0)
            {
                list = _helper.ConvertList<GuestItem>(dt);
            }

            return list;
        }

        [Route("GetGuests")]
        [HttpGet]
        public IHttpActionResult GetGuests(string userID, int pageIndex, int pageSize, string des, string voucherId, string status, string enterprise, DateTime? dateFrom, DateTime? dateTo, string region, string guestType)
        {
            Console.WriteLine("GetGuests：" + DateTime.Now.ToString());
            try
            {
                object[] outParameters = null;
                string[] parameters = new string[] { "UserID", "pageIndex", "pageSize", "Des", "VoucherID", "Status", "Enterprise", "region", "guestType", "dateFrom", "dateTo" };
                DataTable dt = gate.ExecuteStoredProcedure("FK_HS_GetGuests_Page_language", parameters, 
                    new object[] { userID, pageIndex, pageSize, des, voucherId, status, enterprise, region, guestType, dateFrom, dateTo }, 
                    new string[] { "Count" }, new DbType[] { DbType.Int32 }, out outParameters).Tables[0];

                Dictionary<string, Object> values = new Dictionary<string, object>();
                values.Add("TableData", _helper.ConvertJson(dt));
                values.Add("TableCount", outParameters);
                return Ok(values);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// 自动生成访客进出厂信息单据号
        /// </summary>
        /// <returns></returns>
        public string GetGuestInfoVoucherIDFlow()
        {
            try
            {
                string getVoucherSql = @"SELECT 'FK' + right(convert(nvarchar(8) , getdate(),112),8) +
			        right('00000000' + convert(nvarchar(4) , IsNull(Max(right(VoucherID,4))+1,1)) , 4)
			        FROM dbo.Guest 
			        WHERE VoucherID LIKE 'FK' + right(convert(nvarchar(8) , getdate(),112),8) + '%'";

                return gate.SelectScalar<string>(getVoucherSql, new object[] { });
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("SaveGuestInfo")]
        [HttpPost]
        /// <summary>
        /// 保存访客进出厂信息
        /// </summary>
        /// <param name="guest"></param>
        /// <returns></returns>
        public IHttpActionResult SaveGuestInfo(Guest guest)
        {
            try
            {
                Console.WriteLine("SaveGuestInfo：" + DateTime.Now.ToString());
                string voucherId = string.IsNullOrEmpty(guest.VoucherID) ? "" : guest.VoucherID;
                var _Guest = _GateContext.Guests.Find(voucherId);
                string result = string.Empty;

                if (_Guest != null)
                {                    
                    //移除原Item项次
                    while (_Guest.GuestItems.Count > 0)
                    {
                        _GateContext.GuestItems.Remove(_Guest.GuestItems[0]);
                    }
                    //新增Item项次
                    foreach (GuestItem item in guest.GuestItems)
                    {
                        item.Image = null;
                        item.ID = Guid.NewGuid();
                        item.VoucherID = guest.VoucherID;
                        _GateContext.GuestItems.Add(item);
                    }

                    guest.Stamp = DateTime.Now;
                    //
                    RemoveHoldingEntityInContext(guest);                   
                    //
                    _GateContext.Entry(guest).State = EntityState.Modified;
                }
                else
                {
                    if (string.IsNullOrEmpty(guest.Status))
                    {
                        guest.Status = "";
                    }
                    string vouIdNew = GetGuestInfoVoucherIDFlow();
                    foreach (GuestItem item in guest.GuestItems)
                    {
                        item.Image = null;
                        item.ID = Guid.NewGuid();
                        item.VoucherID = vouIdNew;
                        _GateContext.GuestItems.Add(item);
                    }

                    guest.VoucherID = vouIdNew;
                    guest.Stamp = DateTime.Now;
                    _GateContext.Guests.Add(guest);
                }

                _GateContext.SaveChanges();

                return Ok(guest);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                
                return BadRequest(e.Message+e.StackTrace);              
            }
        }

        /// <summary>
        /// 用于监测Context中的Entity是否存在，如果存在，将其Detach，防止出现问题。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Boolean RemoveHoldingEntityInContext(Guest entity)
        {
            var objContext = ((IObjectContextAdapter)_GateContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<Guest>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }

        public string InsertGuest(Guest guest)
        {
            if (string.IsNullOrEmpty(guest.Status))
            {
                guest.Status = "F";
            }

            string sql = "DELETE FROM GuestItem WHERE VoucherID=@VoucherID";
            gate.DbHelper.ExecuteNonQuery(sql, new object[] { guest.VoucherID });

            string voucherId = GetGuestInfoVoucherIDFlow();
            foreach (GuestItem item in guest.GuestItems)
            {
                item.Image = null;
                item.ID = Guid.NewGuid();
                item.VoucherID = voucherId;
                _GateContext.GuestItems.Add(item);
            }

            guest.VoucherID = voucherId;
            guest.Stamp = DateTime.Now;
            _GateContext.Guests.Add(guest);

            _GateContext.SaveChanges();

            return guest.VoucherID;
        }

        [Route("SaveGuestStatus")]
        [HttpPost]
        public HttpResponseMessage UpdateGuest(Guest guest)
        {
            if (string.IsNullOrEmpty(guest.VoucherID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Document number is empty！");
            try
            {
                gate.ExecuteNonQuery(@"UPDATE Guest
                                       SET [Complete] = @Complete
                                          ,[Status] = @Status
                                          ,[Stamp] = @Stamp
                                    WHERE VoucherID = @VoucherID",
                                         new object[] 
                                 {
                                     guest.Complete,
                                     guest.Status,
                                     DateTime.Now,                                     
                                     guest.VoucherID,
                                 });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message + e.StackTrace);
            }
        }

        [Route("DeleteGuests")]
        [HttpDelete]
        /// <summary>
        /// 删除访客进出厂信息
        /// </summary>
        /// <param name="voucherIds"></param>
        /// <returns></returns>
        public HttpResponseMessage DeleteGuests(string voucherId)
        {
            try
            {
                //foreach (var v in voucherIds)
                //{
                    string sql = "UPDATE Guest SET [Status] = 'X' WHERE VoucherID=@VoucherID AND [Status] IN ('','N')";
                    gate.DbHelper.ExecuteNonQuery(sql, new object[] { voucherId });
                //}
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
        /// <summary>
        /// 修改状态为Q
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        [Route("UpdateGuestStatus")]
        [HttpPut]
        public HttpResponseMessage UpdateGuestStatus(string voucherID, string status)
        {
            if (string.IsNullOrEmpty(voucherID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Document number is empty！");
            try
            {
                gate.ExecuteNonQuery(@"UPDATE Guest SET [Status] = @Status ,[Stamp] = @Stamp WHERE VoucherID = @VoucherID",
                                      new object[]  {   status,  DateTime.Now, voucherID         });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message + e.StackTrace);
            }
        }
        [Route("GetRegions")]

        [HttpGet]
        public IHttpActionResult GetRegions(string language)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("FK_HS_Regions_language",
                    new string[] { "Language" }, new object[] { language }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetGuestTypes")]
        [HttpGet]
        public IHttpActionResult GetGuestTypes(string language)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("FK_HS_GuestType_language",
                    new string[] { "Language" }, new object[] { language }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
         /// <summary>
         /// GetEmailByUSerID
         /// </summary>
         /// <param name="disposing"></param>
         /// 
        [Route("getEmailbyUserID")]
        [HttpGet]
        /// <summary>
        /// 根据单号获得访客进出厂信息
        /// </summary>
        /// <param name="voucherId"></param>
        /// <returns></returns>
        public IHttpActionResult getEmailbyUserID(string UserID)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("getDatasendMail",
                    new string[] { "UserID" }, new object[] { UserID }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
         /// <summary>
         /// getGuestInfo
         /// </summary>
         /// <param name="disposing"></param>
        [Route("getGuestInfo")]
        [HttpGet]
        /// <summary>
        /// 根据单号获得访客进出厂信息
        /// </summary>
        /// <param name="voucherId"></param>
        /// <returns></returns>
        public IHttpActionResult getGuestInfo(string UserID,string VoucherID)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("EGT2_GetGuestInformation",
                    new string[] { "UserID","VoucherID" }, new object[] { UserID,VoucherID }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
         /// <summary>
         /// 获得该用户的部门
         /// </summary>
         /// <param name="EmployeeID"></param>
         /// <returns></returns>
        [Route("checkUserBelongDyeing")]
        [HttpGet]
        public IHttpActionResult checkUserBelongDyeing(string EmployeeID)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("checkUserBelongDyeing",
                    new string[] { "EmployeeID" }, new object[] { EmployeeID }).Tables[0];

               
                return Ok(dt);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
