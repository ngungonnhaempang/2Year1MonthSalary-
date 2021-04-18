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
using FEPVWebApiOwinHost.Models.FEPV;
using RestSharp;

namespace FEPVWebApiOwinHost
{

    [FEPVWebApiOwinHost.Filter.UserFilter]
    [RoutePrefix("api/MIS/Label")]
    public class LabelController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("MIS");
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        [Route("GetMaterial")]
        [HttpGet]
        public IHttpActionResult GetMaterial(string AB)
        {
            try
            {
                DataTable dt = gate.SelectDataSet(@"SELECT * FROM MATERIAL WHERE AB=@AB", new object[] { AB }).Tables[0];
                return Ok(dt);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// 2 is exist,1 success
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        /// 
        [Route("UploadSTAP")]
        [HttpPost]
        [Obsolete]
        public int UploadSTAP( FEPV_STAP goods)
        {
            DbTransaction tran = null;
            try
            {
                if (gate.SelectScalar<int>("SELECT Count(BarCode) FROM Goods WHERE BarCode = @B", new object[] { goods.BarCode })
                    == 1)
                {
                    Console.WriteLine(goods.BarCode + "->2");
                    return 2;
                }
                tran = gate.BeginTransaction();
                gate.ExecuteNonQuery(@"INSERT INTO Goods(BarCode,MaterialNO,ProdSpec,CenterID,Plant,Loc,Batch,Num,Version0,Version,ProdDate,State,UserID,Counter,Stamp,Checker,LastVouId,PrevVouID,TableName,Remark,Store,LabelDate)
                                  Values(@BarCode,@MaterialNO,@ProdSpec,@CenterID,@Plant,@Loc,@Batch,@Num,@Version0,@Version,@ProdDate,@State,@UserID,@Counter,@Stamp,@Checker,@LastVouId,@PrevVouID,@TableName,@Remark,@Store,@LabelDate)",
                                     tran,
                                     new object[]
                                 {
                                     goods.BarCode,//BarCode
                                     goods.MaterialNO,//MaterialNO
                                     goods.ProdSpec,//ProdSpec
                                     goods.CenterID,//CenterID *
                                     goods.Plant == null?"":goods.Plant,//"",//Plant
                                     goods.Loc== null?"": goods.Loc,//"",//Loc
                                     goods.Batch,//Batch
                                     goods.Num,//Num
                                     goods.Version0== null?"":goods.Version0,//Version0
                                     goods.Version== null?"":goods.Version,//Version
                                     goods.ProdDate,//ProdDate
                                     goods.State== null?"N":goods.State,//"N",//State
                                     goods.UserID,//UserID  *
                                     1,//Counter
                                     goods.Stamp,//Stamp
                                     Guid.NewGuid(),//Checker
                                     goods.LastVouID== null?"":goods.LastVouID,//"",//LastVouId
                                     "",//PrevVouID
                                     goods.TableName,//TableName
                                     "",//Remark
                                     "",//Store,
                                     goods.LabelDate
                                 }
                                     );

                //InsertExtend(goods, tran);
                gate.DbHelper.Insert(goods.TableName, goods.COLUMNS, goods.VALUES, tran, "BarCode");

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            Console.WriteLine(goods.BarCode + "->1");
            return 1;
        }
        [Route("checkandUpdate")]
        [HttpGet]
        [Obsolete]
        public int checkandUpdate(string barcode, decimal Drang,string LabelDate)
        {
            try
            {
                if (gate.SelectScalar<int>("SELECT Count(BarCode) FROM Goods WHERE BarCode = @B", new object[] { barcode })
                    == 1)
                {
                     gate.ExecuteNonQuery(@"UPDATE FEPV_STAP SET Drang = @Drang WHERE BarCode=@B ",
                                     new object[] { Drang,  barcode});
                    gate.ExecuteNonQuery(@"UPDATE Goods SET LabelDate = @LabelDate WHERE BarCode=@B ",
                                    new object[] { Convert.ToDateTime( Convert.ToDateTime(LabelDate).ToString("yyyy-MM-dd HH:mm:ss")), barcode });
                    return 1;
                   
                }
                return 2;
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                return 2;
            }
        }
        [Route("CheckandUpdateBarcode")]
        [HttpGet]
        [Obsolete]
        public IHttpActionResult CheckandUpdateBarcode(string BeginDate, string EndDate)
        {
            try
            {

                DateTime B = Convert.ToDateTime(BeginDate);
                DateTime E = Convert.ToDateTime(EndDate).AddDays(1);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                DataSet dsData = gate.ExecuteStoredProcedure("Get_N_CompareBarcode_MIS_Bagging", new string[] { "BeginDate", "EndDate" }, new object[] { B,E });
                return Ok(dsData.Tables[0]);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }
        [Route("GetMainBatch")]
        [HttpGet]
        [Obsolete]
        public IHttpActionResult GetMainBatch()
        {
            try
            {
                DataTable dt = gate.SelectDataSet("SELECT * FROM STAP_Bale_Standard", new object[] { }).Tables[0];
                return Ok(dt);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


    }
    
}
