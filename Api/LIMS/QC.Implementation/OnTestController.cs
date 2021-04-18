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
using QC.Utility;


namespace QC.Implementation
{
    [FilterIP]
    [RoutePrefix("api/LIMS/OnTestController")]
    public class OnTestController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        [Route("GetMaterial")]
        [HttpGet]
        public IHttpActionResult GetMaterial(string sampleName)
        {
            try
            {
                DataTable dt = gate.DbHelper.Select(" SELECT * FROM [Material] WHERE ([SampleName] = @SampleName) order by LOT_NO", new object[] { sampleName }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }

        [Route("GetSample")]
        [HttpGet]
        public IHttpActionResult GetSample( string UserID,string lang)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("QA23_GetSampleName",
                    new string[] { "UserID","Lang" }, new object[] { UserID, lang }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll(string Owner)
        {
            try
            {
                DataTable dt = gate.DbHelper.Select(@"SELECT [State], [VoucherID], [SampleName], [LOT_NO], [LINE], [ProdDate] FROM [PlanAdd] 
                                                      WHERE (([Owner] = @A OR ISNULL(@B,'')='') AND ([State] = '1')) ORDER BY [ProdDate] DESC", 
                                                      new object[] { Owner, Owner }).Tables[0];        
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw e;
            }
        }
        // Query

        [Route("QueryPlanAdd")]
        [HttpGet]
        /// <summary>
        /// 获取PTA\EG类型
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult QueryPlanAdd(DateTime? TimeB , DateTime TimeE, string SampleName, string Lot_No,
  string Owner)
        {
            try
            {//D_PtaEgs
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("aspnet_q_QueryPlanAdd", new string[] { "TimeB", "TimeE", "SampleName", "Lot_No", "Owner" }, new object[] { TimeB, TimeE,
               SampleName,Lot_No,Owner }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
        //
        [Route("InsertRequesition")]
        [HttpGet]
        public HttpResponseMessage InsertRequesition(string SampleName, string LOT_NO, string LINE, DateTime SheetDate, DateTime ProdDate, string SampleID,
           string State, string purpose, string owner, string CompanyOfferSample, string CompanyProduce, string POY_LOT_NO)
        {
            string sql = @"INSERT INTO [dbo].[PlanAdd]
                               ([SampleName]
                               ,[LOT_NO]
                               ,[LINE]
                               ,[SheetDate]
                               ,[ProdDate]
                               ,[SampleID]
                               ,[State]
                               ,[Purpose]
                               ,[Owner]
                               ,[CompanyOfferSample]
                               ,[CompanyProduce]
                               ,[POY_LOT_NO])
                         VALUES
                               (@SampleName,
                                @LOT_NO,
                                @LINE,
                                @SheetDate,
                                @ProdDate,
                                @SampleID,
                                @State,
                                @Purpose,
                                @Owner,
                                @CompanyOfferSample,
                                @CompanyProduce,
                                @POY_LOT_NO)";
            try
            {
                gate.ExecuteNonQuery(sql, new object[] 
                { 
                    SampleName, 
                    LOT_NO, 
                    LINE, 
                    SheetDate,
                    ProdDate, 
                    SampleID, 
                    State,
                    purpose, 
                    owner,
                    CompanyOfferSample,
                    CompanyProduce,
                    POY_LOT_NO
                    
                });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                 return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
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
