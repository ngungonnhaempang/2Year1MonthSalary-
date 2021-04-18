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
using System.Reflection;
using Newtonsoft.Json;
using RestSharp;
using System.IO;

namespace FEPVWebApiOwinHost
{
    [FEPVWebApiOwinHost.Filter.FilterIP]
    [RoutePrefix("api/Gate/JointTruck")]
    public class JointTruckController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("Beling");
        private NBear.Data.Gateway misgate = new NBear.Data.Gateway("MIS");
        protected readonly GateContext _GateContext = new GateContext();
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();


        /// <summary>
        /// 保存shippingOrder
        /// </summary>
        /// <param name="shippingOrder"></param>
        /// <returns></returns>
        [Route("ShippingOrder")]
        [HttpPost]
        public IHttpActionResult ShippingOrder(JointTruckItem shippingOrder)
        {
            return Ok(SaveShippingOrder(shippingOrder));
        }
        //[Route("ReuploadIEMS")]
        //[HttpGet]
        //public IHttpActionResult reUpdateIEMS()
        //{
        //    var vbeln = "";
        //    var msg = "";
        //    for (int i = 0; i < length; i++)
        //    {
        //        msg += UploadPickInfo(vbeln);
        //    }
        //    return Ok(msg);

        //}
        [Route("UploadFile")]
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

        [Route("ShippingOrder")]
        [HttpDelete]
        public IHttpActionResult DeleteShippingOrder(JointTruckItem shippingOrder)
        {
            try
            {
                JsonObject json = new JsonObject();
                var _JointTruck = _GateContext.JointTrucks.Find(shippingOrder.VoucherID);
                if (_JointTruck != null)
                {
                    if (_JointTruck.Status == "D" || _JointTruck.Status == "E" || _JointTruck.Status == "O")
                    {
                        json["Message"] = "Status(D,E,O) is ERROR ,Cann't Delete";
                        return Ok(json);
                    }
                    else
                    {
                        var jointItem = _GateContext.JointTruckItems.Find(shippingOrder.VoucherID, shippingOrder.ShippingOrder);
                        if (jointItem != null)
                        {
                            _GateContext.JointTruckItems.Remove(jointItem);
                            _GateContext.SaveChanges();
                        }
                        else
                        {
                            json["Message"] = "Item is not exsit";
                            return Ok(json);
                        }
                    }
                }
                else
                {
                    json["Message"] = "Plan is not exsit!";
                    return Ok(json);

                }
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        private JsonObject SaveShippingOrder(JointTruckItem shippingOrder)
        {
            JsonObject json = new JsonObject();
            try
            {
                json["Message"] = string.Empty;
                var _JointTruck = _GateContext.JointTrucks.Find(shippingOrder.VoucherID);
                if (_JointTruck != null)
                {


                    if (_JointTruck.Status == "D" || _JointTruck.Status == "E" || _JointTruck.Status == "O")
                    {
                        json["Message"] = "Status(D,E,O) is ERROR ,Cann't Update Save";
                        return json;
                    }
                    else
                    {
                        #region 判断是否能添加
                        //XT_HS_CanSaveJointTruck
                        string isMsg = gate.ExecuteStoredProcedure("XT_HS_CanSaveJointTruck",
                             new string[] { "ShippingOrder" },
                             new object[] { shippingOrder.ShippingOrder }).Tables[0].Rows[0][""].ToString(); ;
                        if (!string.IsNullOrEmpty(isMsg))
                        {
                            json["Message"] = isMsg;
                            return json;
                        }
                        #endregion
                        var shiporder = _GateContext.JointTruckItems.Find(shippingOrder.ShippingOrder, shippingOrder.VoucherID);

                        if (shiporder == null)
                        {
                            shiporder = new JointTruckItem
                            {
                                ShippingOrder = shippingOrder.ShippingOrder,
                                VoucherID = shippingOrder.VoucherID,
                                Direction = shippingOrder.Direction

                            };
                            _GateContext.JointTruckItems.Add(shiporder);

                        }
                        else
                        {
                            _GateContext.Entry(shippingOrder).State = EntityState.Modified;
                        }
                        _GateContext.SaveChanges();
                    }
                }
                else
                {
                    json["Message"] = "Plan is not exsit!";
                }
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                json["Message"] = ex.Message;
            }
            return json;
        }

        /// <summary>
        /// 上传拣配资料给进出口
        /// </summary>
        /// <param name="orderitems"></param>
        [Route("UploadPick")]
        [HttpGet]
        public IHttpActionResult UploadPickInfo(string VBELN)
        {
            JsonObject json = new JsonObject();
            List<IEPick> iepick = new List<IEPick>();
            try
            {
                Console.WriteLine("UploadPickInfo" + VBELN);
                DataTable dt = misgate.ExecuteStoredProcedure("IE_UploadGoods",
                     new string[] { "VBELN" }, new object[] { VBELN }).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["macno"].ToString()))
                        {
                            IEPick headpicks = new IEPick();
                            headpicks.sono = row["sono"].ToString();
                            headpicks.macno = row["macno"].ToString();
                            headpicks.outno = row["outno"].ToString();
                            headpicks.pkno = row["pkno"].ToString();
                            headpicks.Ordno = row["Ordno"].ToString();
                            headpicks.Pos = row["Pos"].ToString();
                            headpicks.unit = row["unit"].ToString();
                            headpicks.fbcno = row["fbcno"].ToString();
                            headpicks.matdesc = row["matdesc"].ToString();
                            headpicks.cunicode = row["cunicode"].ToString(); ;
                            headpicks.outdate = Convert.ToDateTime(row["outdate"]).ToString("yyyyMMdd");
                            headpicks.Chkno = row["Batch"].ToString();
                            headpicks.Fpu = row["Fpu"].ToString();
                            headpicks.Pallet = Convert.ToDecimal(row["Pallet"]);
                            string voucherid = row["VoucherID"].ToString();

                            #region  Pick Goods
                            string AB = voucherid.Substring(0, 1);
                            string barcode = row["pattern"].ToString();
                            headpicks.ryard = Convert.ToDecimal(row["Num"].ToString());//每一包的净重
                            headpicks.ryard2 = Convert.ToDecimal(row["Num"].ToString());
                            DataTable tableNameDT = misgate.ExecuteStoredProcedure("IEMS_GetTableByAB",
                                new string[] { "AB" }, new object[] { AB }).Tables[0];
                            string tableName = tableNameDT.Rows[0][0].ToString();

                            //string tableName = AB == "C" ? "FEPV_SSP" : "FEPV_POLY";
                            string spc = misgate.SelectDataSet("SELECT Spec  from MaterialType where AB=@AB", new object[] { AB }).Tables[0].Rows[0][0].ToString();
                            DataTable dtgoods = misgate.SelectDataSet(string.Format("SELECT * FROM {0} where BarCode=@Barcode", tableName), new object[] { barcode }).Tables[0];
                            if (dtgoods.Rows.Count > 0)
                            {
                                headpicks.pattern = barcode;
                                headpicks.gwquan = Convert.ToDecimal(dtgoods.Rows[0]["GWT"].ToString());//，毛重
                                headpicks.PackNum = 1m;
                                if (AB == "C" || AB == "L")
                                {
                                    string packmethodid = dtgoods.Rows[0]["PackageID"].ToString();
                                    //包装方式的英文方式
                                    headpicks.PackType = misgate.SelectDataSet("SELECT * FROM PackageMethod where PackageID=@PackageID", new object[] { packmethodid }).Tables[0].Rows[0]["Description_EN"].ToString();

                                }
                                else
                                {
                                    headpicks.PackType = "";
                                }
                                headpicks.Volume = 0m;
                                headpicks.VolUnit = "";
                                headpicks.MaterialName = spc;
                                iepick.Add(headpicks);
                            }
                            else
                            {
                                json["MSG"] = "NO Goods Barcode:" + barcode;
                                json["Flag"] = 0;
                                return Ok(json);
                            }

                            #endregion
                        }
                        else
                        {
                            json["MSG"] = "NO Container/ Không có số Container";
                            json["Flag"] = 0;
                            return Ok(json);
                        }

                    }
                    DataSet ds = new DataSet();
                    DataTable newtable = MIS.Utility.DataFormatter.ToDataTable(iepick);

                    Console.WriteLine(JsonConvert.SerializeObject(newtable));
                    Loger.Info(JsonConvert.SerializeObject(newtable));
                    ds.Merge(newtable);
                    Console.WriteLine("FEPV_GetSOInfo" + ds.Tables[0].Rows.Count);

                    MISInterfaceDAL.UploadPickInfo.FEPV_GetSOInfo so = new MISInterfaceDAL.UploadPickInfo.FEPV_GetSOInfo();
                    DataSet returnds = so.UploadStockInfo_Chemical(ds);
                    Console.WriteLine(returnds.Tables.Count);
                    if (returnds != null)
                    {
                        DataTable retrundt = returnds.Tables[0];

                        if (retrundt.Rows.Count > 0)
                        {
                            string errorcode = retrundt.Rows[0]["ErrorCode"].ToString();
                            Loger.Info("call IEMS" + DateTime.Now.ToString("s"));
                            if (!string.IsNullOrEmpty(errorcode))
                            {
                                json["MSG"] = string.Format("IEMS ERROR:  SONO:{0},ErrorCode:{1},ErrorMsg {2}", retrundt.Rows[0]["SONO"].ToString(), retrundt.Rows[0]["ErrorCode"].ToString(), retrundt.Rows[0]["ErrorMsg"].ToString());
                                json["Flag"] = 0;
                                Loger.Info(json["MSG"]);
                            }
                            else
                            {
                                json["MSG"] = string.Format("IEMS Success:  SONO:{0},Msg {2}", retrundt.Rows[0]["SONO"].ToString(), retrundt.Rows[0]["ErrorCode"].ToString(), retrundt.Rows[0]["ErrorMsg"].ToString());
                                json["Flag"] = 1;
                                json["VBELN"] = VBELN;
                            }

                        }
                        else
                        {
                            json["MSG"] = "IEMS WEBSERIVE ERROR";
                            json["Flag"] = 0;

                        }
                    }
                    else
                    {
                        json["MSG"] = "IEMS WEBSERIVE NOT GET UploadStockInfo_Chemical DataSet";
                        json["Flag"] = 0;

                    }
                }
                else
                {
                    json["MSG"] = "API:  NO Data";
                    json["Flag"] = 0;
                }
                return Ok(json);
            }
            catch (Exception e)
            {

                json["MSG"] = e.Message + e.StackTrace;
                return Ok(json);
            }



        }
        /// <summary>
        /// 上传拣配资料给进出口
        /// </summary>
        /// <param name="orderitems"></param>
        [Route("UploadIEMSTemporary")]
        [HttpGet]
        public IHttpActionResult UploadIEMSTemporary(string VBELN, string pos, string VoucherID, int Flag)
        {
            Console.WriteLine("UploadPickInfo" + VBELN);
            DataTable dt = null;
            if (Flag == 0)
            {
                dt = misgate.ExecuteStoredProcedure("IE_UploadTEMPData",
                 new string[] { "VBELN", "VoucherID" }, new object[] { VBELN, VoucherID }).Tables[0];
            }
            else
            {
                dt = misgate.ExecuteStoredProcedure("IE_UploadTEMPDataBySONO",
                 new string[] { "VBELN" }, new object[] { VBELN }).Tables[0];
            }

            JsonObject json = new JsonObject();
            List<IEMSTemparory> iepick = new List<IEMSTemparory>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(row["macno"].ToString()))
                    {
                        IEMSTemparory headpicks = new IEMSTemparory();
                        headpicks.sono = row["sono"].ToString();
                        headpicks.salesorder = row["Ordno"].ToString();
                        headpicks.Pos = row["pkno"].ToString();
                        headpicks.fbcno = row["fbcno"].ToString();
                        headpicks.matdesc = row["matdesc"].ToString();
                        headpicks.packnum = row["packnum"].ToString();
                        headpicks.macno = row["macno"].ToString();
                        headpicks.gwquan = row["gwquan"].ToString();
                        headpicks.grossweight = row["grossweight"].ToString();
                        headpicks.Fpu = row["Fpu"].ToString();

                        #region  Pick Goods

                        headpicks.ryard = Convert.ToDecimal(row["ryard"].ToString());//每一包的净重
                        headpicks.ryard2 = Convert.ToDecimal(row["ryard2"].ToString());
                        iepick.Add(headpicks);
                        #endregion
                    }
                    else
                    {
                        json["MSG"] = "NO Container/ Không có số Container";
                        json["Flag"] = 0;
                        return Ok(json);
                    }

                }
                DataSet ds = new DataSet();
                DataTable newtable = MIS.Utility.DataFormatter.ToDataTable(iepick);

                Console.WriteLine(JsonConvert.SerializeObject(newtable));
                Loger.Info(JsonConvert.SerializeObject(newtable));
                ds.Merge(newtable);
                MISInterfaceDAL.UploadPickInfo.FEPV_GetSOInfo so = new MISInterfaceDAL.UploadPickInfo.FEPV_GetSOInfo();
                DataTable retrundt;
                if (Flag == 0)
                {
                    DataSet returnds = so.GetEstPackingInfo_SSP(ds);
                    retrundt = returnds.Tables[0];
                }
                else
                {
                    DataSet returnds = so.GetEstPackingInfo_SSPBySONO(ds);
                    retrundt = returnds.Tables[0];
                }

                //Console.WriteLine(returnds);
                string errorcode = retrundt.Rows[0]["ErrorCode"].ToString();
                Loger.Info("call IEMS" + DateTime.Now.ToString("s"));
                if (!string.IsNullOrEmpty(errorcode))
                {
                    json["MSG"] = string.Format("IEMS ERROR:  SONO:{0},ErrorCode:{1},ErrorMsg {2}", retrundt.Rows[0]["SONO"].ToString(), retrundt.Rows[0]["ErrorCode"].ToString(), retrundt.Rows[0]["ErrorMsg"].ToString());
                    json["Flag"] = 0;
                    Loger.Info(json["MSG"]);
                }
                else
                {
                    json["MSG"] = string.Format("IEMS Success:  SONO:{0},Msg {2}", retrundt.Rows[0]["SONO"].ToString(), retrundt.Rows[0]["ErrorCode"].ToString(), retrundt.Rows[0]["ErrorMsg"].ToString());
                    json["Flag"] = 1;
                    json["VBELN"] = VBELN;
                }

            }
            else
            {
                json["MSG"] = "API:  NO Data";
                json["Flag"] = 0;
            }
            return Ok(json);

        }

        [Route("JointTruckByVoucherID")]
        [HttpGet]
        /// <summary>
        /// 根据单号获得一条系统协同车辆信息
        /// </summary>
        /// <param name="voucherid"></param>
        /// <returns></returns>
        public JointTruck JointTruckByVoucherID(string voucherid)
        {
            Console.WriteLine("JointTruckByVoucherID：" + DateTime.Now.ToString());
            var _JointTruck = _GateContext.JointTrucks.Find(voucherid);
            //_JointTruck.JointShipNOs = GetSubJointTruckList(voucherid);
            return _JointTruck;
        }


        /// <summary>
        /// 根据VoucherID获取系统协同车辆发货单信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<JointShipNO> GetSubJointTruckList(string VoucherID)
        {
            List<JointShipNO> list = null;
            DataTable dt = gate.ExecuteStoredProcedure("XT_HS_GetSubJointTruckList",
                new string[] { "VoucherID" }, new object[] { VoucherID }).Tables[0];
            if (dt.Rows.Count > 0)
            {
                list = _helper.ConvertList<JointShipNO>(dt);
            }

            return list;
        }

        /// <summary>
        /// 自动生成系统协同车辆信息单据号
        /// </summary>
        /// <returns></returns>
        public string GetJointTruckVoucherIdFlow()
        {
            try
            {
                string getVoucherSql = @"SELECT 'XT' + right(convert(nvarchar(8) , getdate(),112),8) +
			                                right('00000000' + convert(nvarchar(4) , IsNull(Max(right(VoucherID,4))+1,1)) , 4)
			                                FROM dbo.JointTruck 
			                                WHERE VoucherID LIKE 'XT' + right(convert(nvarchar(8) , getdate(),112),8) + '%'";
                return gate.SelectScalar<string>(getVoucherSql, new object[] { });
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("SaveJointTruck")]
        [HttpPost]
        /// <summary>
        /// 保存一条系统协同车辆信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public IHttpActionResult SaveJointTruck(JointTruck jointTruck)
        {
            try
            {
                Console.WriteLine("SaveJointTruck：" + DateTime.Now.ToString());
                List<string> items = new List<string>();// jointTruck.ShippingOrder.Split(',');
                foreach (var or in jointTruck.JointTruckItems)
                {
                    items.Add(or.ShippingOrder);
                    string PageStatus = "New";
                    if (!string.IsNullOrEmpty(jointTruck.VoucherID))
                    {
                        PageStatus = "Update";
                    }
                    string msg = gate.ExecuteStoredProcedureScalar("XT_HS_CanAddedToPlan4VBELV",
                      new string[] { "VBELV", "VoucherID", "PageStatus" }, new object[] { or.ShippingOrder, or.VoucherID, PageStatus }).ToString();

                    if (!string.IsNullOrEmpty(msg))
                    {
                        if (jointTruck.CompanyCode == "VH")
                        {
                            //check  delete voucher


                        }
                        else
                        {
                            return BadRequest(msg);
                        }
                    }
                }


                string voucherId = string.IsNullOrEmpty(jointTruck.VoucherID) ? "" : jointTruck.VoucherID;
                var _JointTruck = _GateContext.JointTrucks.Find(voucherId);
                string result = string.Empty;

                jointTruck.ShippingOrder = string.Join(",", items);
                //核查是否发货单已经安排
                //重复创建单据判断
                if (_JointTruck != null)
                {
                    //已经出厂、二磅、打印不能对发货单的修改
                    if (_JointTruck.Status == "D" || _JointTruck.Status == "E" || _JointTruck.Status == "O")
                    {
                        return BadRequest("Status(D,E,O) is ERROR ,Cann't Update Save");
                    }
                    else
                    {
                        #region  //移除原Item项次
                        while (_JointTruck.JointTruckItems.Count > 0)
                        {
                            _GateContext.JointTruckItems.Remove(_JointTruck.JointTruckItems[0]);
                        }
                        //新增Item项次
                        foreach (JointTruckItem item in jointTruck.JointTruckItems)
                        {
                            item.VoucherID = jointTruck.VoucherID;
                            item.Stamp = DateTime.Now;
                            _GateContext.JointTruckItems.Add(item);
                        }
                        #endregion
                    }

                    jointTruck.Stamp = DateTime.Now;
                    jointTruck.Message = "";
                    jointTruck.Reason = "";
                    //
                    RemoveHoldingEntityInContext(jointTruck);
                    //
                    _GateContext.Entry(jointTruck).State = EntityState.Modified;


                }
                else
                {
                    if (string.IsNullOrEmpty(jointTruck.Status))
                    {
                        jointTruck.Status = "";
                    }


                    string vouIdNew = GetJointTruckVoucherIdFlow();
                    foreach (JointTruckItem item in jointTruck.JointTruckItems)
                    {
                        item.VoucherID = vouIdNew;
                        _GateContext.JointTruckItems.Add(item);
                    }

                    jointTruck.VoucherID = vouIdNew;
                    jointTruck.Stamp = DateTime.Now;
                    jointTruck.Message = "";
                    jointTruck.Reason = "";
                    _GateContext.JointTrucks.Add(jointTruck);
                }

                _GateContext.SaveChanges();
                Console.WriteLine("SaveChanges OK：" + DateTime.Now.ToString());

                //string[] parameters = new string[] { "UserID", "VoucherID", "TruckCompany", "TuckID" };
                //gate.DbHelper.ExecuteStoredProcedureNonQuery("WF_MIS_Q_UpdatePlan", parameters, new object[]{
                //        "",jointTruck.VoucherID,jointTruck.TransferCompany,jointTruck.VehicleNO});

                return Ok(jointTruck);
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
        private Boolean RemoveHoldingEntityInContext(JointTruck entity)
        {
            var objContext = ((IObjectContextAdapter)_GateContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<JointTruck>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }

        [Route("GetJointTrucks")]
        [HttpGet]
        /// <summary>
        /// 分页查询系统协同车辆信息
        /// </summary>
        public IHttpActionResult GetJointTrucks(string userID, int pageIndex, int pageSize, string des, string voucherId, string status, string vehicleNO, DateTime? dateFrom, DateTime? dateTo, string CompanyCode)
        {
            Console.WriteLine("GetJointTrucks：" + DateTime.Now.ToString());
            try
            {//CC_Q_GetJointTrucks
                object[] outParameters = null;
                string[] parameters = new string[] { "UserID", "pageIndex", "pageSize", "Des", "VoucherID", "Status", "VehicleNO", "dateFrom", "dateTo", "CompanyCode" };
                DataTable dt = gate.ExecuteStoredProcedure("XT_HS_GetJointTrucks_Page", parameters,
                    new object[] { userID, pageIndex, pageSize, des, voucherId, status, vehicleNO, dateFrom, dateTo, CompanyCode },
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

        [Route("DeleteJointTrucks")]
        [HttpPost]
        /// <summary>
        /// 删除系统协同车辆信息
        /// </summary>
        /// <param name="voucherIds"></param>
        /// <returns></returns>
        public HttpResponseMessage DeleteJointTrucks(string userId, string voucherId)
        {
            Console.WriteLine("DeleteJointTrucks：" + DateTime.Now.ToString());
            try
            {
                string msg = gate.DbHelper.ExecuteStoredProcedure("CC_J_CanDelete",
                    new string[] { "UserID", "VoucherID" }, new object[] { userId, voucherId }).Tables[0].Rows[0][0].ToString();
                if (string.IsNullOrEmpty(msg))
                {
                    string sql = "UPDATE JointTruck SET [Status] = 'X' WHERE VoucherID=@VoucherID AND [Status] IN ('','N','Q')";
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
        [Route("UpdateJointTruckStatus")]
        [HttpPut]
        public HttpResponseMessage UpdateJointTruckStatus(string voucherID, string status)
        {
            if (string.IsNullOrEmpty(voucherID))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Document number is empty！");
            try
            {
                gate.ExecuteNonQuery(@"UPDATE JointTruck
                                       SET [Status] = @Status, [Stamp] = @Stamp
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
        [Route("WriteJointTruckReason")]
        [HttpGet]
        public IHttpActionResult WriteJointTruckReason(string voucherID, string message, string reason)
        {
            Console.WriteLine("WriteJointTruckReason：" + DateTime.Now.ToString());
            Dictionary<string, string> values = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(voucherID))
            {
                values.Add("msg", "单据号为空！");
                return Ok(values);
            }
            try
            {
                gate.ExecuteNonQuery(@"UPDATE JointTruck
                                       SET [Message] = @Message, [Reason] = @Reason, [Stamp] = @Stamp, [Complete] = @Complete
                                       WHERE VoucherID = @VoucherID",
                                        new object[] { message, reason, DateTime.Now, DateTime.Now, voucherID });
                int count = gate.SelectScalar<int>(@"SELECT COUNT(*)  FROM [JointTruck]
                                WHERE [Message]<>'' AND Reason<>'' AND VoucherID=@VoucherID ", new object[] { voucherID });
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

        [Route("IsCanUpdate")]
        [HttpGet]
        /// <summary>
        /// 是否可以更新
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult IsCanUpdate(string userId, string voucherId)
        {
            Console.WriteLine("IsCanUpdate：" + DateTime.Now.ToString());
            try
            {//CC_J_CanUpdate
                string msg = gate.DbHelper.ExecuteStoredProcedure("CC_J_CanUpdate",
                    new string[] { "UserID", "VoucherID" }, new object[] { userId, voucherId }).Tables[0].Rows[0][0].ToString();

                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("msg", msg);
                return Ok(values);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("IsInBlackList")]
        [HttpGet]
        /// <summary>
        /// 是否在黑名单
        /// </summary>
        /// <param name="vehicle">车号/身份证</param>
        /// <param name="type">类型（车辆，人员）</param>
        /// <returns></returns>
        public IHttpActionResult IsInBlackList(string vehicle, string type)
        {
            Console.WriteLine("IsInBlackList：" + DateTime.Now.ToString());
            try
            {//CC_J_BL_IsInBlackList
                string msg = gate.DbHelper.ExecuteStoredProcedure("BL_HS_IsInBlackList", new string[] { "NO", "Type" },
                new object[] { vehicle, type }).Tables[0].Rows[0][0].ToString();

                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("msg", msg);//在黑名单里面
                return Ok(values);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetMaterialTypes")]
        [HttpGet]
        /// <summary>
        /// 获取物料类型
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetMaterialTypes(string Language, string Type)
        {
            try
            {//D_MaterialTypes4JointTruck
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("XT_HS_GetMaterialTypes", new string[] { "Language", "Type" }, new object[] { Language, Type }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetTruckCompany")]
        [HttpGet]
        /// <summary>
        /// 获取车行信息
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetTruckCompany(string Language)
        {
            try
            {//D_TruckCompany
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("XT_HS_GetTruckCompanys", new string[] { "Language" }, new object[] { Language }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetVehicleShapes")]
        [HttpGet]
        /// <summary>
        /// 获取车型信息
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetVehicleShapes(string Language)
        {
            try
            {//D_VehicleShape
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("XT_HS_GetVehicleShapes", new string[] { "Language" }, new object[] { Language }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("CanAddedToPlan4VBELVs")]
        [HttpGet]
        /// <summary>
        /// 检查所添加的发货单是否符合条件：1，产品类别一致；2，发货单存在；3，没被其他计划使用
        /// </summary>
        /// <param name="vs">发货单拼接</param>
        /// <param name="VoucherID">计划单号</param>
        /// <param name="PageStatus">新建/修改</param>
        /// <param name="mType">产品类别(例如:H|胶片)</param>
        /// <returns></returns>
        public IHttpActionResult CanAddedToPlan4VBELVs(string vs, string VoucherID, string PageStatus, string mType, string companycode)
        {
            Console.WriteLine("CanAddedToPlan4VBELVs：" + DateTime.Now.ToString());
            try
            {
                string msg = string.Empty;

                List<string> vbelvs = vs.Split(',').ToList();
                //DataTable dt = GetFHsByVBELVs(vbelvs);
                //if (dt != null && dt.Rows.Count != 0)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        if (dt.Rows[i]["产品类别"].ToString() != mType.Split('|')[0])
                //        {
                //            msg = string.Format("Document number[{0}] of the material types inconsistent with you select the type of material", dt.Rows[i]["预收单附号"].ToString());
                //            break;
                //        }
                //        if (i > 0)
                //        {
                //            if (dt.Rows[i]["产品类别"].ToString() != dt.Rows[i - 1]["产品类别"].ToString())
                //            {
                //                msg = string.Format("Document number[{0}] of the different number of single type and other materials", dt.Rows[i]["预收单附号"].ToString());
                //                break;
                //            }
                //        }
                //    }
                //}

                if (string.IsNullOrEmpty(msg) && vbelvs != null && vbelvs.Count != 0)
                {
                    foreach (var i in vbelvs)
                    {
                        //CC_J_CanAddedToPlan4VBELV
                        msg = gate.ExecuteStoredProcedureScalar("XT_HS_CanAddedToPlan4VBELV",
                            new string[] { "VBELV", "VoucherID", "PageStatus" },
                            new object[] { i, VoucherID, PageStatus }).ToString();
                        if (string.IsNullOrEmpty(msg))
                            continue;
                        else
                            break;
                    }
                }

                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("msg", msg);
                return Ok(values);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获得单个计划中的发货单列表
        /// </summary>
        /// <param name="vbelvs"></param>
        /// <returns></returns>
        public DataTable GetFHsByVBELVs(List<string> vbelvs)
        {
            try
            {
                DataTable result = new DataTable();
                if (vbelvs != null && vbelvs.Count != 0)
                {
                    foreach (var i in vbelvs)
                    {
                        result.Merge(gate.ExecuteStoredProcedure("XT_HS_GetFHsByVBELV",
                            new string[] { "VBELV" }, new object[] { i }).Tables[0]);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetShippingOrder")]
        [HttpGet]
        public IHttpActionResult GetShippingOrder(string Trantype, string Outno)
        {
            DataSet ds = new DataSet();
            DataSet result = new DataSet();
            DataTable dt = new DataTable("OUTDAT");
            dt.Columns.Add(new DataColumn("Trantype", typeof(string)));
            dt.Columns.Add(new DataColumn("Outno", typeof(string)));
            DataRow dr = dt.NewRow();
            dr["Trantype"] = Trantype;
            dr["Outno"] = Outno;
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);

            try
            {


                MISInterfaceDAL.ShippingOrderDate.WA27N6MIS001 so = new MISInterfaceDAL.ShippingOrderDate.WA27N6MIS001();
                result = so.GetOUTDATC(ds);
                var test = result.Tables[0];

                return Ok(result.Tables[0]);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        [Route("VehicleInformation")]
        [HttpPost]
        public IHttpActionResult VehicleInformation(JointTruckToERP erp)
        {

            if (string.IsNullOrEmpty(erp.orders))
            {
                return BadRequest("orders is null");

            }
            if (string.IsNullOrEmpty(erp.TRUCKNO))
            {
                return BadRequest("TRUCKNO is null");

            }
            DataSet ds = new DataSet();
            DataSet result = new DataSet();
            string[] sArray = erp.orders.Split(',');
            DataTable dt = new DataTable("Order");
            dt.Columns.Add(new DataColumn("Outno", typeof(string)));
            dt.Columns.Add(new DataColumn("GDELINO", typeof(string)));//物流公司
            dt.Columns.Add(new DataColumn("TRUCKNO", typeof(string)));//车号
            dt.Columns.Add(new DataColumn("CDRIVER", typeof(string)));//司机
            dt.Columns.Add(new DataColumn("OUTTIME", typeof(string)));//20170417
            for (int i = 0; i < sArray.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Outno"] = sArray[i];
                dr["GDELINO"] = erp.GDELINO;
                dr["TRUCKNO"] = erp.TRUCKNO;
                dr["CDRIVER"] = erp.CDRIVER;
                dr["OUTTIME"] = Convert.ToDateTime(erp.OUTTIME).ToString("yyyyMMddHHmmss");
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            foreach (DataTable cd in ds.Tables)
            {
                Console.WriteLine(JsonConvert.SerializeObject(cd));

            }
            MISInterfaceDAL.VehicleInformationInput.WA27N6MIS002 vi = new MISInterfaceDAL.VehicleInformationInput.WA27N6MIS002();
            result = vi.SetOUTDATC(ds);
            var test = result.Tables[0];
            foreach (DataTable cd in result.Tables)
            {
                Console.WriteLine(JsonConvert.SerializeObject(cd));

            }


            return Ok(result.Tables[0]);

        }



        [Route("GetFHsByVBELVs4Grid")]
        [HttpGet]
        /// <summary>
        /// 获得单个计划中的发货单列表
        /// </summary>
        /// <param name="vbelvs"></param>
        /// <returns></returns>
        public IHttpActionResult GetFHsByVBELVs4Grid(string vs)
        {
            Console.WriteLine("GetFHsByVBELVs4Grid：" + DateTime.Now.ToString());
            try
            {
                List<string> vbelvs = vs.Split(',').ToList();
                DataTable result = new DataTable();
                if (vbelvs != null && vbelvs.Count != 0)
                {
                    foreach (var i in vbelvs)
                    {
                        result.Merge(gate.ExecuteStoredProcedure("XT_HS_GetFHsByVBELV",
                            new string[] { "VBELV" }, new object[] { i }).Tables[0]);
                    }
                }
                return Ok(_helper.ConvertJson(result));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Language"></param>
        /// <returns></returns>
        /// 
        [Route("QueryContratorByEmployer")]
        [HttpGet]
        /// <summary>
        /// QueryAllCardLogByContractor get data from Cardlog table with conditional using store procedure QueryAllCardLog
        /// </summary>
        public IHttpActionResult QueryContratorByEmployer(string Employer, string Date)
        {
            Console.WriteLine("GetJointTrucks：" + DateTime.Now.ToString());
            try
            {//CC_Q_GetJointTrucks
                object[] outParameters = null;
                string[] parameters = new string[] { "Employer", "Date" };
                DataTable dt = gate.ExecuteStoredProcedure("QueryContratorByEmployer", parameters,
                    new object[] { Employer, Date }).Tables[0];

                //Dictionary<string, Object> values = new Dictionary<string, object>();
                //values.Add("TableData", _helper.ConvertJson(dt));
                //values.Add("TableCount", outParameters);
                return Ok(dt);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
        [Route("GetCardType")]
        [HttpGet]
        /// <summary>
        /// <GetCardType> get data from CardType table with conditional using store procedure EGCD_Get_CardData_Type
        /// </summary>
        public IHttpActionResult GetCardType(string UserID, string Language)
        {
            Console.WriteLine("GetJointTrucks：" + DateTime.Now.ToString());
            try
            {//CC_Q_GetJointTrucks
                object[] outParameters = null;
                string[] parameters = new string[] { "UserID", "Language" };
                DataTable dt = gate.ExecuteStoredProcedure("EGCD_Get_CardData_Type", parameters,
                    new object[] { UserID, Language }).Tables[0];

                //Dictionary<string, Object> values = new Dictionary<string, object>();
                //values.Add("TableData", _helper.ConvertJson(dt));
                //values.Add("TableCount", outParameters);
                return Ok(dt);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
        [Route("QueryAllTruck")]
        [HttpGet]
        /// <summary>
        /// <QueryAllTruck> get data from vLogsTruck table using store procedure QueryAllTruck
        /// </summary>
        /// <param name="vbelvs"></param>
        /// <returns></returns>
        public IHttpActionResult QueryAllTruck()
        {
            try
            {//D_VehicleShape
                DataTable dt = gate.ExecuteStoredProcedure("QueryAllTruck", new string[] { }, new object[] { }).Tables[0];
                return Ok(dt);
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
        [Route("QueryExportTruck")]
        [HttpGet]
        public IHttpActionResult QueryExportTruck(string UserID, string FromDate, string ToDate, string VoucherID, string VehicleNContainerNO, string Language)
        {
            try
            {
                DataTable dt = gate.ExecuteStoredProcedure("XT_VH_QueryPlans", new string[] { "UserID", "B", "E", "VoucherID", "VehicleNO", "Language" }, new object[] { UserID, FromDate, ToDate, VoucherID, VehicleNContainerNO, Language }).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {

                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
        [Route("SaveExportTruck")]
        [HttpPost]
        public IHttpActionResult SaveExportTruck(AC_Permission permission)
        {
            JsonObject json = new JsonObject();
            try
            {
                string msg = gate.ExecuteStoredProcedure("AC_Check_MotoCars",
                                 new string[] { "MotoCars", "ContainerID", "UserID" },
                                 new object[] { permission.TruckNO, permission.RemarkContainerNO, permission.UserId }).Tables[0].Rows[0][0].ToString();
                string getVoucherSql_Permis = @"SELECT  'P'+ right(convert(nvarchar(8) , getdate(),112),8) +
                                        right('00000000' + convert(nvarchar(4) , IsNull(Max(right(PermissionID,4))+1,1)) , 4)
                                        FROM dbo.[AC_Permission] 
                                        WHERE PermissionID LIKE  'P'+right(convert(nvarchar(8) , getdate(),112),8) + '%'";
                string PermisVoucherID = gate.SelectScalar<string>(getVoucherSql_Permis, new object[] { });
               
                if (string.IsNullOrEmpty(msg))
                {
                    
                    permission.State = "N";
                    permission.PermissionID = PermisVoucherID;
                    permission.CheckInTime = DateTime.Now;
                    permission.Stamp = DateTime.Now;
                    _GateContext.aC_Permissions.Add(permission);
                    if (!string.IsNullOrEmpty(permission.RemarkContainerNO))
                    {
                        string getVoucherSql_Pond = @"SELECT  'B'+ right(convert(nvarchar(8) , getdate(),112),8) +
	                                       right('00000000' + convert(nvarchar(4) , IsNull(Max(right(PonderationID,4))+1,1)) , 4)
			                                    FROM dbo.[AC_Ponderation] 
                                         WHERE PonderationID LIKE  'B'+right(convert(nvarchar(8) , getdate(),112),8) + '%'";
                        string PondVoucherID = gate.SelectScalar<string>(getVoucherSql_Pond, new object[] { });
                        AC_Ponderation ponder = new AC_Ponderation();
                        ponder.ContainerNO = permission.RemarkContainerNO;
                        ponder.PonderationID = PondVoucherID;
                        //一次过磅时才磅单许可
                        ponder.PermissionID = "";
                        ponder.State = "I";
                        ponder.Stamp = DateTime.Now;
                        ponder.UserID = permission.UserId;
                        _GateContext.aC_Ponderations.Add(ponder);
                    }
                    _GateContext.SaveChanges();
                    return Ok();
                }
                else
                {
                    json["Message"] = msg;
                    return Ok(json);
                }
            }
            catch (Exception e)
            {

                Loger.Error(e);
                throw new Exception(e.InnerException.ToString());
            }
        }
        [Route("GetExportTruckType")]
        [HttpGet]
        public IHttpActionResult GetExportTruckType(string Language)
        {
            try
            {
                DataTable dt =gate.ExecuteStoredProcedure(@"XT_VH_GetVehicleShapes", new string[] {"Language" }, new object[] { Language}).Tables[0];
                return Ok(dt);
            }
            catch (Exception e)
            {

                throw;
            }
        }


    }

    public class JointTruckToERP
    {
        public string orders { set; get; }
        public string GDELINO { set; get; }
        public string TRUCKNO { set; get; }
        public string CDRIVER { set; get; }

        public string OUTTIME { get; set; }

    }
}
