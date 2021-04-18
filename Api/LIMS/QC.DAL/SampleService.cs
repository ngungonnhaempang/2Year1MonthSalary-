using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS.Model;
using Shawoo.Core;
using Newtonsoft.Json;
using QC.Model;

namespace QC.DAL
{
    public class SampleService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");
        private DB db = new DB("LIMS");

        public int monthCount(string lab)
        {
            if (lab.Trim().Length != 1) return -1;
            try
            {
                string sqlCommand = "SELECT isnull( max(Convert(int, SUBSTRING(ReqInDraft.VoucherID,10,4)))  +1 ,1) FROM Draft INNER JOIN  ReqInDraft ON Draft.DraftID = ReqInDraft.DraftID   WHERE (YEAR(Draft.CreateDate) = YEAR(GETDATE())) AND (SUBSTRING(ReqInDraft.VoucherID, 9, 1) =  @Lab) AND (MONTH(Draft.CreateDate) = MONTH(GETDATE())) ";
                return acQC.SelectScalar<int>(sqlCommand, new object[] { lab });
            }
            catch (Exception e)
            {
                log.Error(e);

                return -1;
            }

        }
        /// <summary>
        /// 得到委托的ID cassie
        /// </summary>
        /// <param name="lab"></param>
        /// <returns></returns>
        public string GetDocID(string lab)
        {
            DateTime date = DateTime.Now;
            int count = monthCount(lab);
            if (count == -1) return "";
            return string.Format("RMXQ{0}{1}{2}{3}", date.Year.ToString().Substring(2, 2),
                                                date.Month.ToString().PadLeft(2, '0'),
                                                lab,
                                                count.ToString().PadLeft(4, '0'));
        }

        public string GetDraftID(string lab)
        {
            DateTime date = DateTime.Now;
            string pre = string.Format("MXQ{0}{1}{2}", date.Year.ToString().Substring(2, 2),
                                                  date.Month.ToString().PadLeft(2, '0'),
                                                  lab);


            int flow = acQC.SelectScalar<Int32>(string.Format(@"
                                                 SELECT  isnull(max(Convert(int, SUBSTRING(DraftID,9,4))) +1,1)
                                                  FROM Draft
                                                 WHERE DraftID LIKE @Pre"),
                                              new object[] { pre + "%" });


            return pre + flow.ToString().PadLeft(4, '0');

        }
        public int draftCount(string voucherID)
        {
            try
            {


                string sqlCommand = @"SELECT isnull(max(Convert(int, SUBSTRING(DraftID,9,4))) +1,1)  FROM Draft 
                                                 WHERE LEFT(DraftID,8) = SUBSTRING(@VoucherID,2,8)";
                return acQC.SelectScalar<int>(sqlCommand, new object[] { voucherID });
            }
            catch (Exception e)
            {
                log.Error(e);
                return -1;
            }
        }

        public bool InsertRequesition(string usr, ref string draftID, string voucherID, string sampleName, string LotNo,
                                string purpose, string sampleFrom, DateTime requireDate, DateTime sendDate, DateTime getDate,
                                string[] profiles)
        {
            bool NeedDraft = false;
            if (draftID.Trim().Length < 11)
            {
                int count = draftCount(voucherID.Trim());
                if (count < 0)
                {
                    return false;
                }
                else
                {
                    // draftID = voucherID.Substring(1, 8) + count.ToString().PadLeft(3, '0');
                    draftID = voucherID.Substring(1, 8) + count.ToString().PadLeft(4, '0');
                }
                NeedDraft = true;
            }
            //
            bool result = false;
            string createDraft = @"INSERT INTO Draft VALUES(@DraftID,@Owner,GETDATE())";
            //
            string createDraftCommand = @"INSERT INTO ReqInDraft VALUES(@DraftID,@VoucherID,@Stamp)";
            //
            string createRequCommand = @"INSERT INTO Requisition(VoucherID,SampleName,LOT_NO,Purpose,SampleFrom,RequireDate,SendDate,GetDate) VALUES(@VoucherID,@SampleName,@LOT_NO,@Purpose,@SampleFrom,@RequireDate,@SendDate,@GetDate)";
            System.Data.Common.DbTransaction tran = acQC.BeginTransaction();
            //

            try
            {
                if (NeedDraft)
                    acQC.ExecuteNonQuery(createDraft, new object[] { draftID, usr });

                acQC.ExecuteNonQuery(createRequCommand, new object[] { voucherID, sampleName, LotNo, purpose, sampleFrom, requireDate, sendDate, getDate });
                acQC.ExecuteNonQuery(createDraftCommand, new object[] { draftID, voucherID, System.DateTime.Now });

                //
                int i = 1;
                foreach (string propertyName in profiles)
                {
                    string sqlProfile = @"INSERT INTO Profile(VoucherID,Item,SampleName,PropertyName,Result,State) VALUES(@VoucherID,@Item,@SampleName,@PropertyName,@Result,@State)";
                    acQC.ExecuteNonQuery(sqlProfile, new object[] { voucherID, i, sampleName, propertyName, DBNull.Value, "0" });
                    i++;
                }
                tran.Commit();
                result = true;
            }
            catch (Exception e)
            {
                tran.Rollback();
                log.Error(e);

                throw e;
            }
            return result;
        }
        public DataTable GetReqInDraft(string draftId)
        {
            try
            {
                DataTable dt = acQC.DbHelper.Select("SELECT [VoucherID] FROM [ReqInDraft] WHERE ([DraftID] = @DraftID)", new object[] { draftId }).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                log.Error(e);
                return null;


            }
        }
        /// <summary>
        /// Get Category List
        /// </summary>
        public DataTable GetCategory(string userID, string language)
        {
            Console.WriteLine("SampleService - GetCategory():" + DateTime.Now.ToString());
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_Category",
                            new string[] { "UserID", "Language" },
                            new object[] { userID, language }).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Get Samples list by Category 
        /// </summary>
        public DataTable GetSampleByCategory(string userID, string typeID)
        {
            Console.WriteLine("SampleService - GetSampleByCategory():" + DateTime.Now.ToString());
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_SamplesByCategory",
                            new string[] { "UserID", "TypeID" },
                            new object[] { userID, typeID }).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Get Samples list by SampleName 
        /// </summary>
        public DataTable GetSamplesBySampleName(string userID, string SampleName)
        {
            Console.WriteLine("SampleService - SamplesBySampleName():" + DateTime.Now.ToString());
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_SamplesBySampleName",
                            new string[] { "UserID", "SampleName" },
                            new object[] { userID, SampleName }).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Get Samples List ,0 Entrusted 委托 ,1In Process   例行 , 2 SPC  ,3 by receive
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Query"></param>
        /// <returns></returns>
        public DataTable GetSample(string userID, string query)
        {
            Console.WriteLine("SampleDAL - GetSample():" + DateTime.Now.ToString());
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_Samples",
               new string[] { "UserID", "Query" },
               new object[] { userID, query }).Tables[0];

                return dt;
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }

        public DataTable GetMaterial(string userID, string sampleName, string query)
        {
            Console.WriteLine("MaterialDAL - GetMaterial():" + DateTime.Now.ToString());
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_Materials",
                    new string[] { "UserID", "SampleName", "Query" },
                    new object[] { userID, sampleName, query }).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }
        public DataTable GetVendor(string Language, string Fromtime, string Totime)
        {
            Console.WriteLine("MaterialDAL - GetVendor():" + DateTime.Now.ToString());
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_Vendor",
                    new string[] { "Language", "Fromtime", "Totime" },
                    new object[] { Language, Fromtime, Totime }).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                log.Error(ex);

                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAttribute(string sampleName)
        {
            try
            {
                DataTable dt = acQC.DbHelper.Select("Select * From Attribute WHERE SampleName = @sampleName ORDER BY OrderBy", new object[] { sampleName }).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                log.Error(e);
                return null;
            }
        }
        public DataTable GetReqVoucher(string userid, DateTime sendB, DateTime sendE, string owner, string voucherState)
        {
            DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_GetReq4Delegate",
                  new string[] { "UserID", "SendB", "SendE", "Owner", "VoucherState" },
                  new object[] { userid, sendB, sendE, owner, voucherState }).Tables[0];
            return dt;
        }
        public DataTable QueryPlanAdd(string userID, DateTime begin, DateTime end, string sampleName, string lot_No)
        {
            Console.WriteLine("PlanAddDAL - CreatePlanAdd():" + DateTime.Now.ToString());
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("Q_GetPlanAdd",
                     new string[] { "UserID", "B", "E", "SampleName", "LOT_NO" },
                     new object[] { userID, begin, end, sampleName, lot_No }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }
        public bool CreatePlanAdd(DOCPlanAdd planadd)
        {
            Console.WriteLine("PlanAddDAL - CreatePlanAdd():" + DateTime.Now.ToString());
            try
            {
                return db.Insert(planadd);
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }
        public QCSample SelectSample(string sampleName)
        {
            return db.Select(new QCSample { SampleName = sampleName });
        }
        public DataTable GetProfileList(string voucherID)
        {
            Console.WriteLine("ProfileDAL - GetProfileList():" + DateTime.Now.ToString());
            try
            {
                return acQC.DbHelper.Select("SELECT [VoucherID] ,[SampleName], [PropertyName] from Profile WHERE VoucherID = @VoucherID",
                                                new object[] { voucherID }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Get Material
        /// </summary>
        /// <param name="sampleName">SampleName</param>
        /// <param name="lotNo">LOT_NO</param>
        /// <returns></returns>
        public QCMaterial GetMaterial(string sampleName, string lotNo)
        {
            try
            {
                return db.Select<QCMaterial>(new QCMaterial { SampleName = sampleName, LOT_NO = lotNo });
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }
        public bool DeletePlandAdd(string voucherID)
        {
            Console.WriteLine("DOCPlanAddDAL--DeletePlandAdd " + DateTime.Now);

            //核查是否能删除
            string cmd = @"Update PlanAdd SET state = 'X' WHERE VoucherID = @VoucherID and state='0'";
            try
            {
                acQC.ExecuteNonQuery(cmd, new object[] { voucherID });
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return true;


        }
        public bool DeleteRequistion(string voucherID)
        {
            Console.WriteLine("DOCPlanAddDAL--DeletePlandAdd " + DateTime.Now);

            //核查是否能删除
            //  string cmd = @"Update Requisition SET STATE = 'X' WHERE VoucherID = @VoucherID and state='0'";
            string cmd = @"UPDATE dbo.Requisition 
                            SET dbo.Requisition.[State] = 'X'
                            WHERE dbo.Requisition.VoucherID IN (SELECT rid.VoucherID FROM dbo.ReqInDraft rid WHERE rid.DraftID = @DraftID)
                            AND dbo.Requisition.[State] ='0'";
            try
            {
                acQC.ExecuteNonQuery(cmd, new object[] { voucherID });
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
        /// Get Entrusted detial
        /// </summary>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        public DataTable GetDelegateDetailsByVoucherID(string voucherId)
        {
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("Q_GetDelegateDetailsByVoucherID",
                  new string[] { "VoucherID" },
                  new object[] { voucherId }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Create by Isaac on 2018-09-19
        /// </summary>
        /// <param name="sampleName"></param>
        /// <param name="B"></param>
        /// <param name="E"></param>
        /// <param name="Lang"></param>
        /// <returns></returns>
        public DataTable ReportInComingAnalysis(string sampleName,string material, DateTime B, DateTime E, string Lang, string VoucherType)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                var myData = acQC.DbHelper.ExecuteStoredProcedure("RawMaterial_Q_Analysis",
                  new string[] { "SampleName","Material", "B", "E", "Lang", "VoucherType"},
                  new object[] { sampleName,material, B, E, Lang, VoucherType});
                if (myData.Tables.Count > 1)
                {
                    dt.Merge(myData.Tables[0]);
                    dt2 = myData.Tables[1];

                }
                else
                {
                    return dt;
                }
                
                //main to sort
                dt = AddRowToTable(dt, dt2);
                dt.DefaultView.Sort = "SampleName ASC, Description ASC, ProdDate ASC";
                dt = dt.DefaultView.ToTable();
                dt.AcceptChanges();             

                return dt;


            }
            catch (Exception ex)
            {

                log.Error(ex);
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Create by Isaac on 2018-12-14
        /// </summary>
        /// <param name="VoucherID"></param>
        /// <returns></returns>
        public DataTable GetReportByVoucherID(string VoucherID)
        {
            try
            {

                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("RawMaterial_Q_Analysis_PrintMaterial",
                  new string[] { "VoucherID" },
                  new object[] { VoucherID }).Tables[0];              

                return dt;

            }
            catch (Exception ex)
            {

                log.Error(ex);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Created By Jang
        /// Check SP voucher from ReceiveGeneral if it submited (Voucher=='SP' && Status!='N')
        /// </summary>
        /// <param name="VoucherID"></param>
        /// <returns></returns>
        public bool IsNew_EntrustedVoucher_General(string VoucherID)
        {
            try
            {
                DataTable status = acQC.DbHelper.ExecuteStoredProcedure("RawMaterial_General_GetVoucher", //in table ReceiveGeneral 
                  new string[] { "VoucherID" }, new object[] { VoucherID }).Tables[0];
                return status.Rows[0]["StatusofSubmit"].ToString() == "N" ? true : false;

            }
            catch (Exception ex)
            {

                log.Error(ex);
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateStatus_EntrustedVoucher_General(string voucherid, string status, string qualifedstring)
        {
            try
            {
                Console.WriteLine("UpdateStatus_EntrustedVoucher_General "+ voucherid +" with status "+ status+"(QualifedString: "+qualifedstring+") "+ DateTime.Now);
                try
                {
                    acQC.ExecuteStoredProcedure("RawMaterial_Update_ReceiveVoucher",
                       new string[] { "VoucherID", "StatusOfSubmit", "QualifedString" },
                       new object[] { voucherid, status, qualifedstring });
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    Console.WriteLine(ex.Message);
                    throw ex;
                }

                return true;

            }
            catch (Exception ex)
            {

                log.Error(ex);
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Create by Jang 09-10-2018
        /// FUNCTION TO ADD TABLE GROUPFOOTER (MIN, MAX,...) TO MAIN DATA TABLE dt
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="groupFooter"></param>
        /// <returns></returns>
        public DataTable AddRowToTable(DataTable dt, DataTable groupFooter)
        {
            /*INIT*/
            List<string> myListRows = new List<string>() { "ValueSpec", "MIN", "MAX", "AVG", "STD" };
            var _resultTable = dt.Clone();
            
            if (groupFooter != null && groupFooter.Rows.Count > 0) //GROUP FOOTER EXIST
            {
                foreach (DataRow dr in groupFooter.Rows) 
                {
                    string SampleName = dr["SampleName"].ToString();
                    string propertyname = dr["PropertyName"].ToString();
                    string description = dr["Description"].ToString();
                    foreach (var item in myListRows) //REPEAT TO ADD 5 ROWS THAT NAME IN myListRows
                    {
                        var _value = dr[item.ToString()];
                        var myRows = item.ToString() == "ValueSpec" ?  
                            /* 
                             *  '*' AND 'Z' are used for SORTING data 
                             *  '*' for the ValueSpec header showing
                             *  'Z' for the Groupby Data showing
                             */
                            _resultTable.Select("SampleName='" + SampleName + "' AND ProdDate='*" + item.ToString() + "' AND Description='" + description + "'").FirstOrDefault(): 
                            _resultTable.Select("SampleName='" + SampleName + "' AND ProdDate='Z" + item.ToString() + "' AND Description='" + description + "'").FirstOrDefault();
                        if (myRows == null) 
                        {
                            myRows = _resultTable.NewRow();
                            switch (item.ToString())
                            {
                                case "ValueSpec": myRows["ProdDate"] = "*" + item.ToString(); //VALUESPEC
                                    break;
                                default: myRows["ProdDate"] = "Z" + item.ToString(); //MIN, MAX, AVG, STD
                                    break;
                            }
                            myRows["SampleName"] = SampleName;
                            myRows["Description"] = description;
                            _resultTable.Rows.Add(myRows);
                        }
                        myRows[propertyname] = _value;
                    }
                }
            }
            _resultTable.Merge(dt);
            return _resultTable;
        }     
        /// <summary>
        /// GetData4ReportSPPnPOLY
        /// </summary>
        /// <param name="sampleName"></param>
        /// <param name="lot_No"></param>
        /// <param name="line"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetData4ReportSPPnPOLY(string sampleName, string lot_No, string line, DateTime from, DateTime to, bool type)
        {
            try
            {
               
                return acQC.DbHelper.ExecuteStoredProcedure("Report_POLY_SSP",
                  new string[] { "SampleName", "LOT_NO", "LINE", "DateForm", "DateTo", "Type" },
                  new object[] { sampleName, lot_No, line, from, to, type }).Tables[0];
            }
            catch (Exception ex)
            {

                log.Error(ex);
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get Department following by Entrusted vouchers that exist - created by Jang 2018-11-15
        /// </summary>
        /// <returns></returns>
        public DataTable GetEntrustedDepartment()
        {
            try
            {
                return acQC.DbHelper.Select(@"SELECT DISTINCT DepartmentID, Specification
	                                            FROM Draft d
	                                            JOIN HRMS.dbo.vEGate_Employees emp ON emp.EmployeeID = d.[Owner]",
                                                new object[] {}).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);

                throw new Exception(e.Message);
            }
        }
        public bool Save_StaplePickForGrade(StaplePickForGrade staple)
        {
            try
            {
                bool result= false;
                //check is it exists
                var _stapleData = db.Select<StaplePickForGrade>(new StaplePickForGrade { BarCode = staple.BarCode });

                if (_stapleData.MaterialNO !=null)
                {
                    if (_stapleData.PickDate != null)
                    {
                        staple.PickDate = _stapleData.PickDate;
                    }
                    result= db.Update(staple);
                }
                else
                {
                    result= db.Insert(staple);
                }
                return result;
            }
            catch (Exception e)
            {

                log.Error(e);

                throw new Exception(e.Message);
            }
        }
        public DataTable StapleFiber_ReportByBatch(string B, string E,string ReportType)
        {
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("StapleFiber_ReportByBatch",
                 new string[] { "B", "E", "ReportType" },
                 new object[] { B, E, ReportType }).Tables[0];

                if (ReportType=="Month")
                {
                    var newSort = from row in dt.AsEnumerable()
                                  where row.Field<int>("OrderNumber") == 0
                                  group row by new { Line = row.Field<string>("Line"), LOT_NO = row.Field<string>("LOT_NO"), main_Batch = row.Field<string>("main_Batch") } into grp
                                  select new
                                  {
                                      Line = grp.Key.Line,
                                      LOT_NO = grp.Key.LOT_NO,
                                      main_Batch = grp.Key.main_Batch,
                                      Count = grp.Count()
                                  };
                    var newSortOrderNumber = from row in dt.AsEnumerable()
                                             where row.Field<int>("OrderNumber") == 0
                                             // group row by new { Line = row.Field<string>("Line"), LOT_NO = row.Field<string>("LOT_NO"), main_Batch = row.Field<string>("main_Batch") } into grp
                                             orderby row.Field<string>("Line"), row.Field<string>("LOT_NO"), row.Field<string>("main_Batch")
                                             select row
                                 ;




                    foreach (var itemnewSort in newSort)
                    {
                        foreach (DataRow Rowitem in dt.Rows)
                        {
                            if (Rowitem["Line"].ToString() == itemnewSort.Line && Rowitem["LOT_NO"].ToString() == itemnewSort.LOT_NO && Rowitem["main_Batch"].ToString() == itemnewSort.main_Batch)
                            {
                                if (Rowitem["sub_Batch"].ToString().Contains("AVG"))
                                {
                                    Rowitem["sub_Batch"] = itemnewSort.Count;
                                }
                            }
                        }
                    }
                    foreach (DataRow Rowitem in dt.Rows)
                    {
                        int order = 4;
                        foreach (var item in newSortOrderNumber)
                        {

                            if (item["Line"].ToString() == Rowitem["Line"].ToString() && item["LOT_NO"].ToString() == Rowitem["LOT_NO"].ToString() && item["main_Batch"].ToString() == Rowitem["main_Batch"].ToString())
                            {
                                var aaaaaa = Rowitem["OrderNumber"];
                                order++;
                                Rowitem["OrderNumber"] = order;

                                var aaab = Rowitem["OrderNumber"];
                            }
                        }
                    }
                    dt = dt.AsEnumerable().OrderBy(x => x.Field<string>("Line")).ThenBy(x => x.Field<string>("LOT_NO")).ThenBy(x => x.Field<string>("main_Batch")).ThenBy(x => x.Field<int>("OrderNumber"))
   .Select(x => x)
   .CopyToDataTable();

                    
                    return dt;
                }
                else
                {
                    //REPORT BY YEAR
                    return dt;
                }
                
               

            }
            catch (Exception e )
            {

                log.Error(e);

                throw new Exception(e.Message);
            }
        }
        public DataTable GET_PrecValueBySampleName(string SampleName)
        {
            try
            {
                 DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("GET_PrecValueBySampleName",
                new string[] { "SampleName" },
                new object[] { SampleName }).Tables[0];
                return dt;
            }
            catch (Exception e)
            {

                log.Error(e);

                throw new Exception(e.Message);
            }
        }




    }
        
}
