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

namespace FEPVWebApiOwinHost
{
    [FEPVWebApiOwinHost.Filter.FilterIP]
    [RoutePrefix("api/Gate/UnJointTruck")]
    public class UnJointTruckController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("Beling");
        protected readonly GateContext _GateContext = new GateContext();
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        [Route("UnJointTruckByVoucherID")]
        [HttpGet]
        /// <summary>
        /// 根据单号获得一条非系统协同车辆信息
        /// </summary>
        /// <param name="voucherid"></param>
        /// <returns></returns>
        public UnJointTruck UnJointTruckByVoucherID(string voucherid)
        {
            Console.WriteLine("UnJointTruckByVoucherID：" + DateTime.Now.ToString());
            var _UnJointTruck = _GateContext.UnJointTrucks.Find(voucherid);

            return _UnJointTruck;
        }

        /// <summary>
        /// 自动生成非系统协同车辆信息的单据号
        /// </summary>
        /// <returns></returns>
        public string GetUnjointTruckVoucherIDFlow()
        {
            try
            {
                string getVoucherSql = @"SELECT 'FX' + right(convert(nvarchar(8) , getdate(),112),8) +
			                                right('00000000' + convert(nvarchar(4) , IsNull(Max(right(VoucherID,4))+1,1)) , 4)
			                                FROM dbo.UnJointTruck 
			                                WHERE VoucherID LIKE 'FX' + right(convert(nvarchar(8) , getdate(),112),8) + '%'";
                return gate.SelectScalar<string>(getVoucherSql, new object[] { });
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }




        [Route("SaveUnJointTruckList")]
        [HttpPost]
        /// <summary>
        /// 保存一条非系统协同车辆信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public IHttpActionResult SaveUnJointTruckList(Container truck)
        {
            JsonObject json = new JsonObject();
            Console.WriteLine("SaveUnJointTruckList：" + DateTime.Now.ToString());
            List<UnJointTruck> newUnJointTruck = new List<UnJointTruck>();
            StringBuilder msg = new StringBuilder();
            List<UnJointTruck> _newUnJointTruck = new List<UnJointTruck>();
            foreach (var unJointTruck in truck.Trucks)
            {
                if (unJointTruck.VehicleType == "19")
                {
                    if (!CheckContainerisExist(unJointTruck.OrderNO, unJointTruck.StorageNO))
                    {
                        newUnJointTruck.Add(unJointTruck);
                    }
                    else
                    {
                        msg.Append(string.Format("{0} - {1} already exist, that can not add!", unJointTruck.OrderNO, unJointTruck.StorageNO));

                    }

                }
            }

            foreach (var unjoin in newUnJointTruck)
            {
                try
                {
                    var _UnJointTruck = string.IsNullOrEmpty(unjoin.VoucherID) ? null : _GateContext.UnJointTrucks.Find(unjoin.VoucherID);
                    if (_UnJointTruck != null)
                    {
                        unjoin.Stamp = DateTime.Now;
                        RemoveHoldingEntityInContext(unjoin);
                        _GateContext.Entry(unjoin).State = EntityState.Modified;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(unjoin.Status))
                        {
                            unjoin.Status = "N";
                        }
                        string vouIdNew = GetUnjointTruckVoucherIDFlow();
                        unjoin.VoucherID = vouIdNew;
                        unjoin.Stamp = DateTime.Now;
                        _GateContext.UnJointTrucks.Add(unjoin);
                    }
                    _GateContext.SaveChanges();
                    _newUnJointTruck.Add(unjoin);

                }
                catch (Exception ex)
                {

                    msg.Append(ex.Message);
                }
            }

            // }
            json["Error"] = msg.ToString();
            json["Data"] = _newUnJointTruck;
            return Ok(json);



        }
        [Route("SaveUnJointTruck")]
        [HttpPost]
        /// <summary>
        /// 保存一条非系统协同车辆信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public IHttpActionResult SaveUnJointTruck(UnJointTruck unJointTruck)
        {
            try
            {
                Console.WriteLine("SaveUnJointTruck：" + DateTime.Now.ToString());
                string voucherId = string.IsNullOrEmpty(unJointTruck.VoucherID) ? "" : unJointTruck.VoucherID;

                if (unJointTruck.VehicleType == "19" && CheckContainerisExist(unJointTruck.OrderNO, unJointTruck.StorageNO))
                {
                    return BadRequest(string.Format("Container with {0} - {1} already exist, that can not add!", unJointTruck.OrderNO, unJointTruck.StorageNO));
                }
                var _UnJointTruck = _GateContext.UnJointTrucks.Find(voucherId);
                string result = string.Empty;

                if (_UnJointTruck != null)
                {
                    unJointTruck.Stamp = DateTime.Now;
                    //
                    RemoveHoldingEntityInContext(unJointTruck);
                    //
                    _GateContext.Entry(unJointTruck).State = EntityState.Modified;
                }
                else
                {
                    if (string.IsNullOrEmpty(unJointTruck.Status))
                    {
                        unJointTruck.Status = "";
                    }
                    string vouIdNew = GetUnjointTruckVoucherIDFlow();

                    unJointTruck.VoucherID = vouIdNew;
                    unJointTruck.Stamp = DateTime.Now;
                    _GateContext.UnJointTrucks.Add(unJointTruck);
                }

                _GateContext.SaveChanges();

                return Ok(unJointTruck);
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
        private Boolean RemoveHoldingEntityInContext(UnJointTruck entity)
        {
            var objContext = ((IObjectContextAdapter)_GateContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<UnJointTruck>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }

        [Route("GetUnJointTrucks")]
        [HttpGet]
        /// <summary>
        /// 分页查询非系统协同车辆信息
        /// </summary>
        public IHttpActionResult GetUnJointTrucks(string userID, int pageIndex, int pageSize, string des, string voucherId, string status, string manufacturer, string ContainerNO, DateTime? dateFrom, DateTime? dateTo, string vehicleType,string BLNO)
        {
            Console.WriteLine("GetUnJointTrucks：" + DateTime.Now.ToString());
            try
            {//CC_Q_GetUnjointTrucks
                object[] outParameters = null;
                string[] parameters = new string[] { "UserID", "pageIndex", "pageSize", "Des", "VoucherID", "Status", "Manufacturer", "ContainerNO", "dateFrom", "dateTo", "VehicleType","BLNO" };
                DataTable dt = gate.ExecuteStoredProcedure("FX_HS_GetUnJointTrucks_Page", parameters,
                    new object[] { userID, pageIndex, pageSize, des, voucherId, status, manufacturer, ContainerNO, dateFrom, dateTo, vehicleType, BLNO },
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
        [Route("GetOldContainerNO")]
        [HttpGet]
        /// <summary>
        /// Create by Marco 24/03/2018
        /// </summary>
        public IHttpActionResult GetOldContainerNO(string userID)
        {
            Console.WriteLine("GetOldContainerNO：" + DateTime.Now.ToString());
            try
            {//CC_Q_GetUnjointTrucks

                string[] parameters = new string[] { "UserID" };
                DataTable dt = gate.ExecuteStoredProcedure("FX_CON_GetOldContainer", parameters,
                    new object[] { userID }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("DeleteUnJointTrucks")]
        [HttpPost]
        /// <summary>
        /// 删除非系统协同车辆信息
        /// </summary>
        /// <param name="voucherIds"></param>
        /// <returns></returns>
        public HttpResponseMessage DeleteUnJointTrucks(string userId, string voucherId)
        {
            Console.WriteLine("DeleteUnJointTrucks：" + DateTime.Now.ToString());
            try
            {
                string msg = gate.DbHelper.ExecuteStoredProcedure("CC_J_CanDelete",
                    new string[] { "UserID", "VoucherID" }, new object[] { userId, voucherId }).Tables[0].Rows[0][0].ToString();
                if (string.IsNullOrEmpty(msg))
                {
                    string sql = "UPDATE UnJointTruck SET [Status] = 'X' WHERE VoucherID=@VoucherID AND [Status] IN ('N','Q')";
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
        /// Update   'VehicleNO'and 'Container'
        /// </summary>
        /// <param name="VoucherID">DOC ID</param>
        /// <param name="VehicleNO">Truck NO</param>
        /// <param name="Container">Container</param>
        /// <param name="Flag">'1' for  'Container'  '2' for 'VehicleNO' 
        /// '3' for 'VehicleNO'and'Container'</param>
        /// <returns></returns>
        [Route("UpdateVehicleContainer")]
        [HttpPut]
        public HttpResponseMessage UpdateVehicleContainer(string VoucherID, string VehicleNO, string Container, string Remark)
        {
            if (string.IsNullOrEmpty(VoucherID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Document number is empty！");
            try
            {
                int Flag = 0;
                if (!string.IsNullOrEmpty(Container))
                {
                    Flag = 1;
                } if (!string.IsNullOrEmpty(VehicleNO))
                {
                    Flag = 2;
                } if (!string.IsNullOrEmpty(VehicleNO) && !string.IsNullOrEmpty(Container))
                {
                    Flag = 3;
                }
                string msg = gate.DbHelper.ExecuteStoredProcedure("U_UnJointTruck_VehicleNOandContainer"
                , new string[] { "VoucherID", "VehicleNO", "Container", "Remark", "Flag" }
                , new object[] { VoucherID, VehicleNO, Container, Remark, Flag }).Tables[0].Rows[0]["MSG"].ToString();

                if (string.IsNullOrEmpty(msg))
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, msg);

            }
            catch (Exception e)
            {
                Loger.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message + e.StackTrace);
            }

        }






        /// <summary>
        /// 修改状态为Q
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        [Route("UpdateUnJointTruckStatus")]
        [HttpPut]
        public HttpResponseMessage UpdateUnJointTruckStatus(string voucherID, string status)
        {
            if (string.IsNullOrEmpty(voucherID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Document number is empty！");
            try
            {
                gate.ExecuteNonQuery(@"UPDATE UnJointTruck
                                       SET 
                                          [Status] = @Status
                                          ,[Stamp] = @Stamp
                                    WHERE VoucherID = @VoucherID",
                                      new object[]
                                 {
                                     status,
                                     DateTime.Now,
                                     voucherID
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

        [Route("SaveUnJointTruckStatus")]
        [HttpPost]
        public HttpResponseMessage SaveUnJointTruckStatus(UnJointTruck unJointTruck)
        {
            if (string.IsNullOrEmpty(unJointTruck.VoucherID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Document number is empty！");
            try
            {
                gate.ExecuteNonQuery(@"UPDATE UnJointTruck
                                       SET [Status] = @Status
                                          ,[Stamp] = @Stamp
                                    WHERE VoucherID = @VoucherID",
                                         new object[]
                                 {
                                     unJointTruck.Status,
                                     DateTime.Now,
                                     unJointTruck.VoucherID,
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

        [Route("GetVehicleTypes")]
        [HttpGet]
        /// <summary>
        /// 获取车型信息
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetVehicleTypes(string Language, string Type)
        {
            try
            {//D_VehicleType4UnJointTruck
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("FX_HS_GetVehicleTypes", new string[] { "Language", "Type" }, new object[] { Language, Type }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetLevelType")]
        [HttpGet]
        /// <summary>
        /// 获取车型信息
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetLevelType(string VehicleType, string OrderNO, DateTime ExpectIn)
        {
            Console.WriteLine("GetLevelType：" + DateTime.Now.ToString());
            try
            {
                Console.WriteLine("VehicleType：" + VehicleType);
                Console.WriteLine("OrderNO：" + OrderNO);
                Console.WriteLine("ExpectIn：" + ExpectIn);

                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("OS_GetLevelType_UnJointTruck"
                    , new string[] { "VehicleType", "OrderNO", "ExpectIn" }
                    , new object[] { VehicleType, OrderNO, ExpectIn }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
        [Route("UnjoinTruck_Q_PTA_Transport")]
        [HttpGet]
        public IHttpActionResult UnjoinTruck_Q_PTA_Transport(string language)
        {
            try
            {
                DataTable dtreuslt = gate.DbHelper.ExecuteStoredProcedure(@"Q_PTA_Transport", new string[] { "Language" }, new object[] { language }).Tables[0];
                return Ok(dtreuslt);
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
        [Route("Q_PTA_Truck_InComp")]
        [HttpGet]
        public IHttpActionResult Q_PTA_Truck_InComp(string from,string to,string blno, string containerno,string language)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure(@"Q_PTA_Truck_InComp", new string[] { "From", "To", "BLNO", "ContainerNO", "Language" }, new object[] { from, to, blno,containerno,language }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// get service from IMS System
        /// </summary>
        /// <param name="BLNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [Route("VehicleTransfer")]
        [HttpGet]
        public IHttpActionResult VehicleTransfer(string BLNo, string date)
        {     
            try
            {
                DataTable dt = GetVehicleTransfer(BLNo, date);
                DataTable  dtSorted=new DataTable();
               // Console.WriteLine(dt.Rows.Count);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string storageNO = dt.Rows[i]["ContType"].ToString().Trim() + "-" + dt.Rows[i]["ContainerNo"].ToString().Trim();
                    //Console.WriteLine(dt.Rows[i]["BLNo"].ToString() + "---" + storageNO + "--");
                    if (CheckContainerisExist(dt.Rows[i]["BLNo"].ToString().Trim(), storageNO))
                    {
                        dt.Rows[i].Delete();
                    }
                }
                   dt.AcceptChanges();
                   if (dt.Rows.Count > 0)
                   {
                        dtSorted = dt.AsEnumerable().OrderBy(r => r.Field<string>("BLNo")).CopyToDataTable();
                   }
                   

                
                 
                   //Console.WriteLine(dt.Rows.Count);
                   return Ok(dtSorted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


        private DataTable GetVehicleTransfer(string BLNo, string date)
        {
            //DataSet result = new DataSet();
            try
            {

                MISInterfaceDAL.VehicleTransfer.ZFEPV_GetBLInfo so = new MISInterfaceDAL.VehicleTransfer.ZFEPV_GetBLInfo();
                return so.GetBLInfoByBL(BLNo, date).Tables[0];
                //var list = result.Tables[0].AsEnumerable().OrderBy(l => l.Field<string>("BLNo")).ThenBy(l => l.Field<string>("EstimatedPickUpDate")).CopyToDataTable();
            }
            catch (Exception ex)
            {

               Loger.Error(ex);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Vehicle Transfer New
        /// </summary>
        /// <param name="BLNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        //[Route("VehicleTransfer_New")]
        //[HttpGet]
        //public IHttpActionResult VehicleTransfer_New(string BLNo, string date,string pageindex,string pagesize)
        //{
        //    DataSet ds = new DataSet();
        //    List<DataTable> result = new List<DataTable>();
            

        //    try
        //    {
        //        VehicleTranfer_New.ZFEPV_GetBLInfo so = new VehicleTranfer_New.ZFEPV_GetBLInfo();
        //        result = so.GetBLInfoByBL(BLNo, date,Convert.ToInt16(pageindex),Convert.ToInt16(pagesize)).ToList();
        //        var list = result[1].AsEnumerable().OrderBy(l => l.Field<string>("BLNo")).ThenBy(l => l.Field<string>("EstimatedPickUpDate")).CopyToDataTable();
        //        Dictionary<string, Object> values = new Dictionary<string, object>();
        //        values.Add("TableData", _helper.ConvertJson(list));
        //        values.Add("TableCount", result[0]);
        //        return Ok(values);
        //        //= 
        //        //return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }


        //}
        /// <summary>
        /// Check Container is Exist by BL No and Container No
        /// </summary>
        /// <param name="BLNo"></param>
        /// <param name="ContainerNO"></param>
        /// <returns></returns>
        private bool CheckContainerisExist(string BLNo, string StorageNO)
        {

            //Dictionary<string, string> values = new Dictionary<string, string>();
            try
            {
                return gate.SelectScalar<int>(@"SELECT COUNT(*)
                                                FROM [UnJointTruck]
                                                WHERE OrderNO=@BLNo
                                                AND StorageNO =@StorageNO and Status not in ('N','X')",
                           new object[] { BLNo, StorageNO }) > 0;
            
                //if (count>0)
                //    values.Add("msg", "true");
                //else
                //    values.Add("msg", "false");
                //return Ok(values);
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                throw new Exception(ex.Message);
            }


        }
        /// <summary>
        /// Check Vehicle No and Material is Exist?
        /// </summary>
        /// <param name="BLNo"></param>
        /// <param name="ContainerNO"></param>
        /// <returns></returns>
        [Route("isExistByVehicleNoandMaterial")]
        [HttpGet]
        public IHttpActionResult isExistByVehicleNoandMaterial(string VehicleNo)
        {

            Dictionary<string, string> values = new Dictionary<string, string>();
            try
            {

                int count = gate.SelectScalar<int>(@"    select Count(*)  from  [Beling].[dbo].[UnJointTruck]
                                                        where VehicleNO=@VehicleNO and Status not in ('O','N','X')
                                                        and DATEDIFF(DAY, CONVERT(nvarchar(20),GETDATE(),111), CONVERT(nvarchar(20),ValidatePeriod,111)) >0
                                                        and DATEDIFF(DAY,  CONVERT(nvarchar(20),GETDATE(),111), CONVERT(nvarchar(20),ValidatePeriod,111)) <=3
                                                        "
                                     , new object[] { VehicleNo});

              


                
                if (count >= 0 && count < 2)
                    values.Add("msg", "true");
                else
                    values.Add("msg", "false");
                return Ok(values);

            }
            catch (Exception ex)
            {

                Loger.Error(ex);
                throw new Exception(ex.Message);
            }


        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

    public class Container {
        public List<UnJointTruck> Trucks { get; set; }
    }
}
