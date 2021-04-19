using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using LIMS.Contract;
using LIMS.Model;
using Shawoo.Core;

namespace LIMS.Service
{
    public class SampleBiz
    {
        private readonly ISample proxy = ServiceFactory.Create<ISample>();

        public bool CreateSample(QCSample sample)
        {
            return proxy.CreateSample(sample);
        }

        public bool UpdateSample(QCSample sample)
        {
            return proxy.UpdateSample(sample);
        }

        public string DeleteSample(string SampleName)
        {
            return proxy.DeleteSample(SampleName);
        }


        public DataTable GetSample(string query)
        {
            return proxy.GetSample(query);
        }

        public DataTable GetLine(string sampleName)
        {
            return proxy.GetLine(sampleName);
        }


        public bool AssignUserForSample(string username, string sampleid)
        {
            return proxy.AssignUserForSample(username,sampleid);
        }

        public DataTable QuerySampleUser(string userid)
        {
            return proxy.QuerySampleUser(userid);
        
        }

        public UserInSamples SelectSampleUser(string userid, string samplename)
        {

            return proxy.SelectSampleUser(userid, samplename);
        }

        public bool DeleteSampleUser(string userid, string samplename)
        {

            return proxy.DeleteSampleUser(userid, samplename);
        }
    }
}