using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;
using Shawoo.Core;
using Shawoo.Common;

namespace LIMS.DAL
{
    public class SampleDAL
    {
        public SampleDAL()
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //FEPV LIMS
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        private DB db = new DB("LIMS");

        private string CreateSampleNameCode(int labId,int typeId)
        {

            int Id = 0;
            try
            {

                 Id = (int)acQC.DbHelper.SelectScalar(@"
                                    SELECT ISNULL(MAX(CONVERT(INT,RIGHT(SampleName,4))),0) +1 
                                    FROM Samples  
                                     WHERE LabID = @LabID AND TypeID = @TypeID ",
                                              new object[] { labId ,typeId});


                 Console.WriteLine(labId);
                 Console.WriteLine(typeId);
                 return "S" + labId.ToString().PadLeft(2, '0') +
                     typeId.ToString().PadLeft(2, '0') +    Id.ToString().PadLeft(4, '0');
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }
        
        }
      
        /// <summary>
        /// Add a new Sample
        /// </summary>
        /// <param name="sample"> Sample Model</param>
        /// <returns></returns>
        public bool AddSample(QCSample sample)
        {
            string sampleName=CreateSampleNameCode(sample.LabID, sample.TypeID);
            sample.SampleName = sampleName;
            Console.WriteLine("SampleDAL - AddSample():" + DateTime.Now.ToString());
            try
            {
                return db.Insert(sample);
            }
            catch (Exception e)
            {
                log.Error(e);
           
                throw new Exception(e.Message);
            }
        }

        public bool UpdateSample(QCSample sample)
        {
            Console.WriteLine("SampleDAL - UpdateSample():" + DateTime.Now.ToString());
            try
            {
                return db.Update(sample);
            }
            catch (Exception e)
            {
                log.Error(e);
              
                throw new Exception(e.Message);
            }
        }

        public string DeleteSample(string samplename)
        {
            Console.WriteLine("SampleDAL - DeleteSample():" + DateTime.Now.ToString());

            try
            {
                if (db.Delete(db.Select<QCSample>(new QCSample { SampleName = samplename })))
                {
                    return "";
                }
                else
                {
                    return "Delete SampleName Failed!";
                }
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
        public QCSample SelectSample(string sampleName)
        {
            return db.Select(new QCSample { SampleName = sampleName });
        }
        public DataTable GetLine(string userId, string sampleName)
        {
            try
            {
                return acQC.DbHelper.ExecuteStoredProcedure("Q_GetLinesBySampleName", new string[] { "UserID", "SampleName" }, new object[] { userId, sampleName }).Tables[0];
            }
            catch (Exception e)
            {
                log.Error(e);
              
                throw new Exception(e.Message);
            }
        }
    }
}