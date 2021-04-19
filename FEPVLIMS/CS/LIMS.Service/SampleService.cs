using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Contract;
using LIMS.Model;
using MIS.Utility;
using System.Data;
using Shawoo.Core;
using Shawoo.Common;
using System.ServiceModel;
using LIMS.DAL;

namespace LIMS.Service
{
    public class SampleService : MarshalByRefObject, ISample
    {

        //FEPV LIMS
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private DB db = new DB("LIMS");
        private SampleDAL sampledal = new SampleDAL();

        public bool CreateSample(QCSample sample)
        {
            return sampledal.AddSample(sample);
        }
        public bool UpdateSample(QCSample sample)
        {
            Console.WriteLine("SampleService-DeleteSample():" + DateTime.Now);
            try
            {
                return sampledal.UpdateSample(sample);
            }
            catch (Exception e)
            {
                log.Error(e);
              
                throw new Exception(e.Message);
            }   
            
        }

        public string DeleteSample(string sampleName)
        {
            Console.WriteLine("SampleService-DeleteSample():" + DateTime.Now);
            try
            {
              
                return sampledal.DeleteSample(sampleName);
            }
            catch (Exception e)
            {
                log.Error(e);
              
                throw new Exception(e.Message);
            }          
        }

        public DataTable GetSample(string query)
        {

            return sampledal.GetSample(Token.UID, query);
        }

        public DataTable GetLine(string sampleName)
        {
            return sampledal.GetLine(Token.UID, sampleName);
        }


        /// <summary>
        /// set UserInSamples
        /// </summary>
        /// <param name="username"></param>
        /// <param name="sampleid"></param>
        /// <returns></returns>
        public bool AssignUserForSample(string username, string sampleid)
        {

            try
            {

                UserInSamples us = new UserInSamples();
                us.UserID = username;
                us.SampleName = sampleid;
                return db.Save(us);
            }
            catch (Exception e)
            {

                log.Error(e);
            
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// query list UserInSamples
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable QuerySampleUser(string userid)
        {


            DataTable ds = acQC.ExecuteStoredProcedure("CU18_Samples", new string[] { "UserID" }, new object[] { userid }).Tables[0];
            return ds;

        }
        /// <summary>
        /// query UserInSamples
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="samplename"></param>
        /// <returns></returns>
        public UserInSamples SelectSampleUser(string userid, string samplename)
        {


            return db.Select(new UserInSamples { UserID = userid, SampleName = samplename });

        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="samplename"></param>
        /// <returns></returns>
        public bool DeleteSampleUser(string userid, string samplename)
        {
            UserInSamples us = db.Select(new UserInSamples { UserID = userid, SampleName = samplename });
            return db.Delete(us);
        }
    }
}