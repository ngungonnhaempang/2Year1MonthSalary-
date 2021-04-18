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
    [RoutePrefix("api/Gate/PtaEgTruck")]
    public class PtaEgTruckController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("Beling");
        protected readonly GateContext _GateContext = new GateContext();
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        [Route("PtaEgTruckByVoucherID")]
        [HttpGet]
        /// <summary>
        /// 根据单号获得PTA\EG车辆信息
        /// </summary>
        /// <param name="voucherid"></param>
        /// <returns></returns>
        public PtaEgTruck PtaEgTruckByVoucherID(string voucherid)
        {
            Console.WriteLine("PtaEgTruckByVoucherID：" + DateTime.Now.ToString());
            var _PtaEgTruck = _GateContext.PtaEgTrucks.Find(voucherid);
            //_PtaEgTruck.PtaEgItems = GetSubPtaEgTruckList(voucherid);
            return _PtaEgTruck;
        }

        /// <summary>
        /// 根据VoucherID获取PTA\EG车辆子表信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<PtaEgItem> GetSubPtaEgTruckList(string VoucherID)
        {
            List<PtaEgItem> list = null;
            DataTable dt = gate.ExecuteStoredProcedure("PE_HS_GetSubPtaEgTruckList",
                new string[] { "VoucherID" }, new object[] { VoucherID }).Tables[0];
            if (dt.Rows.Count > 0)
            {
                list = _helper.ConvertList<PtaEgItem>(dt);
            }

            return list;
        }

        [Route("GetPtaEgTruckByItemID")]
        [HttpGet]
        /// <summary>
        /// 根据项次号获得PTA\EG一条车辆信息
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetPtaEgTruckByItemID(string itemId)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("PE_HS_PtaEgTruckByItemID", new string[] { "ItemID" }, new object[] { itemId }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 自动生成PTA\EG车辆信息的单据号
        /// </summary>
        /// <returns></returns>
        public string GetPtaEgTruckVoucherIDFlow()
        {
            try
            {
                string getVoucherSql = @"SELECT 'PE' + right(convert(nvarchar(8) , getdate(),112),8) +
			                                right('00000000' + convert(nvarchar(4) , IsNull(Max(right(VoucherID,4))+1,1)) , 4)
			                                FROM dbo.PtaEgTruck 
			                                WHERE VoucherID LIKE 'PE' + right(convert(nvarchar(8) , getdate(),112),8) + '%'";
                return gate.SelectScalar<string>(getVoucherSql, new object[] { });
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("SavePtaEgTruck")]
        [HttpPost]
        /// <summary>
        /// 保存一条PTA\EG车辆信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public IHttpActionResult SavePtaEgTruck(PtaEgTruck ptaEgTruck)
        {
            try
            {
                Console.WriteLine("SavePtaEgTruck：" + DateTime.Now.ToString());
                string voucherId = string.IsNullOrEmpty(ptaEgTruck.VoucherID) ? "" : ptaEgTruck.VoucherID;
                var _PtaEgTruck = _GateContext.PtaEgTrucks.Find(voucherId);
                string result = string.Empty;

                if (_PtaEgTruck != null)
                {
                    ptaEgTruck.Stamp = DateTime.Now;
                    //
                    RemoveHoldingEntityInContext(ptaEgTruck);
                    //
                    _GateContext.Entry(ptaEgTruck).State = EntityState.Modified;
                }
                else
                {
                    if (string.IsNullOrEmpty(ptaEgTruck.Status))
                    {
                        ptaEgTruck.Status = "";
                    }
                    string vouIdNew = GetPtaEgTruckVoucherIDFlow();

                    ptaEgTruck.VoucherID = vouIdNew;
                    ptaEgTruck.Stamp = DateTime.Now;
                    _GateContext.PtaEgTrucks.Add(ptaEgTruck);
                }

                _GateContext.SaveChanges();

                return Ok(ptaEgTruck);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                Console.WriteLine(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        /// <summary>
        /// 用于监测Context中的Entity是否存在，如果存在，将其Detach，防止出现问题。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Boolean RemoveHoldingEntityInContext(PtaEgTruck entity)
        {
            var objContext = ((IObjectContextAdapter)_GateContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<PtaEgTruck>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }

        [Route("GetPtaEgTrucks")]
        [HttpGet]
        /// <summary>
        /// 分页查询PTA\EG车辆信息
        /// </summary>
        public IHttpActionResult GetPtaEgTrucks(string userID, int pageIndex, int pageSize, string des, string voucherId, string status, string manufacturer, DateTime? dateFrom, DateTime? dateTo)
        {
            Console.WriteLine("GetPtaEgTrucks：" + DateTime.Now.ToString());
            try
            {//CC_Q_GetPtaEgTrucks
                object[] outParameters = null;
                string[] parameters = new string[] { "UserID", "pageIndex", "pageSize", "Des", "VoucherID", "Status", "Manufacturer", "dateFrom", "dateTo" };
                DataTable dt = gate.ExecuteStoredProcedure("PE_HS_GetPtaEgTrucks_Page", parameters,
                    new object[] { userID, pageIndex, pageSize, des, voucherId, status, manufacturer, dateFrom, dateTo },
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

        [Route("DeletePtaEgTrucks")]
        [HttpPost]
        /// <summary>
        /// 删除PTA\EG车辆信息
        /// </summary>
        /// <param name="voucherIds"></param>
        /// <returns></returns>
        public HttpResponseMessage DeletePtaEgTrucks(string userId, string voucherId)
        {
            Console.WriteLine("DeletePtaEgTrucks：" + DateTime.Now.ToString());
            try
            {
                string msg = gate.DbHelper.ExecuteStoredProcedure("CC_J_CanDelete",
                    new string[] { "UserID", "VoucherID" }, new object[] { userId, voucherId }).Tables[0].Rows[0][0].ToString();
                if (string.IsNullOrEmpty(msg))
                {
                    string sql = "UPDATE PtaEgTruck SET [Status] = 'X' WHERE VoucherID=@VoucherID AND [Status] IN ('','N','Q')";
                    gate.DbHelper.ExecuteNonQuery(sql, new object[] { voucherId });

                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, msg);
                }
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
        [Route("UpdatePtaEgTruckStatus")]
        [HttpPut]
        public HttpResponseMessage UpdatePtaEgTruckStatus(string voucherID, string status)
        {
            if (string.IsNullOrEmpty(voucherID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Document number is empty！");
            try
            {
                gate.ExecuteNonQuery(@"UPDATE PtaEgTruck
                                       SET [Status] = @Status,[Stamp] = @Stamp
                                       WHERE VoucherID = @VoucherID",
                                      new object[] { status, DateTime.Now, voucherID });

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
        /// 异常原因说明
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        [Route("WritePtaEgTruckReason")]
        [HttpGet]
        public IHttpActionResult WritePtaEgTruckReason(string itemID, string message, string reason)
        {
            Console.WriteLine("WritePtaEgTruckReason：" + DateTime.Now.ToString());
            Dictionary<string, string> values = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(itemID))
            {
                values.Add("msg", "单据号为空！");
                return Ok(values);
            }
            try
            {
                gate.ExecuteNonQuery(@"UPDATE [PtaEgItem]
                                       SET [Message] = @Message, [Reason] = @Reason, [Stamp] = @Stamp, [Complete] = @Complete
                                       WHERE ItemID = @ItemID",
                                        new object[] { message, reason, DateTime.Now, DateTime.Now, itemID });
                int count = gate.SelectScalar<int>(@"SELECT COUNT(*)  FROM [PtaEgItem]
                                WHERE [Message]<>'' AND Reason<>'' AND ItemID = @ItemID ", new object[] { itemID });
                if (count > 0)
                    values.Add("msg", "");
                else
                    values.Add("msg", "原因不能为空，保存失败！");
                return Ok(values);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetPtaEgTypes")]
        [HttpGet]
        /// <summary>
        /// 获取PTA\EG类型
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetPtaEgTypes(string Language,string Type)
        {
            try
            {//D_PtaEgs
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("PE_HS_GetPtaEgTypes", new string[] { "Language","Type"}, new object[] { Language,Type}).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
        [Route("getPtaEgTruckByVehicleNO")]
        [HttpGet]
        /// <summary>
        /// getPtaEgTruckByVehicleNO by marco
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult getPtaEgTruckByVehicleNO(string VehicleNO,string Ctype, string Language )
        {
            try
            {//D_PtaEgs
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("getPtaEgTruckByVehicleNO", new string[] { "VehicleNO", "CType", "Language" }, new object[] { VehicleNO, Ctype, Language }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
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
