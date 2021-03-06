using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS.Model;

namespace QC.DAL
{
    public class OtherQueryDAL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");


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
                       new string[] { "B", "E", "SampleName" },
                       new object[] { startTime, endTime, SampleName }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }

        /// GET QUERY PLAN LIMS 
        /// <param name="sampleName"></param>
        /// <param name="lot_no"></param>
        /// <param name="line"></param>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public DataTable QueryPlans(string sampleName, string lot_no, string line, DateTime b, DateTime e)
        {
            try
            {
                if (string.IsNullOrEmpty(lot_no.Trim())) lot_no = "%";
                if (string.IsNullOrEmpty(line.Trim())) line = "%";
                b = b.Date;
                e = e.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                string[] parameters = new string[] { "UserID", "SampleName", "LOT_NO", "LINE", "B", "E" };
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_GetPlans",
                    parameters, new object[] { "", sampleName, lot_no, line, b, e }).Tables[0];
                string str1, str2;
                dt.Columns.Remove("VoucherID");
                dt.Columns.Remove("SampleName");
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    for (int j = 4; j < dt.Columns.Count; j++)
                    {
                        var grade = dt.Rows[i]["Grade"].ToString();
                        if (dt.Rows[i][j].ToString().Contains("$"))
                        {
                            dt.Rows[i]["Grade"] = "#" + grade.Replace("^", "");
                            break;
                        }
                        if (dt.Rows[i][j].ToString().Contains("~"))
                        {
                            if (!grade.Contains("^")) dt.Rows[i]["Grade"] = "^" + grade;

                        }
                    }
                    if (i == 0) continue;
                    str1 = dt.Rows[i - 1]["LINE"].ToString();
                    str2 = dt.Rows[i]["LINE"].ToString();
                    if (str1 != str2 && str1 != "**")
                    {
                        DataRow myRow = dt.NewRow();
                        myRow["LOT_NO"] = "xxx";
                        dt.Rows.InsertAt(myRow, i);

                        i++;
                    }
                }
                #region Unknow function
                if (dt != null && dt.Rows.Count != 0)
                {
                    string sql4 = @"SELECT p.VoucherID,p.filePath
                                FROM Plans p
                                WHERE ISNULL(p.filePath,'') <> '' 
                                AND p.[State] = '2'
                                AND p.SampleName = @SampleName
                                AND p.LOT_NO LIKE '%'+@LOT_NO+'%'
                                AND p.LINE LIKE '%'+@LINE+'%'
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

                #endregion
                dt.AcceptChanges();

                return dt;
            }
            catch (Exception ex)
            {
                log.Error(e);

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleName"></param>
        /// <param name="line"></param>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public DataTable GetMaterialOfPlans(string sampleName, string line, DateTime b, DateTime E)
        {
            try
            {

                return acQC.DbHelper.ExecuteStoredProcedure("Q_GetMaterialOfPlans",
                    new string[] { "SampleName", "Line", "B", "E" },
                    new object[] { sampleName, line, b, E }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
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
        /// Get In Process Analysis
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
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Create By Isaac 2018-11-19
        /// Query for R/Y Voucher
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Lot_no"></param>
        /// <param name="B"></param>
        /// <param name="E"></param>
        /// <returns></returns>

        public DataTable SearchRYVoucher(string materialType,string voucherid, string UserID, string sampleName, string Lot_no, string status, string colorlabel, DateTime? B, DateTime? E, string Line)
        {
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("RY_SearchISO",
                    new string[] { "VoucherID", "UserID", "SampleName", "LOT_NO", "Status", "ColorLabel", "B", "E", "Line", "MaterialType" },
                    new object[] { voucherid, UserID, sampleName, Lot_no, status, colorlabel, B, E, Line,materialType }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }

        public DataTable CreateVoucher(Report_ISO_Result entity)
        {
            Console.WriteLine("Report_ISO_Result-AddISO():" + DateTime.Now.ToString());

            try
            {
                DataTable dt = new DataTable();
                if (entity.SampleName== "S03020001")
                {
                     dt = acQC.DbHelper.ExecuteStoredProcedure("RY_Create_Stable_ISO_Voucher",
                 new string[] { "UserID", "SampleName", "LOT_NO", "ColorLabel", "B", "E", "Line", "VoucherType" },
                 new object[] { entity.CreateBy, entity.SampleName, entity.LOT_NO, entity.ColorLabel, entity.BeginDate, entity.EndDate, entity.Line,entity.ColorLabel }).Tables[0];
                }
                else
                {
                     dt = acQC.DbHelper.ExecuteStoredProcedure("RY_Create_ISO_Voucher",
                  new string[] { "UserID", "SampleName", "LOT_NO", "ColorLabel", "B", "E", "Line","Vendor" },
                  new object[] { entity.CreateBy, entity.SampleName, entity.LOT_NO, entity.ColorLabel, entity.BeginDate, entity.EndDate, entity.Line,entity.Vendor }).Tables[0];
                }


              
                return dt;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception("Create ReportISO_Result Eror:" + e.Message);
            }
        }




        public Boolean GetRYVoucher(string voucherid)
        {
            Console.WriteLine("Report_ISO_Get_ISOResult():" + DateTime.Now.ToString());
            try
            {

                var dt = acQC.DbHelper.ExecuteStoredProcedure("RY_GetRYVoucher",
                    new string[] { "VoucherID" },
                    new object[] { voucherid }).Tables[0];

                return dt.Rows.Count > 0 ? true : false;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }

        public bool UpdateRYResult(string userID, string voucherID, string state, string status, string reason, string solution, string prevention, string remark)
        {
            Console.WriteLine("Report_ISO_Result-Update_ISO_Approve():" + DateTime.Now.ToString());
            try
            {

                var dt = acQC.DbHelper.ExecuteStoredProcedure("RY_UpdateRYVoucher",
                    new string[] { "UserID", "VoucherID", "State", "Status", "Reason", "Solution", "Prevention", "Stamp", "Remark" },
                    new object[] { userID, voucherID, state, status, reason, solution, prevention, DateTime.Now, remark }).Tables[0];

                return dt.Rows.Count > 0 ? true : false;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }


        public bool UpdateStatusVoucher(string voucherID, string status, string userid, Report_ISO_Detail propertyNameList)
        {
            Console.WriteLine("DOCPlanAddDAL--DeletePlandAdd " + DateTime.Now);
            if(propertyNameList != null) {
                if (propertyNameList.Status != "X")
                {
                    foreach (var item in propertyNameList.ListProperty)
                    {
                        string TotalBag = "";
                        if (item.PropertyValue.Contains("&"))
                        {
                            string[] PropertyArray = item.PropertyValue.Split('&');
                            for (int i = 0; i < PropertyArray.Length; i++)
                            {
                                if (i > 0)
                                {
                                    TotalBag += " & ";
                                }
                                if (PropertyArray[i].ToString().Trim().Contains("-"))
                                {
                                    string[] PropertyDetail = PropertyArray[i].Split('-');
                                    string first_param = PropertyDetail[0].Trim();
                                    string second_param = PropertyDetail[1].Trim();
                                    decimal number_1 = Convert.ToDecimal(string.Concat(first_param.Substring(first_param.Length - 4, 4).Reverse().Skip(1).Reverse()));
                                    decimal number_2 = Convert.ToDecimal(string.Concat(second_param.Substring(second_param.Length - 4, 4).Reverse().Skip(1).Reverse()));
                                    Decimal result = Decimal.Subtract(number_1, number_2);

                                    TotalBag += (Math.Abs(result) + 1);
                                }
                            }
                        }
                        else
                        {
                            if (item.PropertyValue.Trim().Contains("-"))
                            {

                                string[] PropertyDetail = item.PropertyValue.Split('-');
                                string first_param = PropertyDetail[0].Trim();
                                string second_param = PropertyDetail[1].Trim();
                                decimal number_1 = Convert.ToDecimal(string.Concat(first_param.Substring(first_param.Length - 4, 4).Reverse().Skip(1).Reverse()));
                                decimal number_2 = Convert.ToDecimal(string.Concat(second_param.Substring(second_param.Length - 4, 4).Reverse().Skip(1).Reverse()));
                                Decimal result = Decimal.Subtract(number_1, number_2);

                                TotalBag += (Math.Abs(result) + 1);
                            }
                        }
                        if (string.IsNullOrEmpty(TotalBag))
                        {
                            string updatecmd = @"Update Report_ISO_Detail SET [Range] = @Range
                            WHERE VoucherID = @VoucherID AND PropertyName=@PropertyName ";
                            acQC.ExecuteNonQuery(updatecmd, new object[] { item.PropertyValue, voucherID, item.PropertyName });
                        }
                        else
                        {
                            string updatecmd = @"Update Report_ISO_Detail SET [Range] = @Range, [TotalBag] = @TotalBag
                            WHERE VoucherID = @VoucherID AND PropertyName=@PropertyName ";
                            acQC.ExecuteNonQuery(updatecmd, new object[] { item.PropertyValue, TotalBag, voucherID, item.PropertyName });
                        }


                    }
                }
            }
           
           
            //核查是否能删除
            string cmd = @"Update Report_ISO_Result SET Status = @Status, Stamp = GETDATE()
                            WHERE VoucherID = @VoucherID; ";
            if (status == "P") cmd += "EXEC RY_AddResults_To_ISOVoucher '" + voucherID + "', @UserID";
            try
            {
                acQC.ExecuteNonQuery(cmd, new object[] { status, voucherID, userid });
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return true;
        }

        public bool RY_SendReminder(string voucherID, string userid, string formkey)
        {
            Console.WriteLine("QCOverGrade(SendReminder) " + DateTime.Now);

            //核查是否能删除
            string cmd = @"EXEC RY_SendReminder @voucherid, @userid, @formkey";
            try
            {
                acQC.ExecuteNonQuery(cmd, new object[] { voucherID, userid, formkey });
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return true;
        }



        /// <summary>
        /// Create By Isaac 2018-11-19
        /// Get for Details by VoucherID
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        public DataTable GetRYReportDetail(string voucherID)
        {
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("RY_Detail",
                    new string[] { "voucherID" },
                    new object[] { voucherID }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// jang 3614
        /// Get for All UQ Voucher between date
        /// </summary>
        /// <returns></returns>
        public DataTable Report_Q_RY_AllVoucher(DateTime? B, DateTime? E, string sampleName, string Lot_no, string status, string colorlabel, string Line)
        {
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("Report_Q_RY_AllVoucher",
                    new string[] { "B", "E", "SampleName", "LOT_NO", "Status", "Colorlabel", "Line" },
                    new object[] { B, E, sampleName, Lot_no, status, colorlabel, Line }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }
        ///
        public DataTable ISO_DetailExport(string voucherid, string UserID, string sampleName, string Lot_no, string status, string colorlabel, DateTime? B, DateTime? E, string Line)
        {
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("RY_ExportDetail",
                    new string[] { "VoucherID", "UserID", "SampleName", "LOT_NO", "Status", "ColorLabel", "B", "E", "Line" },
                    new object[] { voucherid, UserID, sampleName, Lot_no, status, colorlabel, B, E, Line }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Created By Jang 181224
        /// 
        /// </summary>
        /// <param name="B"></param>
        /// <param name="E"></param>
        /// <returns></returns>
        public List<DataTable> Report_Q_GetChartPlans(string samplename, string propertyname, string B, string E, string line)
        {
            try
            {

                var dt = acQC.DbHelper.ExecuteStoredProcedure("Q_GetChartPlans",
                    new string[] { "SampleName", "PropertyName", "B", "E", "Line" },
                    new object[] { samplename, propertyname, B, E, line });
                List<DataTable> values = new List<DataTable>();
                values.Add(dt.Tables[0]);
                values.Add(dt.Tables[1]);
                values.Add(dt.Tables[2]);
                return values;

            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }
        [Obsolete]
        public Dictionary<string, Object> Get_BarcodeInStock(int pageIndex, int pageSize, string des, string B, string E, string SampleName,string MaterialNO, string Line, string Group, string Status)
        {
            try
            {
                object[] outParameters = null;
               var dt =  acQC.ExecuteStoredProcedure(@"Get_BarcodeInStock", new string[] { "pageIndex", "pageSize", "Des", "B", "E","SampleName","MaterialNO","Line","Group","Status" }
                , new object[] { pageIndex, pageSize, des,B, E, SampleName, MaterialNO, Line,Group,Status },
                    new string[] { "Count" }, new DbType[] { DbType.Int32 }, out outParameters).Tables[0];
                Dictionary<string, Object> values = new Dictionary<string, object>();
                values.Add("TableData", dt);
                values.Add("TableCount", outParameters);
                return (values);
            }
            catch (Exception e)
            {

                log.Error(e);

                throw new Exception(e.Message);
            }
        }
        [Obsolete]
        public Dictionary<string, Object> GET_EvaluateStapleGrade( string B, string E, string MaterialNO, string Line, string Group)
        {
            try
            {
                
                var dt = acQC.ExecuteStoredProcedure(@"GET_EvaluateStapleGrade", new string[] { "B", "E", "MaterialNO", "Line", "Group" }
                 , new object[] {  B, E, MaterialNO, Line, Group }).Tables[0];
                Dictionary<string, Object> values = new Dictionary<string, object>();
                values.Add("TableData", dt);
                
                return (values);
            }
            catch (Exception e)
            {

                log.Error(e);

                throw new Exception(e.Message);
            }
        }

    }
   
}