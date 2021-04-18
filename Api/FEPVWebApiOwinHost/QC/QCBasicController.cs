using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using QC.DAL;
using LIMS.Model;
using System.Data;
using FEPVWebApiOwinHost.Filter;
using RestSharp;
using log4net;

namespace FEPVWebApiOwinHost
{
    [FilterIP]
    [RoutePrefix("api/LIMS/Basic")]
    public class QCBasicController : ApiController
    {
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        BasicDAL basicDAL = new BasicDAL();

        /// <summary>
        /// Get Category List
        /// </summary>
        [Route("GetCategorys")]
        [HttpGet]
        public IHttpActionResult GetCategorys(string userid, string language)
        {
            DataTable dt = basicDAL.GetCategory(userid, language);
            return Ok(dt);
        }
        /// <summary>
        /// Get Samples list by Category 
        /// </summary>
        [Route("GetSamplesByCategory")]
        [HttpGet]
        public IHttpActionResult GetSampleByCategory(string userid, string typeID)
        {
            DataTable dt = basicDAL.GetSampleByCategory(userid, typeID);
            return Ok(dt);
        }
        /// <summary>
        /// Get Samples list by SampleName 
        /// </summary>
        [Route("GetSamplesBySampleName")]
        [HttpGet]
        public IHttpActionResult SamplesBySampleName(string userid, string SampleName)
        {
            DataTable dt = basicDAL.GetSamplesBySampleName(userid, SampleName);
            return Ok(dt);
        }
        [Route("GetSamples")]
        [HttpGet]
        public IHttpActionResult GetSamples(string userid, string query)
        {
            DataTable dt = basicDAL.GetSample(userid, query);
            return Ok(dt);
        }
        [Route("GetMaterials")]
        [HttpGet]
        public IHttpActionResult GetMaterials(string userid, string sampleName, string query)
        {
            DataTable dt = basicDAL.GetMaterial(userid, sampleName, query);
            return Ok(dt);
        }
        [Route("GetVendor")]
        [HttpGet]
        public IHttpActionResult GetVendor(string Language, string Fromtime, string Totime)
        {
            DataTable dt = basicDAL.GetVendor(Language, Fromtime, Totime);
            return Ok(dt);
        }

        [Route("getAttribute")]
        [HttpGet]
        public IHttpActionResult GetAttribute(string sampleName)
        {
            return Ok(basicDAL.GetAttribute(sampleName));
        }

        [Route("GetSpec")]
        [HttpGet]
        public IHttpActionResult GetSpec(string sampleName)
        {
            JsonObject json = new JsonObject();
            json["Msg"] = basicDAL.GetSpec(sampleName);
            return Ok(json);
        }

        [Route("getStatus")]
        [HttpGet]
        public IHttpActionResult getStatus(string cType, string lang)
        {
            return Ok(basicDAL.getStatus(cType, lang));
        }
        /// <summary>
        /// get data into Q_GetLines store procedure name create by Marco 1/9/2018
        /// </summary>
        /// <returns></returns>
        [Route("getLines")]
        [HttpGet]
        public IHttpActionResult getLines(string userID)
        {
            try
            {
                return Ok(basicDAL.GetLines(userID));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }

        [Route("GetLinesByAB")]
        [HttpGet]
        public IHttpActionResult GetLinesByAB(string userId, string sampleName, string ab)
        {
            try
            {
                return Ok(basicDAL.GetLinesByAB(userId, ab, sampleName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }
        [Route("getCreateType")]
        [HttpGet]
        public IHttpActionResult getLines(string userID, string lang)
        {
            try
            {
                return Ok(basicDAL.GetCreateType(userID, lang));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }
    }
}
