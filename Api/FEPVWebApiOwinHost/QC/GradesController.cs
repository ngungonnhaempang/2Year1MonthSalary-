using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
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
using QC.DAL;
using QC.Model;
using LIMS.Model;
using RestSharp;
using System.Collections.Generic;
using System;

namespace FEPVWebApiOwinHost.QC
{

    [FilterIP]
    [RoutePrefix("api/LIMS/Grades")]
  public  class GradesController : ApiController
    {
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        GradeVersionDAL biz = new GradeVersionDAL();
        HelperBiz helpbiz = new HelperBiz();

        [Route("GetGrade")]
        [HttpGet]
        public IHttpActionResult GetGrade(string sampleName)
        {
           
           DataTable dt= biz.GetGrade(sampleName);
            return Ok(dt);

        }
        /// <summary>
        /// Create by Isaac by 2018-11-03
        /// </summary>
        /// <param name="id"></param>
        /// <param name="propertyName"></param>
        /// <param name="enable"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [Route("UpdateCurrentGradeStatus")]
        [HttpPost]
       
        public IHttpActionResult GradeUpdateStatus(GradeMaterialDto entity)
        {
            //var dt = biz.isUpdateGradeStatus(entity.ID);   
            int result = 0;
            foreach(var item in entity.GradesDto)
            {
                var dt = biz.isUpdateGradeStatus(entity.ID, item.PropertyName, item.Enable, item.Remark);
                result = int.Parse(dt.Rows[0][0].ToString());
            }
            return Ok(result);         

        }
        [Route("CanUpdateGrades")]
        [HttpGet]
        public IHttpActionResult canUpdateGrades(string UserID)
        {
           
            var dt = biz.isCheckExistDepart(UserID);
            return Ok(dt);

        }

        [Route("QCUser")]
        [HttpGet]
        public IHttpActionResult GetQCUser(string sampleName, string userId)
        { 
             JsonObject json = new JsonObject();
             List<string> strs = new List<string>();
             DataTable dt = biz.GetQCUser(sampleName, "C",userId);
             json["QCReceive"] = dt.Rows[0][0].ToString();// biz.GetQCUser(sampleName, "C").Rows[0][0].ToString();
             foreach (DataRow row in dt.Rows)
             {
                 strs.Add(row[0].ToString());
             }
             json["QCReceive"] = strs;
             return Ok(json);
       
        }
        [Route("QCLeader")]
        [HttpGet]
        public IHttpActionResult GetQCLeader(string sampleName, string userId)
        {
            JsonObject json = new JsonObject();
          
            List<string> strs=new List<string>();
            DataTable dt = biz.GetQCUser(sampleName, "Q", userId);
            foreach (DataRow row in dt.Rows)
            {
                strs.Add(row[0].ToString());
            }
            json["QCLeader"] = strs;
            return Ok(json);

        }
        [Route("NewestGrade")]
        [HttpGet]
        public IHttpActionResult NewestGrade(string sampleName, string lotNo, string grades,string status)
        {
            try
            {
                JsonObject json = new JsonObject();
                DataTable dtmaterial = biz.GetStatusGrade(sampleName, lotNo, grades,status);
                return Ok(dtmaterial);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace);

            }
        }



        [Route("GetCurrentPropertyValue")]
        [HttpGet]
        public IHttpActionResult GetCurrentPropertyValue(string sampleName, string language)
        {
            try
            {
                DataTable dt = biz.GetCurrentPropertyValue(sampleName, language);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [Route("GetPropertyValue")]
        [HttpGet]
        public IHttpActionResult GetPropertyValue(string sampleName, string language)
        {
            try
            {
                DataTable dt = biz.GetPropertyValue(sampleName, language);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
        [Route("GradesProcessing")]
        [HttpGet]
        public IHttpActionResult GradesProcessing(string id)
        {
            try
            {
                JsonObject json = new JsonObject();
                GradeMaterial gra = biz.GetGradeMaterial(id);
                GradeMaterialDto grade = new GradeMaterialDto();
                if (!string.IsNullOrEmpty(gra.SampleName) && !string.IsNullOrEmpty(gra.LOT_NO))
                {
                    SampleService sampleDal = new SampleService();
                    QCSample sample = sampleDal.SelectSample(gra.SampleName);
                 
                    QCMaterial material = sampleDal.GetMaterial(gra.SampleName, gra.LOT_NO);
                    json["SampleName"] = sample.SampleName;
                    json["SampleDescription_EN"] =sample.Description_EN;
                    json["SampleDescription_CN"] = sample.Description_CN;
                    json["SampleDescription_TW"] = sample.Description_TW;
                    json["SampleDescription_VN"] = sample.Description_VN;
                    json["Status"] = gra.Status;
                    json["Grade"] = gra.Grades;
                    json["LOT_NO"] = material.LOT_NO;
                    json["MaterialDescription_EN"] = material.Description_EN;
                    json["MaterialDescription_CN"] = material.Description_CN;
                    json["MaterialDescription_TW"] = material.Description_TW;
                    json["MaterialDescription_VN"] = material.Description_VN;
                    json["Version"] =gra.Version;
                    json["VersionSpc"] = gra.VersionSpc;
                    json["UserID"] = gra.UserID;
                    //json["Remark"] = gra.Remark;
                    DataTable dtGrade = biz.NewestGrades(id);
                    json["Grades"] = helpbiz.ConvertJson(dtGrade);
                    return Ok(json);

                }
                else {
                    return BadRequest("NO DATA");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace);

            }

        }
        private DataTable CreateHistoryGradeTable(string sampleName, string lotNo, string grades)
        {
          
           DataTable table = new DataTable();
            table.Columns.Add("Version", typeof(string));
            table.Columns.Add("Grade", typeof(string));
            table.Columns.Add("ValidDate", typeof(DateTime));
            var dtattu = biz.GetAttributGrade(sampleName, lotNo, grades);
            if (dtattu.Rows.Count > 0)
            {
                foreach (DataRow atrow in dtattu.Rows)
                {
                    table.Columns.Add(atrow[0].ToString(), typeof(string));
                }

            }
            return table;
        }
        /// <summary>
        /// Jang #3614
        /// 2019-01-23
        /// History Version With store procedure
        /// </summary>
        /// <param name="sampleName"></param>
        /// <param name="lotNo"></param>
        /// <param name="grades"> From table GradeMaterial (not from table Grades)</param>
        /// <returns></returns>
        [Route("HistoryOfGradeVersion")]
        [HttpGet]
        public IHttpActionResult HistoryOfGradeVersion(string sampleName, string lotNo, string grades)
        {

            try
            {
                return Ok(biz.HistoryOfGradeVersion(sampleName, lotNo, grades));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace);

            }

        }
        [Route("GD_HistoryforGrade_Report")]
        [HttpGet]
        public IHttpActionResult GD_HistoryforGrade_Report(string sampleName, string lotNo, string ValidDate)
        {

            try
            {
                return Ok(biz.HistoryOfGradeVersion_Report(sampleName, lotNo, ValidDate));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace);

            }

        }

        //get history version
        [Route("HistoryGrade")]
        [HttpGet]
        public IHttpActionResult GethistoryGrades(string sampleName, string lotNo, string grades)
        {

            try
            {
                /* init Json return value */
                JsonObject jsonall = new JsonObject();  
                
                //DataTable table = CreateHistoryGradeTable(sampleName, lotNo, grades);
                //DataRow row;

                List<JsonObject> list = new List<JsonObject>(); 
              
                List<string> pros = new List<string>();
                pros.Add("ValidDate"); 
                pros.Add("Grade");
                pros.Add("Status");
                pros.Add("Version");

                /* find GradeMaterial follow by  sampleName, lotNo, grades  */
                DataTable dt = biz.GradeMaterial_ProcessAndPublish(sampleName, lotNo, grades); 
                if (dt.Rows.Count > 0)
                { 
                    for( int i=0;i<dt.Rows.Count;i++)
                    {
                        JsonObject json = new JsonObject();
               
                        string id = dt.Rows[i]["ID"].ToString();
                        int version =int.Parse( dt.Rows[i]["Version"].ToString());
                        string versionSpc = dt.Rows[i]["VersionSpc"].ToString();
                        json["Version"] = versionSpc;
                        json["Versions"] = version;
                        json["Status"] = dt.Rows[i]["Status"].ToString();
                      
                        //json["SampleName"] = dt.Rows[i]["SampleName"].ToString();
                        //json["LOT_NO"] = dt.Rows[i]["LOT_NO"].ToString();
                        //json["Grades"] = dt.Rows[i]["Grades"].ToString();
                  //   table.Rows.Add(row);
                         var dtGrads=  biz.GetGradesByQuery(id);
                          if(dtGrads.Rows.Count>0)
                          {
                             DateTime validDate=Convert.ToDateTime(dtGrads.Rows[0]["ValidDate"]);
                            json["Grade"]  =dtGrads.Rows[0]["Grade"].ToString();
                            json["ValidDate"] =Convert.ToDateTime(validDate).ToString("yyyy-MM-dd HH:mm:ss");
                            foreach (DataRow prow in dtGrads.Rows)
                            {
                                string propertyName = prow["PropertyName"].ToString();
                                if(!pros.Contains(propertyName))
                                {
                                    pros.Add(propertyName);
                                }
                                var de=   biz.GetQCGrades(id, propertyName);
                                JsonObject jsonitem = new JsonObject();
                                        
                                    jsonitem["ValueSpec"] = de.ValueSpec;
                                    jsonitem["MaxValue"] =de.MaxValue!=null ? Math.Round(Convert.ToDecimal(de.MaxValue),de.Prec).ToString():"";
                                    jsonitem["MinValue"] = de.MinValue!=null ? Math.Round(Convert.ToDecimal(de.MinValue),de.Prec).ToString():"";
                                    jsonitem["Enable"] = de.Enable;
                                    jsonitem["Prec"] = de.Prec;

                                json[propertyName] = jsonitem;
                            } 

                          }

                          list.Add(json);
                     
                    }

                    jsonall["Item"] = list;
                  
                  
                    jsonall["Header"] = pros;
                   
                    
                
                }
                return Ok(jsonall);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace); 
            
            }
        
        }

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult CreateGrade(GradeMaterialDto material)
        {
            try
            {
                JsonObject json = new JsonObject();
                GradeMaterial gm = new GradeMaterial();
                gm.SampleName = material.SampleName;
                gm.LOT_NO = material.LOT_NO;
                gm.Grades = material.Grades == null ? "00" : material.Grades;
                Guid id = Guid.NewGuid();
                gm.ID = id.ToString();
                gm.Version = 1;
                gm.Status = "N";
                gm.UserID = material.UserID;
                gm.VersionSpc = "V" + gm.Version.ToString().PadLeft(2, '0');
                gm.Stamp = System.DateTime.Now;
               // gm.Remark = material.Remark;
                string _id = biz.IsExsitGrade(material.SampleName, material.LOT_NO, material.Grades);
                if (!string.IsNullOrEmpty(_id))
                {
                   bool isDelete= biz.DeleteAllGradeByDarf(_id);
                   if (!isDelete)
                   {
                       json["Error"] = "DELETE Failed";
                       return Ok(json);
                   }
                }
                string msg = biz.CreateGradeMaterial(gm);
                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                    return Ok(json);
                }
                gm = biz.GetGradeMaterial(gm.ID.ToString());
                List<QCGrades> grades = new List<QCGrades>();
                foreach (var g in material.GradesDto)
                {
                    QCGrades grade = new QCGrades();
                    grade.ID = gm.ID;
                  
                    grade.PropertyName = g.PropertyName;
                    grade.Grade = material.Grade;
                    grade.Enable = g.Enable;
                    grade.MaxValue = g.MaxValue;
                    grade.MinValue = g.MinValue;
                    grade.Prec = g.Prec;
                    grade.ValueSpec = g.ValueSpec;
                    grade.ValidTODate = null;
                    grade.CreateDate = System.DateTime.Now; ;
                    grade.ValidDate = material.ValidDate;
                    grade.Stamp = System.DateTime.Now;
                    grade.UserID = material.UserID;
                    grades.Add(grade);
                }

                msg = biz.CreateNewVersion(gm.ID.ToString(), gm.Version, grades);
                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                    return Ok(json);
                  
                }
                json["ID"] = gm.ID;
                json["Version"] = gm.Version;
                json["SampleName"] = material.SampleName;
                json["LOT_NO"] = material.LOT_NO;
                json["Grades"] = material.Grades;
                json["VersionSpc"] = gm.VersionSpc;
               
                return Ok(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace); 
            }

        }
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult UpdateGrade(GradeMaterialDto material)
        {
            try
            {
                JsonObject json = new JsonObject();
                GradeMaterial gm = new GradeMaterial();
                string msg = String.Empty;
                gm.SampleName = material.SampleName;
                gm.LOT_NO = material.LOT_NO;
                gm.Grades = material.Grades == null ? "00" : material.Grades;
              
                gm.ID = material.ID;
                gm.Version = material.Version;
                gm.Status = "N";
                gm.UserID = material.UserID;
                gm.VersionSpc = "V" + gm.Version.ToString().PadLeft(2, '0');
                gm.Stamp = System.DateTime.Now;
                //gm.Remark = material.Remark;
                string _id = biz.IsExsitGrade(material.SampleName, material.LOT_NO, material.Grades);
                if (!string.IsNullOrEmpty(_id))
                {
                    bool isDelete = biz.DeleteAllGradeByDarf(_id);
                    if (!isDelete)
                    {
                        json["Error"] = "DELETE Failed";
                        return Ok(json);
                    }
                }
                msg = biz.UpdateGradeMaterial(gm);
                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                    return Ok(json);
                }
                gm = biz.GetGradeMaterial(gm.ID.ToString());
                List<QCGrades> grades = new List<QCGrades>();
                foreach (var g in material.GradesDto)
                {
                    
                    QCGrades grade = new QCGrades();
                    grade.ID = gm.ID;

                    grade.PropertyName = g.PropertyName;
                    grade.Grade = material.Grade;
                    grade.Enable = g.Enable;
                    grade.MaxValue = g.MaxValue;
                    grade.MinValue = g.MinValue;
                    grade.Prec = g.Prec;
                    grade.ValueSpec = g.ValueSpec;
                    grade.ValidTODate = null;
                    grade.CreateDate = System.DateTime.Now; ;
                    grade.ValidDate = material.ValidDate;
                    grade.Stamp = System.DateTime.Now;
                    grade.UserID = material.UserID;
                    grades.Add(grade);
                }

                msg = biz.CreateNewVersion(gm.ID.ToString(), gm.Version, grades);
                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                    return Ok(json);

                }
                json["ID"] = gm.ID;
                json["Version"] = gm.Version;
                json["SampleName"] = material.SampleName;
                json["LOT_NO"] = material.LOT_NO;
                json["Grades"] = material.Grades;
                json["VersionSpc"] = gm.VersionSpc;

                return Ok(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace);
            }

        }

        [Route("ConfirmData")]
        [HttpPost]
        public IHttpActionResult ConfirmData(string id, DateTime validDate)
        {

            try
            {
                JsonObject json = new JsonObject();
                biz.UpdateValidDate(id, validDate);
                json["Success"] = validDate;
                return Ok(json);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
        [Route("DeleteItemVersion")]
        [HttpPost]
        public IHttpActionResult DeleteGradesItem(string id, string propertyName)
        { 
        
            try
            {
                JsonObject json = new JsonObject();
                string msg = biz.DeleteGradesItem(id, propertyName);
                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                }
                return Ok(json); 
                
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
        /// <summary>
        /// draf
        /// </summary>
        /// <param name="material"></param>
        /// <param name="qcGrades"></param>
        /// <returns></returns>
        [Route("DeleteVersion")]
        [HttpPost]
        public IHttpActionResult DeleteVersion(string id, int version)
        {
            try
            {
                JsonObject json = new JsonObject();
                string msg = biz.DeleteGradeStatus(id, version);
                if (!string.IsNullOrEmpty(msg))
                {
                    return BadRequest(msg);
                }
                else
                {
                    json["Success"] = "Success delete Version" + version.ToString();
                    return Ok(json);

                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace); 
            }
           
        }

        [Route("GetCreateVersion")]
        [HttpGet]
        public IHttpActionResult GetNewestVersion(string sampleName, string lotNo, string grades)
        {
            string msg = string.Empty;
            try
            {
                DataTable dt = biz.IsCanUpdateVersion(sampleName, lotNo, grades);
                if (dt.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["MSG"].ToString();
                    if (!string.IsNullOrEmpty(msg))
                    {
                       return BadRequest(msg);

                    }else{
                         string id = dt.Rows[0]["ID"].ToString();
                         return Ok(biz.GetGradesByQuery(id));
                    }
                   
                }
                else
                {


                    return BadRequest(msg);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                Loger.Error(ex);
                return BadRequest(ex.Message + ex.StackTrace);
            }


        }
       
      
        [Route("SaveModifyVersion")]
        [HttpPost]
        public IHttpActionResult SaveModifyVersion(GradeMaterialDto material)
        {
            JsonObject json = new JsonObject();
            string msg = string.Empty;
            DataTable dt = biz.IsCanUpdateVersion(material.SampleName, material.LOT_NO, material.Grades);
            if (dt.Rows.Count > 0)
            {
                // can revise
                msg = dt.Rows[0]["MSG"].ToString();
                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                    return Ok(json);
                }
               int version=int.Parse( dt.Rows[0]["Version"].ToString());
                DataTable dtNewest= biz.NewestGrades(dt.Rows[0]["ID"].ToString());
                if (dtNewest.Rows.Count < 0)
                {
                    json["Error"] = "NO property Name";
                    return Ok(json);
                }
                DateTime lastversionDate =Convert.ToDateTime( dtNewest.Rows[0]["ValidDate"]);
                if (lastversionDate > material.ValidDate)
                {
                    return BadRequest("Valid Date is Error ,it is min  Last Version");
                
                }

                GradeMaterial gm = new GradeMaterial();
                gm.ID = Guid.NewGuid().ToString(); ;

                gm.SampleName = material.SampleName;
                gm.LOT_NO = material.LOT_NO;
                gm.Version = version+1;
                gm.Grades = material.Grades == null ? "00" : material.Grades;
                gm.UserID=material.UserID;
                gm.Stamp=System.DateTime.Now;
                gm.Status = "N";
                gm.VersionSpc = "V" + gm.Version.ToString().PadLeft(2, '0');
               // gm.Remark = material.Remark;
                 msg = biz.InsertGradeMaterial(gm);
                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                    return Ok(json);
                }
             
                List<QCGrades> grades = new List<QCGrades>();
                foreach (var g in material.GradesDto)
                {
                    QCGrades grade = new QCGrades();
                    grade.ID = gm.ID;
                    grade.PropertyName = g.PropertyName;
                    grade.Grade = material.Grade;
                    grade.Enable = g.Enable;
                    grade.MaxValue = g.MaxValue;
                    grade.MinValue = g.MinValue;
                    grade.Prec = g.Prec;
                    grade.ValueSpec = g.ValueSpec;
                    grade.ValidTODate = null;
                    grade.CreateDate = System.DateTime.Now; ;
                    grade.ValidDate = material.ValidDate;
                    grade.Stamp = System.DateTime.Now;
                    grade.UserID = material.UserID;
                    grade.Remark="revise";
                    grades.Add(grade);
                }

                msg = biz.CreateNewVersion(gm.ID.ToString(), gm.Version, grades);


                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                    return Ok(json);             
                }

                //update last verion date

                json["ID"] = gm.ID;
                json["Version"] = gm.Version;
                json["VersionSpc"] = gm.VersionSpc;
                json["SampleName"] = material.SampleName;
                json["LOT_NO"] = material.LOT_NO;
                json["Grades"] = material.Grades;
                return Ok(json);
            }
            else
            {

                return BadRequest("DOn't get Row");
            }
        }
        [Route("SaveSpecial_ModifyVersion")]
        [HttpPost]
        public IHttpActionResult SaveSpecialModifyVersion(GradeMaterialDto material)
        {
            JsonObject json = new JsonObject();
            string msg = string.Empty;
            DataTable dt = biz.IsCanSpecial_UpdateVersion(material.SampleName, material.LOT_NO, material.Grades);
            if (dt.Rows.Count > 0)
            {
                // can revise
                msg = dt.Rows[0]["MSG"].ToString();
                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                    return Ok(json);
                }
                int version = int.Parse(dt.Rows[0]["Version"].ToString());
                DataTable dtNewest = biz.NewestGrades(dt.Rows[0]["ID"].ToString());
                if (dtNewest.Rows.Count < 0)
                {
                    json["Error"] = "NO property Name";
                    return Ok(json);
                }
                DateTime lastversionDate = Convert.ToDateTime(dtNewest.Rows[0]["ValidDate"]);
                if (lastversionDate > material.ValidDate)
                {
                    return BadRequest("Valid Date is Error ,it is min  Last Version");

                }

                GradeMaterial gm = new GradeMaterial();
                gm.ID = Guid.NewGuid().ToString(); 

                gm.SampleName = material.SampleName;
                gm.LOT_NO = material.LOT_NO;
                gm.Version = version;
                gm.Grades = material.Grades == null ? "00" : material.Grades;
                gm.UserID = material.UserID;
                gm.Stamp = System.DateTime.Now;
                gm.Status = "N";
                gm.VersionSpc = "V" + gm.Version.ToString().PadLeft(2, '0');
                //gm.Remark = material.Remark;
                msg = biz.InsertGradeMaterial(gm);
                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                    return Ok(json);
                }

                List<QCGrades> grades = new List<QCGrades>();
                foreach (var g in material.GradesDto)
                {
                    QCGrades grade = new QCGrades();
                    grade.ID = gm.ID;
                    grade.PropertyName = g.PropertyName;
                    grade.Grade = material.Grade;
                    grade.Enable = g.Enable;
                    grade.MaxValue = g.MaxValue;
                    grade.MinValue = g.MinValue;
                    grade.Prec = g.Prec;
                    grade.ValueSpec = g.ValueSpec;
                    grade.ValidTODate = null;
                    grade.CreateDate = System.DateTime.Now; ;
                    grade.ValidDate = material.ValidDate;
                    grade.Stamp = System.DateTime.Now;
                    grade.UserID = material.UserID;
                    grade.Remark = "revise";
                    grades.Add(grade);
                }

                msg = biz.CreateNewVersion(gm.ID.ToString(), gm.Version, grades);


                if (!string.IsNullOrEmpty(msg))
                {
                    json["Error"] = msg;
                    return Ok(json);
                }

                //update last verion date

                json["ID"] = gm.ID;
                json["Version"] = gm.Version;
                json["VersionSpc"] = gm.VersionSpc;
                json["SampleName"] = material.SampleName;
                json["LOT_NO"] = material.LOT_NO;
                json["Grades"] = material.Grades;
                return Ok(json);
            }
            else
            {

                return BadRequest("DOn't get Row");
            }
        }
        [Route("PublishGradeStatus")]
        [HttpGet]
        public IHttpActionResult PublishGradeStatus(string ID, string status)
        {
            string msg = string.Empty;
            if (status == "S")
            {
                msg = biz.PublishVersion(ID);
            }
            else
            {
                msg = "Status is Error";

            }
            return Ok(msg);
        }
        //bpm flow
        [Route("UpdateGradeStatus")]
        [HttpPut]
        public HttpResponseMessage UpdateGradeStatus(string ID, string status)
        {
            if (string.IsNullOrEmpty(ID) )
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Document number is empty！");
            try
            {
                if (status == "S")
                {
                    string msg = biz.PublishVersion(ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Pushlish Version is Failed！");

                    }

                }
                else
                {
                    biz.UpdateInWorkFlow(ID, status);
                }
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                Loger.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message + e.StackTrace);
            }
        }
        [Route("CopyGrade")]
        [HttpPost]
        public HttpResponseMessage CopyGrade(string samplename, string lotno,string grades, string newSampleName, string newLotNo, string newGrades, string userid)
        {
           
            try
            {
                string id = Guid.NewGuid().ToString();
                string msg = biz.CopyGradeVersion(samplename, lotno, grades, id, newSampleName, newLotNo, newGrades, userid);
                if (!string.IsNullOrEmpty(msg))
                {
                    return ReturnJson(msg);
                }
                 
                  return  Request.CreateResponse(HttpStatusCode.OK);

               
            } 
            catch (Exception e)
            {
                return ReturnJson(e.Message + e.StackTrace);
            }
        }
        /// <summary>
        /// 控制台返回值
        /// </summary>
        /// <param name="result">返回值</param>
        private void InterfaceOut(string result)
        {
            Console.WriteLine("返回值");
            Console.WriteLine(result);
        }
        public HttpResponseMessage ReturnJson(string msg)
        {
            JsonObject json = new JsonObject();
            json["Message"] = msg;
            Loger.Error(json);
            InterfaceOut(msg);
            return Request.CreateResponse(HttpStatusCode.BadRequest, json);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

}
