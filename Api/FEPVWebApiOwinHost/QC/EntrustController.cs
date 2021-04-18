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
using System.Collections;
using System.Reflection;
using FEPVWebApiOwinHost.Filter;
using RestSharp;
using Newtonsoft.Json;
using FEPVWebApiOwinHost.QC;
using QC.DAL;
using QC.Model;
using LIMS.Model;

namespace FEPVWebApiOwinHost
{
    [FilterIP]
    [RoutePrefix("api/LIMS/Entrust")]
    public class EntrustController : ApiController
    {

        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        SampleService draftDAL = new SampleService();
        OtherQueryDAL otherQueryDAL = new OtherQueryDAL();

        private decimal GetMinValueChart(TrendChart Model, decimal maxPoint, decimal minPoint, bool isMax)
        {
            List<decimal> listDecimals = new List<decimal>();
            listDecimals.Add(Model.CL);
            listDecimals.Add(Model.LCL);
            listDecimals.Add(Model.LLCL);
            listDecimals.Add(Model.LSL);
            listDecimals.Add(Model.LUCL);
            listDecimals.Add(Model.MR);
            listDecimals.Add(Model.TARGET);
            listDecimals.Add(Model.UCL);
            listDecimals.Add(Model.USL);
            listDecimals.Add(Model.X);
            listDecimals.Add(maxPoint);
            listDecimals.Add(minPoint);
            if (isMax)
            {
                return listDecimals.Where(p => p != decimal.MinValue).Max();
            }
            else
            {
                return listDecimals.Where(p => p != decimal.MinValue).Min();
            }
        }
        [Route("SingleTrendChart")]
        [HttpPost]
        public IHttpActionResult SingleTrendChart(ChartQuery note)
        {

            JsonObject json = new JsonObject();
            if (Convert.ToDateTime(note.BeginTime) >= Convert.ToDateTime(note.EndTime))
            {
                json["Error"] = "Begin Time <End Time";
                return Ok(json); ;
            }
            try
            {
                int AB = Convert.ToInt32(note.AB);
                if (note.Isdelegate)
                    AB = 4;


                ChartCreater cc = new ChartCreater();
                int i;
                i = cc.FillData(note.SampleName, note.LOT_NO, note.Line, note.Property, Convert.ToDateTime(note.BeginTime), Convert.ToDateTime(note.EndTime), AB);
                TrendChart dic = new TrendChart();
                cc.SingleTrendChart(ref dic);

                #region red
                decimal[][] red = null;
                if (dic.Number != null && dic.testScores != null && dic.Number.Length != 0 && dic.testScores.Length != 0)
                {
                    red = new decimal[dic.Number.Length][];
                    for (int r = 0; r < dic.Number.Length; r++)
                    {
                        red[r] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        red[r][0] = dic.Number[r];
                        red[r][1] = dic.testScores[r];
                    }
                    json["RedPoint"] = red;
                }
                #endregion

                #region 超线的点1
                if (dic.overCLNumbers != null && dic.overCLScores != null
                    && dic.overCLNumbers.Length != 0 && dic.overCLScores.Length != 0 && dic.overCLNumbers.Length == dic.overCLScores.Length)
                {
                    decimal[][] over1 = new decimal[dic.overCLNumbers.Length][];
                    for (int r = 0; r < dic.overCLNumbers.Length; r++)
                    {
                        over1[r] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        over1[r][0] = dic.overCLNumbers[r];
                        over1[r][1] = dic.overCLScores[r];
                    }
                    json["Over1"] = over1;

                }
                #endregion

                #region 超线的点2
                if (dic.overNumbers != null && dic.overTestScores != null
                    && dic.overNumbers.Length != 0 && dic.overTestScores.Length != 0 && dic.overNumbers.Length == dic.overTestScores.Length)
                {
                    decimal[][] over2 = new decimal[dic.overNumbers.Length][];
                    for (int r = 0; r < dic.overNumbers.Length; r++)
                    {
                        over2[r] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        over2[r][0] = dic.overNumbers[r];
                        over2[r][1] = dic.overTestScores[r];
                    }
                    json["Over2"] = over2;

                }
                #endregion
                json["ChartData"] = dic;
                json["ChartCreater"] = cc;
                List<decimal> maxs = new List<decimal>();
                List<decimal> mins = new List<decimal>();
                if (dic.testScores != null && dic.testScores.Length != 0)
                {
                    maxs.Add(dic.testScores.Max());
                    mins.Add(dic.testScores.Min());
                }
                if (dic.overCLScores != null && dic.overCLScores.Length != 0)
                {
                    maxs.Add(dic.overCLScores.Max());
                    mins.Add(dic.overCLScores.Min());
                }
                if (dic.overTestScores != null && dic.overTestScores.Length != 0)
                {
                    maxs.Add(dic.overTestScores.Max());
                    mins.Add(dic.overTestScores.Min());
                }
                json["Model"] = dic;
                json["maxValue"] = GetMinValueChart(dic, maxs.Max(), mins.Min(), true);
                json["minValue"] = GetMinValueChart(dic, maxs.Max(), mins.Min(), false);
                json["maxPoint"] = maxs.Max();
                json["minPoint"] = mins.Min();

                return Ok(json);

            }
            catch (Exception e)
            {
                json["Error"] = e.Message + "-" + e.StackTrace;
                return Ok(json);
            }


        }
        [Route("MRChart")]
        [HttpPost]
        public IHttpActionResult MRChart(ChartQuery note)
        {
            JsonObject json = new JsonObject();
            if (Convert.ToDateTime(note.BeginTime) >= Convert.ToDateTime(note.EndTime))
            {
                json["Error"] = "Begin Time <End Time";
                return Ok(json); ;
            }
            try
            {
                int AB = Convert.ToInt32(note.AB);
                if (note.Isdelegate)
                    AB = 4;

                ChartCreater cc = new ChartCreater();
                int i;
                i = cc.FillData(note.SampleName, note.LOT_NO, note.Line, note.Property, Convert.ToDateTime(note.BeginTime), Convert.ToDateTime(note.EndTime), AB);
                TrendChart dic = new TrendChart();
                cc.IChart(ref dic);
                json["Model"] = dic;
                #region 趋势图

                if (dic.Number != null && dic.testScores != null && dic.Number.Length != 0 && dic.testScores.Length != 0)
                {
                    decimal[][] red = new decimal[dic.Number.Length][];
                    for (int r = 0; r < dic.Number.Length; r++)
                    {
                        red[r] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        red[r][0] = dic.Number[r];
                        red[r][1] = dic.testScores[r];
                    }
                    json["Trend"] = red;

                    List<decimal> allLines = new List<decimal>();
                    if (dic.USL != decimal.MinValue)
                        allLines.Add(dic.USL);
                    if (dic.LSL != decimal.MinValue)
                        allLines.Add(dic.LSL);
                    if (dic.CL != decimal.MinValue)
                        allLines.Add(dic.CL);
                    if (dic.UCL != decimal.MinValue)
                        allLines.Add(dic.UCL);
                    if (dic.LCL != decimal.MinValue)
                        allLines.Add(dic.LCL);
                    if (dic.LUCL != decimal.MinValue)
                        allLines.Add(dic.LUCL);
                    if (dic.LLCL != decimal.MinValue)
                        allLines.Add(dic.LLCL);
                    if (dic.X != decimal.MinValue)
                        allLines.Add(dic.X);

                    #region 最大值的点
                    if (red != null && red.Length != 0)
                    {
                        decimal[][] maxSpots = new decimal[2][];
                        maxSpots[0] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        maxSpots[0][0] = red[0][0];
                        maxSpots[0][1] = allLines.Max();
                        maxSpots[1] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        maxSpots[1][0] = red[red.Length - 1][0];
                        maxSpots[1][1] = allLines.Max();

                        json["MaxSpots"] = maxSpots;
                    }
                    #endregion

                    #region 最小值的点
                    if (red != null && red.Length != 0)
                    {
                        decimal[][] minSpots = new decimal[2][];
                        minSpots[0] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        minSpots[0][0] = red[0][0];
                        minSpots[0][1] = allLines.Min();
                        minSpots[1] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        minSpots[1][0] = red[red.Length - 1][0];
                        minSpots[1][1] = allLines.Min();

                        json["MinSpots"] = minSpots;
                    }
                    #endregion
                }
                #endregion

                #region 超线的点1
                if (dic.overCLNumbers != null && dic.overCLScores != null
                    && dic.overCLNumbers.Length != 0 && dic.overCLScores.Length != 0 && dic.overCLNumbers.Length == dic.overCLScores.Length)
                {
                    decimal[][] over1 = new decimal[dic.overCLNumbers.Length][];
                    for (int r = 0; r < dic.overCLNumbers.Length; r++)
                    {
                        over1[r] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        over1[r][0] = dic.overCLNumbers[r];
                        over1[r][1] = dic.overCLScores[r];
                    }
                    json["Over1"] = over1;

                }
                #endregion

                #region 超线的点2
                if (dic.overNumbers != null && dic.overTestScores != null
                    && dic.overNumbers.Length != 0 && dic.overTestScores.Length != 0 && dic.overNumbers.Length == dic.overTestScores.Length)
                {
                    decimal[][] over2 = new decimal[dic.overNumbers.Length][];
                    for (int r = 0; r < dic.overNumbers.Length; r++)
                    {
                        over2[r] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        over2[r][0] = dic.overNumbers[r];
                        over2[r][1] = dic.overTestScores[r];
                    }

                    json["Over2"] = over2;
                    //ViewBag.Over2 = over2;
                }
                #endregion

                #region MRChart
                TrendChart dicR = new TrendChart();
                cc.MRChart(ref dicR);

                #region 趋势图
                if (dicR.Number != null && dicR.testScores != null
                    && dicR.Number.Length != 0 && dicR.testScores.Length != 0 && dicR.Number.Length == dicR.testScores.Length)
                {
                    decimal[][] trendR = new decimal[dicR.Number.Length][];
                    for (int r = 0; r < dicR.Number.Length; r++)
                    {
                        trendR[r] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        trendR[r][0] = dicR.Number[r];
                        trendR[r][1] = dicR.testScores[r];
                    }

                    json["TrendR"] = trendR;
                    List<decimal> allLinesMR = new List<decimal>();
                    if (dicR.UCL != decimal.MinValue)
                        allLinesMR.Add(dicR.UCL);
                    if (dicR.LCL != decimal.MinValue)
                        allLinesMR.Add(dicR.LCL);
                    if (dicR.MR != decimal.MinValue)
                        allLinesMR.Add(dicR.MR);
                    #region MR图最大值的点
                    if (trendR != null && trendR.Length != 0)
                    {
                        decimal[][] maxSpotsMR = new decimal[2][];
                        maxSpotsMR[0] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        maxSpotsMR[0][0] = trendR[0][0];
                        maxSpotsMR[0][1] = allLinesMR.Max();
                        maxSpotsMR[1] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        maxSpotsMR[1][0] = trendR[trendR.Length - 1][0];
                        maxSpotsMR[1][1] = allLinesMR.Max();

                        json["MaxSpotsMR"] = maxSpotsMR;
                    }
                    #endregion

                    #region MR图最小值的点
                    if (trendR != null && trendR.Length != 0)
                    {
                        decimal[][] minSpotsMR = new decimal[2][];
                        minSpotsMR[0] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        minSpotsMR[0][0] = trendR[0][0];
                        minSpotsMR[0][1] = allLinesMR.Min();
                        minSpotsMR[1] = (decimal[])Array.CreateInstance(typeof(decimal), 2);
                        minSpotsMR[1][0] = trendR[trendR.Length - 1][0];
                        minSpotsMR[1][1] = allLinesMR.Min();
                        json["MinSpotsMR"] = minSpotsMR;

                    }
                    #endregion
                }
                #endregion

                json["DicR"] = dicR;
                #endregion
                json["ChartCreater"] = cc;

                return Ok(json);
            }
            catch (Exception e)
            {
                json["Error"] = e.Message + "-" + e.StackTrace;
                return Ok(json);
            }
        }
        private string CheckDraftRequestion(Entrusted entrust)
        {
            if (string.IsNullOrEmpty(entrust.SampleName.Trim()))
            {
                return string.Format("ERROR┆{0}", "SampleName is NULL！");
            }
            if (string.IsNullOrEmpty(entrust.LOT_NO))
            {
                return string.Format("ERROR┆{0}", "Material is NULl！");
            }
            if (string.IsNullOrEmpty(entrust.SampleFrom))
            {
                return string.Format("ERROR┆{0}", "From is null！");
            }
            if (string.IsNullOrEmpty(entrust.Purpose))
            {
                return string.Format("ERROR┆{0}", "Purpose is null！");
            }



            if (entrust.Properties.Length < 1)
            {
                return string.Format("ERROR┆{0}", "Test project is null");

            }
            return string.Empty;

        }


        /// <summary>
        /// Save Entrust class into DB  创建委托单
        /// </summary>
        /// <param name="entrust"></param>
        /// <returns></returns>

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Create(Entrusted entrust)
        {
            JsonObject json = new JsonObject();

            try
            {
                string msg = CheckDraftRequestion(entrust);
                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                    return Ok(json);
                }

                string lab = entrust.Spec.Substring(1, 1);
                string voucherId = draftDAL.GetDocID(lab);

                string draftID = entrust.DraftID == null ? "" : entrust.DraftID.ToString();
                if (!string.IsNullOrEmpty(draftID))
                {
                    string v = voucherId.Substring(6, 2);
                    string s = draftID.Substring(5, 2);
                    if (v != s)
                    {
                        json["Error"] = string.Format("ERROR┆{0}", "跨月请点结束本次委托，再建单！");
                        return Ok(json);
                    }

                }
                if (string.IsNullOrEmpty(voucherId))
                {
                    json["Error"] = string.Format("ERROR┆{0}", "得到单号为空！");
                    return Ok(json);

                }
                string[] strs = new string[entrust.Properties.Length];
                for (int i = 0; i < entrust.Properties.Length; i++)
                {
                    strs[i] = (entrust.Properties[i].PropertyName);

                }

                if (draftDAL.InsertRequesition(entrust.UserID, ref draftID, voucherId,
                     entrust.SampleName, entrust.LOT_NO, entrust.Purpose,
                     entrust.SampleFrom, Convert.ToDateTime(entrust.RequireDate), Convert.ToDateTime(entrust.SendDate),
                     Convert.ToDateTime(entrust.GetDate), strs))
                {

                    json["VoucherId"] = voucherId;
                    json["DraftID"] = draftID;
                }
                else
                {
                    json["Error"] = "Save ReqInDraft  failed";

                }
                return Ok(json);
            }
            catch (Exception e)
            {
                MSG_Error = e;
                json["Error"] = e.Message + "-" + e.StackTrace;
                return Ok(json);
                // throw new Exception(e.Message);
            }

        }
        [Route("GetDraft")]
        [HttpGet]
        public IHttpActionResult GetDraft(string draftID)
        {
            return Ok(draftDAL.GetReqInDraft(draftID));

        }
        /// <summary>
        /// Get Department following by Entrusted vouchers that exist - created by Jang 2018-11-15
        /// </summary>
        /// <returns></returns>
        [Route("GetEntrustedDepartment")]
        [HttpGet]
        public IHttpActionResult GetEntrustedDepartment()
        {
            return Ok(draftDAL.GetEntrustedDepartment());
        }

        /// <summary>
        /// Get Entrust Voucher with conditional
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="sendB"></param>
        /// <param name="sendE"></param>
        /// <param name="owner"></param>
        /// <param name="state"></param>
        /// <returns></returns>

        [Route("GetEntrustVoucher")]
        [HttpGet]
        public IHttpActionResult GetEntrustVoucher(string userID, DateTime sendB, DateTime sendE, string owner, string state)
        {
            JsonObject json = new JsonObject();
            try
            {
                DateTime sendEDate = Convert.ToDateTime(sendE).AddDays(1);
                DataTable dt = draftDAL.GetReqVoucher(userID, sendB, sendEDate, owner, state);

                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                json["Error"] = e.Message + " - " + e.StackTrace;
                return Ok(json);
            }

        }
        /// <summary>
        /// Get Properties by Voucher ID create by Marco 1/8/2018
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        [Route("getPropByVoucherID")]
        [HttpGet]
        public IHttpActionResult getPropByVoucherID(string voucherID)
        {
            JsonObject json = new JsonObject();
            try
            {
                DataTable dt = draftDAL.GetProfileList(voucherID);
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                json["Error"] = e.Message + "-" + e.StackTrace;
                return Ok(json);
            }
        }

        /// <summary>
        /// 修改加测的状态，如删除
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Route("DeletePlanAddStatus")]
        [HttpPost]
        public IHttpActionResult DeletePlanAddStatus(string voucherID)
        {
            if (string.IsNullOrEmpty(voucherID))
                return BadRequest("Document number is empty！");
            try
            {
                if (draftDAL.DeletePlandAdd(voucherID))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Update Failed！");
                }
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }

        }

        /// <summary>
        /// 修改加测的状态，如删除
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Route("DeleteRequisionStatus")]
        [HttpPost]
        public IHttpActionResult DeleteRequisionStatus(string voucherID)
        {
            if (string.IsNullOrEmpty(voucherID))
                return BadRequest("Document number is empty！");
            try
            {
                //RMXQ180710008
                // string vouid = "R" + voucherID.Substring(0, 8) + voucherID.Substring(voucherID.Length - 4, 4);
                if (draftDAL.DeleteRequistion(voucherID))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Update Failed！");
                }
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return BadRequest(e.Message + e.StackTrace);
            }

        }
        /// <summary>
        /// Save Plan Add data into DB create by Marco 1/8/2018
        /// </summary>
        /// <param name="planAdd"></param>
        /// <returns></returns>
        [Route("CreatePlanAdd")]
        [HttpPost]
        public IHttpActionResult CreatePlanAdd(PlanAddDto planAdd)
        {
            JsonObject json = new JsonObject();
            GradeVersionDAL gradeVersion = new GradeVersionDAL();
            string version = gradeVersion.CheckGradeVersion(planAdd.SampleName, planAdd.LOT_NO);
            if (string.IsNullOrEmpty(version))
            {
                json["Error"] = "Grade Version is NULL";
                return Ok(json);
            }
            planAdd.GradeVersion = new Guid(version);

            try
            {

                DOCPlanAdd sd = JsonConvert.DeserializeObject<DOCPlanAdd>(JsonConvert.SerializeObject(planAdd)); ;
                if (!draftDAL.CreatePlanAdd(sd))
                {
                    json["Error"] = "Can not Save Plan Add";
                    return Ok(json);
                }
                else
                {
                    json["Success"] = "Success";
                    return Ok(json);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }
        /// <summary>
        /// get Data from PlanAdd Table in DB create by Elric 27/2/2018
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="sampleName"></param>
        /// <param name="lot_No"></param>
        /// <returns></returns>
        [Route("QueryPlanAdd")]
        [HttpGet]
        public IHttpActionResult QueryPlanAdd(string userID, DateTime begin, DateTime end, string sampleName, string lot_No)
        {
            JsonObject json = new JsonObject();
            try
            {
                DateTime EndDate = Convert.ToDateTime(end).AddDays(1);
                DataTable dt = draftDAL.QueryPlanAdd(userID, begin, EndDate, sampleName, lot_No);
                return Ok(_helper.ConvertJson(dt));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }

        }

        /// <summary>
        /// get Data from Recevie Table in DB create by Elric 27/2/2018
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="sampleName"></param>
        /// <returns></returns>
        [Route("QueryReceive")]
        [HttpGet]
        public IHttpActionResult QueryReceive(DateTime B, DateTime E, string SampleName)
        {
            JsonObject json = new JsonObject();
            try
            {
                // DateTime EndDate = Convert.ToDateTime(E).AddDays(1);
                DataTable dt = otherQueryDAL.QueryReceive(B, E, SampleName);
                return Ok(_helper.ConvertJson(dt));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }

        }
        /// <summary>
        /// Get Detail Recieve
        /// </summary>
        /// <param name="VoucherID"></param>
        /// <returns></returns>
        [Route("ReceiveDetailsByVoucherID")]
        [HttpGet]
        public IHttpActionResult ReceiveDetailsByVoucherID(string VoucherID)
        {
            JsonObject json = new JsonObject();
            try
            {
                DataTable dt = otherQueryDAL.ReceiveDetailsByVoucherID(VoucherID);
                return Ok(_helper.ConvertJson(dt));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }

        }


        /// <summary>
        /// get Entrusted data details in DB create by Marco 1/9/2017
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        [Route("GetDelegateDetailsByVoucherID")]
        [HttpGet]
        public IHttpActionResult GetDelegateDetailsByVoucherID(string voucherID)
        {

            try
            {
                return Ok(_helper.ConvertJson(draftDAL.GetDelegateDetailsByVoucherID(voucherID)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }
        [Route("GetPlans")]
        [HttpGet]
        public IHttpActionResult GetPlans(string sampleName, string lot_no, string line, DateTime b, DateTime e)
        {
            JsonObject json = new JsonObject();
            List<JsonObject> ls = new List<JsonObject>();
            try
            {
                var dt1 = otherQueryDAL.QueryPlans(sampleName, lot_no, line, b, e);
                return Ok(_helper.ConvertJson(dt1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }
        [Route("GetMaterialOfPlans")]
        [HttpGet]
        public IHttpActionResult GetMaterialOfPlans(string sampleName, string line, DateTime b, DateTime e)
        {
            DataTable dt = otherQueryDAL.GetMaterialOfPlans(sampleName, line, b, e);
            return Ok(dt);
        }



        [Route("GetAnalysisQuery")]
        [HttpGet]
        public IHttpActionResult GetAnalysisQuery(string sampleName, string material, DateTime B, DateTime E, string Lang, string VoucherType)
        {

            try
            {
                JsonObject json = new JsonObject();
                var dt = draftDAL.ReportInComingAnalysis(sampleName, material, B, E, Lang, VoucherType);
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }

        }
        [Route("GetReportByVoucherID")]
        [HttpGet]
        public IHttpActionResult GetReportByVoucherID(string VoucherID)
        {

            try
            {
                JsonObject json = new JsonObject();
                var dt = draftDAL.GetReportByVoucherID(VoucherID);
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }

        }

        [Route("IsNew_EntrustedVoucher_General")]
        [HttpGet]
        public IHttpActionResult IsNew_EntrustedVoucher_General(string VoucherID)
        {

            try
            {
                JsonObject json = new JsonObject();
                var result = draftDAL.IsNew_EntrustedVoucher_General(VoucherID);
                json["IsNewVoucher"] = result;
                return Ok(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }

        }


        [Route("UpdateStatus_EntrustedVoucher_General")]

        [HttpGet]
        public IHttpActionResult UpdateStatus_EntrustedVoucher_General(string voucherid, string statusofsubmit, string qualifedstring)
        {
            var result = draftDAL.UpdateStatus_EntrustedVoucher_General(voucherid, statusofsubmit, qualifedstring);
            JsonObject json = new JsonObject();
            json["Success"] = result;
            return Ok(json);
        }


        [Route("GetData4ReportSSPnPoly")]
        [HttpGet]
        public IHttpActionResult GetData4Report(string sampleName, string lot_No, string line, DateTime from, DateTime to, bool type)
        {
            try
            {
                List<JsonObject> list = new List<JsonObject>();
                JsonObject json = new JsonObject();
                DataTable dttemp = new DataTable();//属性
                string[] arr1 = new string[] { "MAX", "MIN", "AVG", "SD" };

                dttemp = draftDAL.GetData4ReportSPPnPOLY(sampleName, lot_No, line, from, to, type);
                if (dttemp.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Line", typeof(string));//add Line
                    dt.Columns.Add("LOT_NO", typeof(string));//Add LOT_NO
                    dt.Columns.Add("DateCreate", typeof(string));//Add DateCreate
                    dt.Columns.Add("Grade", typeof(string));// add Grade
                    var getData = (from p in dttemp.AsEnumerable()
                                   select new
                                   {
                                       Line = p.Field<string>("LINE"),
                                       LOT_NO = p.Field<string>("LOT_NO"),
                                       ProdDate = p.Field<DateTime>("ProdDate"),
                                       Grade = p.Field<string>("Grade"),
                                   }).Distinct();
                    foreach (var item in getData)
                    {
                        dt.Rows.Add(item.Line.ToString(), item.LOT_NO.ToString(), Convert.ToDateTime(item.ProdDate).ToString(), item.Grade);

                    }
                    var dtProperty = (from p in dttemp.AsEnumerable()
                                      select new
                                      {
                                          Line = p.Field<string>("Line"),
                                          LOT_NO = p.Field<string>("LOT_NO"),
                                          PropertyName = p.Field<string>("PropertyName")
                                      }).Distinct();

                    foreach (var item in dtProperty)
                    {
                        if (!dt.Columns.Contains(item.PropertyName))
                        {
                            dt.Columns.Add(item.PropertyName.ToString(), typeof(string));//添加“属性”列
                        }


                    }

                    foreach (DataRow rowsdt in dt.Rows)
                    {


                        foreach (DataRow rowstemp in dttemp.Rows)
                        {

                            string propertyName = rowstemp["PropertyName"].ToString();
                            if (rowsdt["LOT_NO"].ToString() == rowstemp["LOT_NO"].ToString() && rowsdt["DateCreate"].ToString() == rowstemp["ProdDate"].ToString() && rowsdt["Grade"].ToString() == rowstemp["Grade"].ToString())
                            {
                                //获取等级和属性范围
                                rowsdt[propertyName] = Convert.ToDecimal(rowstemp["Result"]).ToString("N2");
                            }
                        }
                    }

                    foreach (var rowsdt in (from p in dttemp.AsEnumerable()
                                            select new
                                            {
                                                Line = p.Field<string>("Line"),
                                                LOT_NO = p.Field<string>("LOT_NO")
                                            }).Distinct())
                    {
                        JsonObject jsonitem = new JsonObject();
                        JsonObject jsonMaster = new JsonObject();


                        for (var i = 0; i < arr1.Length; i++)
                        {
                            var arrItem = arr1[i];
                            JsonObject jsondetail = new JsonObject();
                            foreach (var rowstemp in dtProperty)
                            {


                                if (rowsdt.LOT_NO == rowstemp.LOT_NO && rowsdt.Line == rowstemp.Line)
                                {

                                    string propertyName = rowstemp.PropertyName;
                                    List<decimal> rowsForColumn3 = dt.AsEnumerable().Where(x => Convert.ToDecimal(x.Field<string>(propertyName)) != 0 && Convert.ToDecimal(x.Field<string>(propertyName)) != null && x.Field<string>("LOT_NO") == rowsdt.LOT_NO && x.Field<string>("Line") == rowstemp.Line).Select(x => Convert.ToDecimal(x.Field<string>(propertyName))).ToList();
                                    if (arrItem == "MAX")
                                    {

                                        jsondetail[propertyName] = rowsForColumn3.Max().ToString("N2");

                                    }
                                    if (arrItem == "MIN")
                                    {

                                        jsondetail[propertyName] = rowsForColumn3.Min().ToString("N2");

                                    }
                                    if (arrItem == "AVG")
                                    {

                                        jsondetail[propertyName] = rowsForColumn3.Average().ToString("N2");
                                    }
                                    if (arrItem == "SD")
                                    {
                                        jsondetail[propertyName] = CalculateStdDev(rowsForColumn3).ToString("N2");
                                    }
                                    jsonitem["LINE"] = rowstemp.Line;
                                }

                                jsonitem[arrItem] = jsondetail;

                            }


                            jsonMaster[rowsdt.LOT_NO] = jsonitem;
                        }
                        list.Add(jsonMaster);
                    }
                    json["masterData"] = dt.Select().OrderBy(o => o["LOT_NO"]).ThenByDescending(o => o["Line"]).CopyToDataTable();
                    json["detailData"] = list;
                }
                return Ok(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
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
        private double CalculateStdDev(IEnumerable<decimal> values)
        {
            double ret = 0;
            if (values.Count() > 0)
            {
                //Compute the Average      
                double avg = (double)values.Average();
                //Perform the Sum of (value-avg)_2_2      
                double sum = values.Sum(d => Math.Pow((double)d - avg, 2));
                //Put it all together      
                ret = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return Double.IsNaN(ret) ? 0 : ret;
        }
        [Route("GetOverGradeChart")]
        [HttpGet]
        public IHttpActionResult GetOverGradeChart(string samplename, string propertyname, string b, string e, string line)
        {
            JsonObject json = new JsonObject();
            try
            {
                var dt = otherQueryDAL.Report_Q_GetChartPlans(samplename, propertyname, b, e, line);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                values.Add("getheader", _helper.ConvertJson(dt[0]));
                values.Add("getdetail", _helper.ConvertJson(dt[1]));
                values.Add("getchart", _helper.ConvertJson(dt[2]));
                return Ok(values);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }

        }
        [Route("Get_BarcodeInStock")]
        [HttpGet]
        [Obsolete]
        public IHttpActionResult Get_BarcodeInStock(int pageIndex, int pageSize, string des, string B, string E, string SampleName,string MaterialNO ,string Line, string Group, string Status)
        {
            JsonObject json = new JsonObject();
            try
            {
                var dt = otherQueryDAL.Get_BarcodeInStock(pageIndex, pageSize, des, B, E, SampleName, MaterialNO, Line, Group, Status);


                return Ok(dt);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }
        [Route("GET_EvaluateStapleGrade")]
        [HttpGet]
        [Obsolete]
        public IHttpActionResult GET_EvaluateStapleGrade(string B, string E, string MaterialNO, string Line, string Group)
        {
            JsonObject json = new JsonObject();
            try
            {
                var dt = otherQueryDAL.GET_EvaluateStapleGrade(B, E, MaterialNO, Line, Group);


                return Ok(dt);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }
        [Route("Save_StaplePickForGrade")]
        [HttpPost]
        [Obsolete]
        public IHttpActionResult Save_StaplePickForGrade(ListStaplePickForGrade listStaplePickForGrade)
        {
            JsonObject json = new JsonObject();



            try
            {
                if (listStaplePickForGrade.list_StaplePickForGrade.Count > 0)
                {
                    foreach (StaplePickForGrade item in listStaplePickForGrade.list_StaplePickForGrade)
                    {
                        if (item.isModifyGrade)
                        {
                            item.ModifyGradeDate = DateTime.Now;
                        }
                        else
                        {
                            item.PickDate = DateTime.Now;
                        }

                        item.Status = "S";
                        var _result = draftDAL.Save_StaplePickForGrade(item);
                        if (!_result)
                        {
                            json["Error"] = "Can not Save StaplePickForGrade " + item.BarCode;
                            return Ok(json);
                        }
                    }
                    json["Success"] = "Success";
                }
                else
                {
                    json["Error"] = "ListStaplePickForGrade is empty ";
                }
                return Ok(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }
        [Route("StapleFiber_ReportByBatch")]
        [HttpGet]
        [Obsolete]
        public IHttpActionResult StapleFiber_ReportByBatch(string B, string E, string ReportType)
        {
            JsonObject json = new JsonObject();
            Dictionary<string, Object> values = new Dictionary<string, object>();
           
            try
            {
                DataTable dt = draftDAL.StapleFiber_ReportByBatch(B, E, ReportType);
                values.Add("TableData", dt);
                return Ok(values);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }
        [Route("GET_PrecValueBySampleName")]
        [HttpGet]
        [Obsolete]
        public IHttpActionResult GET_PrecValueBySampleName(string SampleName)
        {
            JsonObject json = new JsonObject();
            Dictionary<string, Object> values = new Dictionary<string, object>();
            try
            {
                var dt = draftDAL.GET_PrecValueBySampleName(SampleName);

                values.Add("TableData", dt);
                return Ok(values);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

                return BadRequest(ex.Message + "-" + ex.StackTrace);
            }
        }

    }
}
