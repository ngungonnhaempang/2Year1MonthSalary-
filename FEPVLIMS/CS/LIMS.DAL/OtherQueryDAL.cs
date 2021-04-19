using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS.Model;
using Shawoo.Core;
using Shawoo.Common;

namespace LIMS.DAL
{
    public class OtherQueryDAL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");
        private DB db = new DB("LIMS");

        /// <summary>
        /// InComing query  for recieve
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SampleName"></param>
        /// <returns></returns>
        public DataTable QueryReceive(DateTime startTime, DateTime endTime, string SampleName)
        {
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("Q_GetMasters4Receive",
                       new string[] {"B", "E", "SampleName"},
                       new object[] { startTime, endTime, SampleName }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }
        public DataTable QueryPlans(string sampleName, string lot_no, string line, DateTime b, DateTime e)
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
                dt.Merge(acQC.DbHelper.ExecuteStoredProcedure("Q_GetPlansStep1", parameters, new object[]{
                            "",sampleName,lot_no,line,b,e}).Tables[0]);
                // Step 2
                DataTable dt2 = acQC.DbHelper.ExecuteStoredProcedure("Q_GetPlansStep2", parameters, new object[]{
                            "",sampleName,lot_no,line,b,e}).Tables[0];

                if (dt2 != null && dt2.Rows.Count != 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        dt.Columns.Add(dt2.Rows[i]["PropertyName"].ToString(), typeof(string));
                    }
                }
                //Step 3
                DataTable dt3 = acQC.DbHelper.ExecuteStoredProcedure("Q_GetPlansStep3", parameters, new object[]{
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
                                        if (dc.ColumnName == propertyName)
                                        {
                                            iRow[propertyName] = dt3.Rows[i]["Result"].ToString();
                                            if ((bool)dt3.Rows[i]["OverRange"])
                                            {
                                                if (!iRow[propertyName].ToString().Contains("$"))
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
                    DataTable dt4 = acQC.SelectDataSet(sql4, new object[] { sampleName, lot_no, line, b, e }).Tables[0];
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

                dt.AcceptChanges();
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
                return dt;
            }
            catch (Exception ex)
            {
                log.Error(e);
              
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get Test's property
        /// </summary>
        /// <param name="sampleName"></param>
        /// <param name="lot_no"></param>
        /// <returns></returns>
        public DataTable GetReceiveProfiles(string sampleName, string lot_no)
        {
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("Q_GetProfiles_Receive",
                    new string[] { "SampleName", "LOT_NO" },
                    new object[] { sampleName, lot_no }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);
              
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        public DataTable ReceiveDetailsByVoucherID(string voucherID)
        {
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("Q_GetReceiveDetailsByVoucherID",
                    new string[] { "VoucherID" },
                    new object[] { voucherID }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);
              
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get In Process Analysic
        /// </summary>
        /// <param name="sampleName"></param>
        /// <param name="lotNo"></param>
        /// <param name="line"></param>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public DataTable GetPlanList(string sampleName, string lotNo, string line, DateTime begin, DateTime end)
        {
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("Q_GetPlanList",
                    new string[] { "SampleName", "LOT_NO", "LINE", "B", "E" },
                    new object[] { sampleName, lotNo, line, begin, end }).Tables[0];
            }
            catch (Exception ex)
            {
                log.Error(ex);
               
                throw new Exception(ex.Message);
            }
        }
    }
}