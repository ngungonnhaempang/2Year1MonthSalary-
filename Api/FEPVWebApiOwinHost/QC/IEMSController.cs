using System.Collections;
using System.Reflection;
using RestSharp;
using System.Collections.Generic;
using System;
using System.Web.Http;
using log4net;
using FEPVWebApiOwinHost.Models;
using QC.DAL;
using LIMS.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
namespace FEPVWebApiOwinHost
{

    [RoutePrefix("api/IEMS")]
    public class IEMSController : ApiController
    {
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
      
        HelperBiz helpbiz = new HelperBiz();
        GradeVersionDAL biz = new GradeVersionDAL();
        /// <summary>
        /// 如果船期编号已经存在报告了，则不能修改
        /// </summary>
        /// <param name="export"></param>
        /// <returns></returns>
        [Route("SaveAESKD")]
        [HttpGet]
        public HttpResponseMessage SaveAESKD(string AESKD, string UserId, string InvoiceNO, string InvoiceDate, string FinishedDate, string Remark, string SaleType)
        {
         
            JsonObject json = new JsonObject();
            string msg = string.Empty;
            try
            {
                IEMS_Export export = new IEMS_Export();
                try
                {
                   
                    export.AESKD = AESKD;
                    export.UserId = UserId;
                    export.InvoiceNO = InvoiceNO;
                    export.InvoiceDate = Convert.ToDateTime(InvoiceDate);
                    export.Remark = Remark;
                    export.FinishedDate = Convert.ToDateTime(FinishedDate);
                    export.SaleType = SaleType;
                }
                catch (Exception e)
                {
                    json["Message"] = e.Message;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, json); 

                }
                Loger.Info(JsonConvert.SerializeObject(export));
                if (string.IsNullOrEmpty(export.AESKD))
                {
                    json["Message"]  = "AESKD is null";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, json); 
                }
                if (string.IsNullOrEmpty(export.UserId))
                {
                    json["Message"] = "UserId is null";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, json); 
                }
                if (string.IsNullOrEmpty(export.InvoiceNO))
                {
                    json["Message"] = "InvoiceNO is null";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, json); 
                }
                //Check is can update
                msg = biz.IsCanUpdateByIEMS(export.AESKD);
                if (!string.IsNullOrEmpty(msg))
                {
                    json["Message"] = msg;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, json); ;
                }
                else
                {
                    msg = biz.UpdateIEMSExport(export);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        json["Message"] = msg;
                        return Request.CreateResponse(HttpStatusCode.BadRequest, json);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK);
                  
                    
                }
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                json["Message"] = ex.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, json);
            }

           
        }
    }
}
