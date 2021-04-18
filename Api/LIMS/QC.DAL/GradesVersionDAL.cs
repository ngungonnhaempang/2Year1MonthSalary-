using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS.Model;
using Shawoo.Common;
using Shawoo.Core;
using Shawoo.GenuineChannels;

namespace QC.DAL
{
  public  class GradeVersionDAL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");
        private DB db = new DB("LIMS");
        public string CheckGradeVersion(string sampleName, string lotNo)
        {

             
            DataTable dt = acQC.ExecuteStoredProcedure("i_CheckGrade_Version",
                                               new string[] { "UserID", "SampleName", "LOT_NO" },
                                               new object[] { "", sampleName, lotNo }).Tables[0];


            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["VersionID"].ToString();
            }
            throw new Exception("VersionID is Null");

        }
       
        private string ISCanModifyVersion(string sampleName, string lotNo, string grades)
        { 
        
         int countgrade = acQC.SelectScalar<int>(@"SELECT COUNT(ID) FROM GradeMaterial WHERE SampleName = @SampleName
                AND LOT_NO = @LOT_NO and Grades=@grades and  Status not in ('X','S')", new object[] { sampleName, lotNo, grades });
             if (countgrade > 0)
            {
                 return string.Format("Already Exist Grade For  {0}, you can only  modify version!", lotNo);
            }

             return string.Empty;
        }
        private string IsCanCreateNewGrade(string sampleName, string lotNo, string grades)
        {
            //
            if (string.IsNullOrEmpty(grades))
            {
                grades = "00";
            }
            int countgrade = acQC.SelectScalar<int>(@"SELECT COUNT(ID) FROM GradeMaterial WHERE SampleName = @SampleName
                AND LOT_NO = @LOT_NO and Grades=@grades and Status not in ('X','N')",
             new object[] { sampleName, lotNo, grades });

            if (countgrade > 0)
            {
                return "Grade is exists,please modify version!";
            }
            return string.Empty;

        }
    

        public string IsExsitGrade(string sampleName, string lotNo, string grades)
        {

            DataTable dt = acQC.SelectDataSet(@"SELECT TOP 1 ID FROM GradeMaterial WHERE SampleName = @SampleName
                AND LOT_NO = @LOT_NO and Grades=@grades and Status='N'",
                new object[] { sampleName, lotNo, grades }).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["ID"].ToString(); ;
            }

            return "";
        }
        public GradeMaterial GetGradeMaterial(string id)
        {

            return db.Select(new GradeMaterial { ID = id});
        }
       
        public QCGrades GetQCGrades(string id, string propertyName)
        {
            return db.Select(new QCGrades { ID = id, PropertyName = propertyName });
        
        }
      
        public string InsertGradeMaterial(GradeMaterial gradematerial)
        {
            try
            {
                if (!db.Insert(gradematerial))
                {
                    if (db.Update(gradematerial))
                    {
                        return "";
                    }
                    else
                    {
                        return "Can not Update: " + gradematerial.ID;
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                if(db.Update(gradematerial))
                {
                    return "";
                }
                return ex.Message;
            }

        }
       
        /// <summary>
        /// create GradeMateril 
        /// </summary>
        /// <param name="gradematerial"></param>
        /// <returns></returns>
        public string CreateGradeMaterial(GradeMaterial gradematerial)
        {
            //修改
            string msg = string.Empty;
           
                msg = IsCanCreateNewGrade(gradematerial.SampleName, gradematerial.LOT_NO, gradematerial.Grades);
                if (!string.IsNullOrEmpty(msg))
                {
                    return msg;
                }

            //gradematerial.UserID = GenuineUtility.CurrentSession["UID"].ToString();
           return  InsertGradeMaterial(gradematerial);
            
           
        }

        public string UpdateGradeMaterial(GradeMaterial gradematerial)
        {
           
            return InsertGradeMaterial(gradematerial);

        }
        /// <summary>
        /// create a new version
        /// </summary>
        /// <param name="sampleName"></param>
        /// <param name="lotno"></param>
        /// <param name="grades">Grades</param>
        /// <param name="qcGrades">propertyName</param>
        /// <returns></returns>
        public string CreateNewVersion(string id,int version, List<QCGrades> qcGrades)
        {
            bool isAllSave = true;
           
            foreach (QCGrades grade in qcGrades)
            {
                try
                {
                    if (!db.Insert(grade))
                    {
                        if (!db.Update(grade))
                        {
                            isAllSave = false;
                            break;
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    isAllSave= db.Update(grade);
                   
                    break;
                }
            }
            if (isAllSave == false)
            { 
                //delete save date;
                if (!DeleteAllGradeByDarf(id))
                {
                    return "DeleteAllGrade is failed in create Grade! ";
                }
                else {
                    return "Save is failed in create Grade! ";
                }
            }
            return "";
        
        }

        public bool UpdateInWorkFlow(string id, string status)
        {

            acQC.ExecuteNonQuery("UPDATE GradeMaterial SET Status = @Status where ID=@ID  and Status <>'S'",
                                     new object[] { status,id });

            return true;
        }

        public bool UpdateValidDate(string id, DateTime validDate)
        {
            acQC.ExecuteNonQuery("UPDATE Grades SET ValidDate = @ValidDate where ID in (SELECT ID from GradeMaterial where Status in('P','N') and ID=@ID)",
                                     new object[] { validDate, id });

            return true;
        }

        public string DeleteGradesItem(string id, string propertyName)
        {
             int countgrade = acQC.SelectScalar<int>(@"SELECT Count(ID)  FROM dbo.GradeMaterial WHERE  ID=@ID  and Status='N'",
                        new object[] { new Guid(id) });

             if (countgrade > 0)
             {

                 acQC.ExecuteNonQuery(@"Delete FROM Grades WHERE  ID=@ID  and [PropertyName]=@PropertyName",
                       new object[] { new Guid(id), propertyName });
                 return "";

             }else{
                 return "this status can not delete";
             }
        
        }
        public string DeleteGradeStatus(string id,int version)
        {
           
                string msg = string.Empty;
               DataTable dt= acQC.DbHelper.Select(
                      "SELECT *  FROM dbo.GradeMaterial WHERE  ID=@ID and  Version=@version",
                      new object[] { id,version }).Tables[0];
               if (dt.Rows.Count > 0)
               {
                   string sampleName = dt.Rows[0]["SampleName"].ToString();
                   string lotNo = dt.Rows[0]["LOT_NO"].ToString();
                   string grades = dt.Rows[0]["Grades"].ToString();
                   // int version  =int.Parse( dt.Rows[0]["Version"].ToString());
                   msg = ISCanDeleteGrade(sampleName, lotNo, grades, version, id);
                   if (!string.IsNullOrEmpty(msg))
                   {
                       return msg;
                   }

                   acQC.ExecuteNonQuery("UPDATE GradeMaterial SET Status = 'X' where ID=@ID and Version=@Version",
                                      new object[] { id, version });
                   if (1 != acQC.SelectScalar<int>("SELECT Count(ID) FROM GradeMaterial WHERE ID = @ID AND Version=@Version AND Status = 'X' ",
                                                      new object[] { id, version }))
                   {

                       msg = "DeleteGradeStatus is failed";
                   }
                   return msg;

               }
               else {

                   return "Don't find this row Data";
               }
            
           
        }


      
        public bool DeleteAllGradeByDarf(string id)
        {

            bool reslut = false;
         
            try
            {
                if (acQC.SelectScalar<int>("SELECT Count(ID) FROM GradeMaterial WHERE ID = @ID and Status='N'",
                                                         new object[] { id }) > 0) 
                {
                    acQC.ExecuteNonQuery("Delete from  Grades  where ID=@ID", new object[] { id });

                    acQC.ExecuteNonQuery("Delete from  GradeMaterial  where ID=@ID   and Status='N'", new object[] { id });


                    return 0 == acQC.SelectScalar<int>("SELECT Count(ID) FROM GradeMaterial WHERE ID = @ID",
                                                             new object[] { id });
                }
              
            }
            catch (Exception ex)
            {
               
                log.Error(ex);
              
                
            }
            return reslut;
        }
        public string ISCanDeleteGrade(string sampleName, string lotNo,string grades,int version,string id)
        {
            //if this version is draft "N",delete it
            //判断这个版本是否已经有创建的单据。如果需要delete 单据,
            //if this version is S, voucher by version ,frist delete voucher or voucher update version , then delete it set "X"

            var sumString =
                   acQC.DbHelper.ExecuteStoredProcedure("GD_CheckDeleteGrade",
                       new string[] { "SampleName", "LotNO", "Grades", "Version","ID" },
                       new object[] { sampleName, lotNo, grades, version,id }).Tables[0].Rows[0][0].ToString();
          
            if (!string.IsNullOrEmpty(sumString))
            {
                return sumString;
            }

            return "";
        
        }

        public string PublishVersion(string id)
        {

          return
                      acQC.DbHelper.ExecuteStoredProcedure("GD_PulishVersion",
                          new string[] { "ID" },
                          new object[] { id}).Tables[0].Rows[0][0].ToString();

          
        }
        /// <summary>
        /// COPY a Grade or Version
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <param name="newSampleName"></param>
        /// <param name="newLotNo"></param>
        /// <param name="newGrades"></param>
        /// <returns></returns>
        public string CopyGradeVersion(string oldSamplename, string oldlot,string oldgrades,string id, string sampleName, string lotNO, string grades, string userid)
        {
            //1.judgment current version status for S         
             string  sumString=  acQC.DbHelper.ExecuteStoredProcedure("GD_CopyGrade",
                     new string[] { "OldSampleName", "OldLOT_NO","OldGrades", "SampleName","NewID", "LOT_NO", "Grades", "UserId" },
                     new object[] { oldSamplename, oldlot, oldgrades, id,sampleName, lotNO, grades, userid }).Tables[0].Rows[0][0].ToString();         
                return sumString; 
          
        }
        /// <summary>
        /// modify version
        /// </summary>
        /// <param name="SampleName"></param>
        /// <param name="lotNo"></param>
        /// <param name="version"></param>
        /// <param name="grades"></param>
        /// <returns></returns>
        public DataTable IsCanUpdateVersion(string sampleName,string lotNo,string grades)
        {

            //1.judgment current version for S

            // create new version +1 , 
            //save
            //select @MaxVersion=ISNULL(max([Version]),0) from GradeMaterial where SampleName=@SampleName and LotNO=@LotNO and Grades=@Grades and Status='S'   version  ID

            //get list
          return  acQC.DbHelper.ExecuteStoredProcedure("GD_CheckModeifyVersion",
                       new string[] {  "SampleName", "LOT_NO", "Grades" },
                       new object[] { sampleName, lotNo, grades }).Tables[0];
            

        
        }
        /// <summary>
        /// special modify version
        /// </summary>
        /// <param name="SampleName"></param>
        /// <param name="lotNo"></param>
        /// <param name="version"></param>
        /// <param name="grades"></param>
        /// <returns></returns>
        public DataTable IsCanSpecial_UpdateVersion(string sampleName, string lotNo, string grades)
        {

            //1.judgment current version for S

            // create new version +1 , 
            //save
            //select @MaxVersion=ISNULL(max([Version]),0) from GradeMaterial where SampleName=@SampleName and LotNO=@LotNO and Grades=@Grades and Status='S'   version  ID

            //get list
            return acQC.DbHelper.ExecuteStoredProcedure("GD_CheckSpecial_ModeifyVersion",
                         new string[] { "SampleName", "LOT_NO", "Grades" },
                         new object[] { sampleName, lotNo, grades }).Tables[0];



        }

        /// <summary>
        /// Create by Isaac for Update Grade Status 2018-11-07
        /// </summary>
        /// <param name="id"></param>
        /// <param name="propertyName"></param>
        /// <param name="enable"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public DataTable isUpdateGradeStatus(string id,string propertyName, bool enable,string remark )
        {
            var data = acQC.DbHelper.ExecuteStoredProcedure("GD_CreateStatusUpdate",
                        new string[] { "ID", "PropertyName", "Enable", "Remark" },
                        new object[] { id, propertyName,enable,remark });           
            return data.Tables[0];
        }
        /// <summary>
        /// Set Only Update for Lab's User
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataTable isCheckExistDepart(string UserID)
        {
            var data = acQC.DbHelper.ExecuteStoredProcedure("GD_SetDepartmentUpdateGrades",
                       new string[] { "UserID"},
                       new object[] { UserID });
            return data.Tables[0];
        }
        /// <summary>
        /// 有效的，正在运行中的
        /// </summary>
        /// <param name="sampleName"></param>
        /// <param name="lotNo"></param>
        /// <param name="grades"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataTable GetQCUser(string sampleName, string ctype, string userId)
        {

            return acQC.DbHelper.ExecuteStoredProcedure("GD_GetQCUser",
                       new string[] { "SampleName", "Ctype", "UserID" },
                       new object[] { sampleName, ctype, userId }).Tables[0];
        }
        public DataTable GetGrade(string sampleName)
        {
            return acQC.DbHelper.ExecuteStoredProcedure("GD_GetGrade",
                         new string[] { "SampleName", "UserID" },
                         new object[] { sampleName,"" }).Tables[0];
        }
        /// <summary>
        /// 有效的，正在运行中的
        /// </summary>
        /// <param name="sampleName"></param>
        /// <param name="lotNo"></param>
        /// <param name="grades"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataTable GetStatusGrade(string sampleName, string lotNo, string grades,string status)
        {

            return acQC.DbHelper.ExecuteStoredProcedure("GD_QueryValidGrade",
                       new string[] {  "SampleName", "LOT_NO", "Grades" ,"Status"},
                       new object[] { sampleName, lotNo, grades, status }).Tables[0];
        }

      /// <summary>
      /// Jang #3614.
      /// this function is upgraded for GetPropertyValue. Updates information of VersionOld. But it's store procedure so it must be faster.
      /// </summary>
      /// <param name="samplename"></param>
      /// <param name="language"></param>
      /// <returns></returns>
        public DataTable GetCurrentPropertyValue(string samplename, string language)
        {

            return acQC.DbHelper.ExecuteStoredProcedure("GD_CurrentPropertyValue",
                       new string[] { "SampleName","Lang" },
                       new object[] { samplename, language }).Tables[0];
        }

      /// <summary>
      /// 根据样品获取所有物料的属性范围
      /// </summary>
      /// <param name="sampleName"></param>
      /// <returns></returns>
        public DataTable GetPropertyValue(string sampleName, string language)
        {
            Console.WriteLine("GetPropertyValue--" + sampleName + " " + DateTime.Now.ToString());
            DataTable dtProperty = new DataTable();//属性表
            string sqlProperty = "SELECT * FROM Attribute WHERE SampleName=@SampleName ORDER BY OrderBy";
            dtProperty = acQC.DbHelper.Select(sqlProperty, new object[] { sampleName }).Tables[0];

            DataTable dtMaterial = new DataTable();//物料表
            string sqlMaterial = @"SELECT LOT_NO,
	                                CASE @Language WHEN 'EN' THEN Description_EN
					                                WHEN 'TW' THEN Description_TW
					                                WHEN 'CN' THEN Description_CN
					                                WHEN 'VN' THEN Description_VN
	                                ELSE Description_EN END AS [Description]
                                  FROM Material  WHERE SampleName=@SampleName";
            dtMaterial = acQC.DbHelper.Select(sqlMaterial, new object[] { language, sampleName }).Tables[0];

            DataTable dt = dtMaterial.Copy();//复制物料，规格表
            dt.Columns.Add("Version", typeof(string));//添加“版别”列
            dt.Columns.Add("Grade", typeof(string));//添加“等级”列
            foreach (DataRow row in dtProperty.Rows)
            {
                dt.Columns.Add(row["PropertyName"].ToString(), typeof(string));//添加“属性”列
            }
            foreach (DataRow row in dt.Rows)
            {
                DataTable dtData = new DataTable();//数据表
                string sqldata = @"SELECT * FROM GradeMaterial AS gm 
                                    LEFT JOIN Grades AS g ON g.ID = gm.ID 
                                    WHERE gm.SampleName=@SampleName
	                                    AND [Status]='S'  
	                                    AND g.ValidDate<=GETDATE() 
	                                    AND (g.ValidTODate IS NULL OR GETDATE()<g.ValidTODate AND g.ValidTODate IS NOT NULL)";
                dtData = acQC.DbHelper.Select(sqldata, new object[] { sampleName }).Tables[0];
                foreach (DataRow rowData in dtData.Rows)
                {
                    if (row["LOT_NO"].ToString() == rowData["LOT_NO"].ToString())
                    {
                        string propertyName = rowData["PropertyName"].ToString();
                        row["Version"] = rowData["Version"].ToString();
                        //获取等级和属性范围
                        row["Grade"] = rowData["Grade"].ToString();
                        row[propertyName] = rowData["ValueSpec"].ToString();
                    }
                }  
            }
            return dt;
        }
        public DataTable GetAttributGrade(string sampleName,string lotNo,string grades)
        {
            string sql = @"select distinct PropertyName  from Grades g inner join GradeMaterial gm on g.id=gm.ID
where Status<>'X' and gm.SampleName=@SampleName and gm.LOT_NO=@LotNO and gm.Grades=@Grades";

            var dt =
             acQC.DbHelper.Select(sql,
                 new object[] { sampleName ,lotNo,grades}).Tables[0];

            return dt;
        }
      
        public DataTable GetGradesByQuery(string id)
        {

            var dt =
               acQC.DbHelper.Select(
                   "SELECT * FROM Grades WHERE  ID=@ID",
                   new object[] { id }).Tables[0];

            return dt;


        }
        public DataTable GetGradesByQuery(string sampleName, string lotNo, string grades)
        {


            return acQC.DbHelper.ExecuteStoredProcedure("GD_HistoryofGradeVersion",
                    new string[] { "SampleName", "LOT_NO", "Grades" },
                    new object[] { sampleName, lotNo, grades }).Tables[0];
         
        }
       
        public string UpdateVersion( DateTime  validdate,string id,int version)
        {
            //this version query, while don't have  other status
            //save

            acQC.ExecuteNonQuery("UPDATE Grades SET ValidTODate = @ValidTODate where ID=@ID and Version=@Version and Status='S'",
                                 new object[] { validdate,id  ,version});


            //grade.Version = acQC.SelectScalar<int>("SELECT MAX([Version])+1 FROM GradeVer WHERE ID = @ID", new object[] { grade.ID });
            //string msg = "";
            //return AddGrade(grade, out msg);

            return null;
        
        }

        public DataTable NewestGrades(string id)
        {
            
            return
             acQC.DbHelper.Select(
                 "SELECT  * FROM Grades WHERE ID=@id",
                 new object[] { id }).Tables[0];
        
        }

        public DataTable HistoryOfGradeVersion(string sampleName, string lotNO, string grades)
        {

            return
               acQC.DbHelper.ExecuteStoredProcedure("GD_HistoryforGrade"
               ,new string[]{"SampleName","LOT_NO","Grades"}
               , new object[] { sampleName, lotNO, grades }).Tables[0];
        }
        public DataTable HistoryOfGradeVersion_Report(string sampleName, string lotNO, string ValidDate)
        {

            return
               acQC.DbHelper.ExecuteStoredProcedure("GD_HistoryforGrade_Report"
               , new string[] { "SampleName", "LOT_NO", "ValidDate" }
               , new object[] { sampleName, lotNO, ValidDate }).Tables[0];
        }
        public DataTable GradeMaterial_ProcessAndPublish(string sampleName, string lotNO, string grades)
        {

            return
               acQC.DbHelper.Select(
                   "SELECT  * FROM GradeMaterial WHERE  SampleName=@sampleName and LOT_NO=@LotNO and Grades=@Grades and  Status<>'X' order by Version",
                   new object[] { sampleName, lotNO, grades }).Tables[0];
        }
        public DataTable NewestGradeMaterial(string sampleName, string lotNO, string grades)
        {

            return
               acQC.DbHelper.Select(
                   "SELECT top 1 * FROM GradeMaterial WHERE  SampleName=@sampleName and LOT_NO=@LotNO and Grades=@Grades and  Status='S' order by Version desc",
                   new object[] { sampleName, lotNO, grades }).Tables[0];

            
           
            
        }

        /// <summary>
        /// 是否可以修改发票
        /// </summary>
        /// <param name="fliex"></param>
        /// <returns></returns>
        public string IsCanUpdateByIEMS(string fliex)
        {
            return acQC.DbHelper.ExecuteStoredProcedure("Report_IEMS_InvoiceNO",
                     new string[] { "LIFEX" },
                     new object[] { fliex }).Tables[0].Rows[0]["MSG"].ToString();

        }
        public string UpdateIEMSExport(IEMS_Export export)
        {
            export.Stamp = System.DateTime.Now;
            if (!db.Save(export))
            {
                return "save failed,please try it";
            }
            else
            {
                return "";
            }
        }
        
    }
}
