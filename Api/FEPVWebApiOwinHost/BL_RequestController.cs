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
using FEPVWebApiOwinHost.Filter;

namespace FEPVWebApiOwinHost
{
    [FilterIP]
    [RoutePrefix("api/Gate/BL_Request")]
    public class BL_RequestController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("Beling");
        protected readonly GateContext _GateContext = new GateContext();
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        [Route("BlackInfoByVoucherID")]
        [HttpGet]
        public BL_Request BlackInfoByVoucherID(string voucherid)
        {
            var _BL_Request = _GateContext.BL_Requests.Find(voucherid);
            return _BL_Request;
        }

        [Route("GetBlackList")]
        [HttpGet]
        public IHttpActionResult GetBlackList(string userID, int pageIndex, int pageSize, string des, string voucherId, string status)
        {
            try
            {
                object[] outParameters = null;
                string[] parameters = new string[] { "UserID", "pageIndex", "pageSize", "Des", "VoucherID", "Status" };
                DataTable dt = gate.ExecuteStoredProcedure("BL_HS_GetBlackList_Page", parameters,
                    new object[] { userID, pageIndex, pageSize, des, voucherId, status },
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
        /// 自动生成黑名单信息单据号
        /// </summary>
        public string GetBlackListVoucherIDFlow()
        {
            try
            {
                string getVoucherSql = @"SELECT 'BL' + right(convert(nvarchar(8) , getdate(),112),8) +
			                                right('00000000' + convert(nvarchar(4) , IsNull(Max(right(VoucherID,4))+1,1)) , 4)
			                                FROM dbo.BL_Request 
			                                WHERE VoucherID LIKE 'BL' + right(convert(nvarchar(8) , getdate(),112),8) + '%'";
                return gate.SelectScalar<string>(getVoucherSql, new object[] { });
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("SaveBlackList")]
        [HttpPost]
        /// <summary>
        /// 保存黑名单信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public IHttpActionResult SaveBlackList(BL_Request bl_Request)
        {
            try
            {
                Console.WriteLine("SaveBlackList：" + DateTime.Now.ToString());
                string voucherId = string.IsNullOrEmpty(bl_Request.VoucherID) ? "" : bl_Request.VoucherID;
                var _BL_Request = _GateContext.BL_Requests.Find(voucherId);
                string result = string.Empty;

                if (_BL_Request != null)
                {
                    bl_Request.Stamp = DateTime.Now;
                    //
                    _GateContext.Entry(bl_Request).State = EntityState.Modified;
                }
                else
                {
                    if (string.IsNullOrEmpty(bl_Request.Status))
                    {
                        bl_Request.Status = "";
                    }
                    string vouIdNew = GetBlackListVoucherIDFlow();

                    bl_Request.VoucherID = vouIdNew;
                    bl_Request.Stamp = DateTime.Now;
                    _GateContext.BL_Requests.Add(bl_Request);
                }

                _GateContext.SaveChanges();

                return Ok(bl_Request);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                Console.WriteLine(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [Route("SaveBlackListStatus")]
        [HttpPost]
        public HttpResponseMessage SaveBlackListStatus(BL_Request bl_Request)
        {
            if (string.IsNullOrEmpty(bl_Request.VoucherID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Document number is empty！");
            try
            {
                gate.ExecuteNonQuery(@"UPDATE BL_Request
                                       SET [Status] = @Status
                                          ,[Stamp] = @Stamp
                                    WHERE VoucherID = @VoucherID",
                                         new object[] 
                                 {
                                     bl_Request.Status,
                                     DateTime.Now,                                     
                                     bl_Request.VoucherID,
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

        [Route("AddBlackInfo/{VoucherID}")]
        [HttpPut]
        public HttpResponseMessage AddBlackInfo(string VoucherID)
        {
            Console.WriteLine("AddBlackInfo：" + DateTime.Now.ToString());
            if (string.IsNullOrEmpty(VoucherID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Document number is empty！");
            try
            {
                string msg = gate.DbHelper.ExecuteStoredProcedure("BL_HS_AddBlackInfo",
                    new string[] { "VoucherID" }, new object[] { VoucherID }).Tables[0].Rows[0][0].ToString();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message + e.StackTrace);
            }
        }
    }
}
