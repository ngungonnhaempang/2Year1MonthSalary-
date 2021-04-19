using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    public interface ISample
    {
        bool CreateSample(QCSample sample);

        bool UpdateSample(QCSample sample);

        /// <summary>
        /// delete SampleName
        /// </summary>
        /// <param name="sampleName"></param>
        /// <returns>error msg</returns>
        string DeleteSample(string sampleName);

        DataTable GetSample( string query);

        DataTable GetLine(string sampleName);
        bool AssignUserForSample(string username, string sampleid);
        DataTable QuerySampleUser(string userid);
        UserInSamples SelectSampleUser(string userid, string samplename);
        bool DeleteSampleUser(string userid, string samplename);
    }
}