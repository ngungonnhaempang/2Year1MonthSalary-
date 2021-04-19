using LIMS.Model;
using Shawoo.Common;
using Shawoo.Core;
using System;
using System.Data;

namespace LIMS.DAL
{
    public class MaterialDAL
    {
        public MaterialDAL()
        {
            //  acQC.RegisterSqlLogger(new NBear.Common.LogHandler());
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");
        private DB db = new DB("LIMS");

        public DataTable GetMaterial(string userID, string sampleName, string query)
        {
            Console.WriteLine("MaterialDAL - GetMaterial():" + DateTime.Now.ToString());
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("Q_Materials",
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

        /// <summary>
        /// Create New Material
        /// </summary>
        /// <param name="Material"></param>
        /// <returns></returns>
        public bool CreateMaterial(QCMaterial Material)
        {
            Console.WriteLine("MaterialDAL-CreateMaterial():" + DateTime.Now.ToString());
            try
            {
                return db.Insert(Material);
            }
            catch (Exception e)
            {
                log.Error(e);
              
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Modify Material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public bool UpdateMaterial(QCMaterial material)
        {
            Console.WriteLine("MaterialDAL-UpdateMaterial():" + DateTime.Now.ToString());
            try
            {
                return db.Update(material);
            }
            catch (Exception e)
            {
                log.Error(e);
              
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public bool DelMaterial(QCMaterial material)
        {
            Console.WriteLine("MaterialDAL-DelMaterial():" + DateTime.Now.ToString());
            try
            {

                string msg =gate.DbHelper.ExecuteStoredProcedure("I_CheckDeleteMaterial",
                    new string[] { "SampleName", "LOT_NO" },
                    new object[] { material.SampleName, material.LOT_NO }).Tables[0].Rows[0][0].ToString();
            
              
                if (string.IsNullOrEmpty(msg))
                {
                    return db.Delete(GetMaterial(material.SampleName, material.LOT_NO));
                }
                else
                {
                    throw new Exception(msg);
                    
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
               
                throw new Exception(ex.Message);
            }
        }
    }
}