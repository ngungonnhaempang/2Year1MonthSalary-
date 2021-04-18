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
     [RoutePrefix("api/Gate/Goods")]
    public class GoodsController : ApiController
    {
         private NBear.Data.Gateway gate = new NBear.Data.Gateway("Beling");
        protected readonly GateContext _GateContext = new GateContext();
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        #region Goods

        [Route("GoodsByVoucherID")]
        [HttpGet]
        public Goods GoodsByVoucherID(string voucherid)
        {
            var _Goods = _GateContext.Goodss.Find(voucherid);
            return _Goods;
        }

        [Route("GetGoodsByVoucherID")]
        [HttpGet]
        /// <summary>
        /// 根据单号获得物品出厂信息
        /// </summary>
        /// <param name="voucherId"></param>
        /// <returns></returns>
        public IHttpActionResult GetGoodsByVoucherID(string VoucherID, string language)
        {
            try
            {
                List<Goods> list = GetGoods(VoucherID, language);
                return Ok(list);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        List<Goods> GetGoods(string voucherId, string language)
        {
            try
            {
                List<Goods> list = new List<Goods>();
                DataTable dt = gate.ExecuteStoredProcedure("GD_HS_GetGoodsByVoucherID",
                    new string[] { "VoucherID", "Language" }, new object[] { voucherId, language }).Tables[0];

                list = _helper.ConvertList<Goods>(dt);
                foreach (var item in list)
                {
                    item.GoodsItems = GetSubGoodsList(item.VoucherID, language);
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
        /// 根据VoucherID获取物品名称等信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<GoodsItem> GetSubGoodsList(string VoucherID, string language)
        {
            List<GoodsItem> list = null;
            DataTable dt = gate.ExecuteStoredProcedure("GD_HS_GetSubGoodsList",
                new string[] { "VoucherID", "Language" }, new object[] { VoucherID, language }).Tables[0];
            if (dt.Rows.Count > 0)
            {
                list = _helper.ConvertList<GoodsItem>(dt);
            }

            return list;
        }

        [Route("GetGoodsByItemID")]
        [HttpGet]
        /// <summary>
        /// 根据单号获得物品出厂信息
        /// </summary>
        /// <param name="voucherId"></param>
        /// <returns></returns>
        public IHttpActionResult GetGoodsByItemID(string ItemID, string language)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("GD_HS_GetGoodsByItemID",
                    new string[] { "ItemID", "Language" }, new object[] { ItemID, language }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetGoods")]
        [HttpGet]
        public IHttpActionResult GetGoods(string userID, int pageIndex, int pageSize, string des, DateTime? dateFrom, DateTime? dateTo, string voucherId, string status)
        {
            try
            {
                object[] outParameters = null;
                string[] parameters = new string[] { "UserID", "pageIndex", "pageSize", "Des", "VoucherID", "Status", "dateFrom", "dateTo" };
                DataTable dt = gate.ExecuteStoredProcedure("GD_HS_GetGoods_Page", parameters, 
                    new object[] { userID, pageIndex, pageSize, des, voucherId, status,dateFrom,dateTo }, 
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
        /// 自动生成物品出厂信息单据号
        /// </summary>
        /// <returns></returns>
        public string GetGoodsVoucherIDFlow()
        {
            try
            {
                string getVoucherSql = @"SELECT 'GD' + right(convert(nvarchar(8) , getdate(),112),8) +
			                                right('00000000' + convert(nvarchar(4) , IsNull(Max(right(VoucherID,4))+1,1)) , 4)
			                                FROM dbo.Goods 
			                                WHERE VoucherID LIKE 'GD' + right(convert(nvarchar(8) , getdate(),112),8) + '%'";
                return gate.SelectScalar<string>(getVoucherSql, new object[] { });
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("SaveGoodsInfo")]
        [HttpPost]
        /// <summary>
        /// 保存物品出厂信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public IHttpActionResult SaveGoodsInfo(Goods goods)
        {
            try
            {
                Console.WriteLine("SaveGoodsInfo：" + DateTime.Now.ToString());
                string voucherId = string.IsNullOrEmpty(goods.VoucherID) ? "" : goods.VoucherID;
                var _Goods = _GateContext.Goodss.Find(voucherId);
                string result = string.Empty;

                if (_Goods != null)
                {
                    //移除原Item项次
                    while (_Goods.GoodsItems.Count > 0)
                    {
                        _GateContext.GoodsItems.Remove(_Goods.GoodsItems[0]);
                    }
                    //新增Item项次
                    foreach (GoodsItem item in goods.GoodsItems)
                    {
                        item.ID = Guid.NewGuid();
                        item.VoucherID = goods.VoucherID;
                        _GateContext.GoodsItems.Add(item);
                    }

                    goods.Stamp = DateTime.Now;
                    //
                    RemoveHoldingEntityInContext(goods);
                    //
                    _GateContext.Entry(goods).State = EntityState.Modified;
                }
                else
                {
                    if (string.IsNullOrEmpty(goods.Status))
                    {
                        goods.Status = "";
                    }
                    string vouIdNew = GetGoodsVoucherIDFlow();
                    foreach (GoodsItem item in goods.GoodsItems)
                    {
                        item.ID = Guid.NewGuid();
                        item.VoucherID = vouIdNew;
                        _GateContext.GoodsItems.Add(item);
                    }
                    
                    goods.VoucherID = vouIdNew;
                    goods.Stamp = DateTime.Now;
                    _GateContext.Goodss.Add(goods);
                    
                }

                _GateContext.SaveChanges();
                return Ok(goods);
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        /// <summary>
        /// 用于监测Context中的Entity是否存在，如果存在，将其Detach，防止出现问题。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Boolean RemoveHoldingEntityInContext(Goods entity)
        {
            var objContext = ((IObjectContextAdapter)_GateContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<Goods>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }

         /// <summary>
         /// 修改状态为Q
         /// </summary>
         /// <param name="voucherID"></param>
         /// <returns></returns>
        [Route("UpdateGoodsStatus")]
        [HttpPut]
        public HttpResponseMessage UpdateGoodsStatus(string voucherID,string status)
        {
            if (string.IsNullOrEmpty(voucherID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Document number is empty！");
            try
            {
                gate.ExecuteNonQuery(@"UPDATE Goods
                                       SET 
                                          [Status] = @Status
                                          ,[Stamp] = @Stamp
                                    WHERE VoucherID = @VoucherID",
                                      new object[] 
                                 {
                                 status,
                                     DateTime.Now, voucherID  
                                    
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

        [Route("SaveGoodsBackItemStatus")]
        [HttpPost]
        public HttpResponseMessage SaveGoodsBackItemStatus(GoodsBackItem goodsBackItem)
        {
            if (string.IsNullOrEmpty(goodsBackItem.ItemID.ToString()) || string.IsNullOrEmpty(goodsBackItem.Status))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "ItemID is empty or Status is empty！");
            try
            {
                if (goodsBackItem.Status == "W" || goodsBackItem.Status == "N")
                {
                    gate.ExecuteNonQuery(@"UPDATE GoodsBackItem
                                       SET [Complete] = @Complete
                                          ,[Status] = @Status
                                          ,[Stamp] = @Stamp
                                    WHERE ItemID = @ItemID",
                                         new object[] 
                                 {
                                     DateTime.Now,
                                     goodsBackItem.Status,
                                     DateTime.Now,                                     
                                     goodsBackItem.ItemID.ToString(),
                                 });
                }
                else
                {
                    gate.ExecuteNonQuery(@"UPDATE GoodsBackItem
                                       SET [Status] = @Status
                                          ,[Stamp] = @Stamp
                                    WHERE ItemID = @ItemID",
                                         new object[] 
                                 {
                                     goodsBackItem.Status,
                                     DateTime.Now,                                     
                                     goodsBackItem.ItemID,
                                 });
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

        [Route("DeleteGoods")]
        [HttpDelete]
        /// <summary>
        /// 删除物品出厂信息
        /// </summary>
        /// <param name="voucherIds"></param>
        /// <returns></returns>
        public HttpResponseMessage DeleteGoods(string voucherId)
        {
           
            try
            {
                //foreach (var v in voucherIds)
                //{
                    string sql = "UPDATE Goods SET [Status] = 'X' WHERE VoucherID=@VoucherID AND [Status] IN ('','N')";
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

        [Route("GetGoodsTypes")]
        [HttpGet]
        public IHttpActionResult GetGoodsTypes(string language)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("GD_HS_GoodsType",
                    new string[] { "Language" }, new object[] { language }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetUnits")]
        [HttpGet]
        public IHttpActionResult GetUnits(string language)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("GD_HS_Unit4Goods",
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
        /// 判断物品是否可以在非正常工作日出厂
        /// </summary>
        /// <param name="goodsType"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        [Route("CanOutIn5X8")]
        [HttpGet]
        public IHttpActionResult CanOutIn5X8(DateTime day, string goodsType)
        {
            string msg = gate.ExecuteStoredProcedure("OS_CanOutIn5X8", new string[]{
                    "UserID","Day","GoodsType"}, new object[] { "", day, goodsType }).Tables[0].Rows[0][0].ToString();

            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("msg", msg);
            return Ok(values);
        }

        [Route("GetOutReasons")]
        [HttpGet]
        public IHttpActionResult GetOutReasons(bool isBack, string language)
        {
            try
            {
                Console.WriteLine("GetOutReasons：" + DateTime.Now.ToString());
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("GD_HS_GetOutReasons",
                    new string[] { "IsBack", "Language" }, new object[] { isBack, language }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("SaveWorkFlowLogs")]
        [HttpPost]
        public HttpResponseMessage SaveWorkFlowLogs(WorkFlowLogs workFlowLogs)
        {
            Console.WriteLine("SaveWorkFlowLogs：" + DateTime.Now.ToString());
            try
            {
                workFlowLogs.ID = Guid.NewGuid();
                workFlowLogs.Stamp = DateTime.Now;
                _GateContext.WorkFlowLogS.Add(workFlowLogs);

                _GateContext.SaveChanges();
                Console.WriteLine("SaveChanges OK：" + DateTime.Now.ToString());

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Loger.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
        #endregion
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
