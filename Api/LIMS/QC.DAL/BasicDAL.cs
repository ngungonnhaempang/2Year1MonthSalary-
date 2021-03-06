using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS.Model;
using Shawoo.Core;
using Shawoo.Common;

namespace QC.DAL
{
    /// <summary>
    /// basic  data query
    /// </summary>
    public class BasicDAL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");
        private DB db = new DB("LIMS");
        private SampleService sampleDAL = new SampleService();
        public DataTable GetCategory(string userID, string language)
        {
            return sampleDAL.GetCategory(userID, language);
        }
        public DataTable GetSampleByCategory(string userID, string typeID)
        {
            return sampleDAL.GetSampleByCategory(userID, typeID);
        }
        public DataTable GetSamplesBySampleName(string userID, string SampleName)
        {
            return sampleDAL.GetSamplesBySampleName(userID, SampleName);
        }
        public DataTable GetSample(string userID, string query)
        {
            return sampleDAL.GetSample(userID, query);
        }

        public DataTable GetMaterial(string userID, string sampleName, string query)
        {
            return sampleDAL.GetMaterial(userID, sampleName, query);
        }
        public DataTable GetVendor(string Language, string Fromtime, string Totime)
        {
            return sampleDAL.GetVendor(Language, Fromtime, Totime);
        }

        public DataTable GetAttribute(string sampleName)
        {
            return sampleDAL.GetAttribute(sampleName);
        }

        public DataTable getStatus(string cType, string Lan)
        {
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_GetState",
                    new string[] { "CType", "Lan" },
                    new object[] { cType, Lan }).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }

        public string GetSpec(string sampleName)
        {
            try
            {
                QCSample sample = db.Select<QCSample>(new QCSample { SampleName = sampleName });

                if (string.IsNullOrEmpty(sample.Description_CN))
                {
                    return "";
                }

                WorkCenter workCenter = db.Select<WorkCenter>(new WorkCenter { LabID = sample.LabID, Lang = "CN" });
                Category category = db.Select<Category>(new Category { TypeID = sample.TypeID, Lang = "CN" });
                return string.Format("({0}){1}-{2}", workCenter.LabID, workCenter.LabName, category.TypeName);
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }
        public List<string> GetLinesBySampleName(string userId,string sampleName)
        {
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_GetLinesBySampleName",
                    new string[] { "UserID", "SampleName" },
                    new object[] { userId, sampleName }).Tables[0];

                List<string> lines = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    lines.Add(dr["Line"].ToString());
                }

                return lines;
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }

        public DataTable GetLinesByAB(string userid, string ab,string sampleName)
        {
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("P_GetLines",
                    new string[] { "UserID", "SampleName", "AB " },
                    new object[] { userid, sampleName, ab }).Tables[0];

                return dt;
            }
            catch (Exception e)
            {
                log.Error(e);
                
                throw new Exception(e.Message);
            }
        
        }
        public List<string> GetLines(string userId)
        {
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_GetLines",
                    new string[] { "UserID" },
                    new object[] { userId }).Tables[0];

                List<string> lines = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    lines.Add(dr["Line"].ToString());
                }

                return lines;
            }
            catch (Exception e)
            {
                log.Error(e);
                
                throw new Exception(e.Message);
            }
        }

        public DataTable GetVendors(string userId, string sampleName, string lotNO)
        {
            try
            {

                //return null;
                return acQC.ExecuteStoredProcedure("Q_GetVendors", new string[] { "UserID", "SampleName", "LOT_NO" }, new object[] { userId, sampleName, lotNO }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);
              
                throw new Exception(e.Message);
            }
        }

        public DataTable GetCreateType(string userId, string lang)
        {
            try
            {
                DataTable dt = acQC.DbHelper.ExecuteStoredProcedure("Q_GetCreatePlanType",
                    new string[] { "UserID", "Lang" },
                    new object[] { userId,lang }).Tables[0];
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