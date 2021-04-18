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
    [RoutePrefix("api/LIMS/LIMSController")]
    public class LIMSController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        [Route("Test")]
        [HttpGet]

        public IHttpActionResult Test(string UserID)
        {
            try
            {
                DataTable dt = gate.DbHelper.Select(@"SELECT * FROM Plans", new object[] { }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }
        }

        [Route("leo123")]
        [HttpGet]
        public IHttpActionResult leo()
        {
            return Ok(new {a="123", b ="123"});
        }

        [Route("GetPlans")]
        [HttpGet]
        public IHttpActionResult GetPlans(string sampleName, string lot_no, string line, DateTime b, DateTime e)
        {
            try
            {
                if (string.IsNullOrEmpty(lot_no.Trim())) lot_no = "%";
                if (string.IsNullOrEmpty(line.Trim())) line = "%";
                b = b.Date;
                e = e.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                DataTable dt = new DataTable();
                string[] parameters = new string[] { "UserID", "SampleName", "LOT_NO", "LINE", "B", "E" };

                // Step 1
                dt.Merge(gate.DbHelper.ExecuteStoredProcedure("aspnet_q_GetPlansStep1", parameters, new object[]{
                            "Leo",sampleName,lot_no,line,b,e}).Tables[0]);
                // Step 2
                DataTable dt2 = gate.DbHelper.ExecuteStoredProcedure("aspnet_q_GetPlansStep2", parameters, new object[]{
                            "Leo",sampleName,lot_no,line,b,e}).Tables[0];

                if (dt2 != null && dt2.Rows.Count != 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        dt.Columns.Add(dt2.Rows[i]["PropertyName"].ToString(), typeof(string));
                    }
                }
                //Step 3
                DataTable dt3 = gate.DbHelper.ExecuteStoredProcedure("aspnet_q_GetPlansStep3", parameters, new object[]{
                            null,sampleName,lot_no,line,b,e}).Tables[0];
                if (dt3 != null && dt3.Rows.Count != 0)
                {
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        string voucherId = dt3.Rows[i]["VoucherID"].ToString();
                        string propertyName = dt3.Rows[i]["PropertyName"].ToString();
                        string overrange = dt3.Rows[i]["OverRange"].ToString();
                       

                        if (dt.Rows.Count != 0)
                        {
                            foreach (DataRow iRow in dt.Rows)
                            {
                                if ((string)iRow["VoucherID"] == voucherId)
                                {
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        if (dc.ColumnName == propertyName )
                                        {
                                            iRow[propertyName] = dt3.Rows[i]["Result"].ToString();
                                            if ((bool)dt3.Rows[i]["OverRange"])
                                            {
                                                if(!iRow[propertyName].ToString().Contains("$"))
                                                    iRow[propertyName] = "$" + iRow[propertyName];
                                                if (!iRow["Grade"].ToString().Contains("#"))
                                                    iRow["Grade"] = "#" + iRow["Grade"];
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                if (dt != null && dt.Rows.Count != 0)
                {
                    string sql4 = @"SELECT 
                                p.VoucherID,p.filePath
                                FROM Plans p
                                WHERE ISNULL(p.filePath,'') <> '' 
                                AND p.[State] NOT IN ('X','U')
                                AND p.SampleName = @SampleName
                                AND p.LOT_NO LIKE @LOT_NO
                                AND p.LINE LIKE @LINE
                                AND p.ProdDate >= @B
                                AND p.ProdDate <= @E
                                ";
                    string filePath = string.Empty;
                    DataTable dt4 = gate.SelectDataSet(sql4, new object[] { sampleName, lot_no, line, b, e }).Tables[0];
                    if (dt4 != null && dt4.Rows.Count != 0)
                    {

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (dt.Columns[i].ColumnName == "Melting point")
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    DataRow[] drs = dt4.Select("VoucherID = '" + row["VoucherID"] + "'", "VoucherID ASC");
                                    if (drs != null && drs.Length != 0)
                                    {
                                        filePath = drs.ToList().FirstOrDefault()["filePath"].ToString();
                                        //string fileName = filePath.Split('/')[filePath.Split('/').Length - 1];
                                        string html = string.Format(" <a href='{0}' target='_blank' download><span class='glyphicon glyphicon-file'></span></a>{1}", filePath, row["Melting point"].ToString());

                                        row["Melting point"] = html.Replace("'", "\"");
                                    }
                                }
                            }

                        }
                    }
                }


                dt.Columns.Remove("VoucherID");
                dt.Columns.Remove("SampleName");
                //dt.Columns.Remove("LOT_NO");

                string tmpStr = "";
                foreach (DataRow LotRow in dt.Rows)
                {
                    if ((string)LotRow["LOT_NO"] == tmpStr)
                        LotRow["LOT_NO"] = "";
                    else
                        tmpStr = LotRow["LOT_NO"].ToString();
                }
                dt.AcceptChanges();
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception ex)
            {
                MSG_Error = ex;
                throw new Exception(ex.Message);
            }
        }
        [Route("GetSampleReceive")]
        [HttpGet]
        public IHttpActionResult GetSampleReceive(string Lang)
        {
            try
            {

                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("aspnet_i_GetSampleReceive", new string[] { "UserID", "Lang" }, new object[] {"leo", 
               Lang }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
        [Route("QueryReceive")]
        [HttpGet]

        public IHttpActionResult QueryReceive(string B, string E, string SampleName)
        {
            try
            {//D_PtaEgs
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("aspnet_CC_Q_GetMasters4Receive", new string[] { "UserID", "B", "E", "SampleName" }, new object[] {"leo", B, E,
               SampleName }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
            }
        }
        // Get Details
        [Route("ReceiveDetailsByVoucherID")]
        [HttpGet]

        public IHttpActionResult ReceiveDetailsByVoucherID(string VoucherID)
        {
            try
            {//D_PtaEgs
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("aspnet_i_queryReceiveDetailsByVoucherID", new string[] { "VoucherID" }, new object[] {VoucherID}).Tables[0]; 
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw new Exception(e.Message);
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
