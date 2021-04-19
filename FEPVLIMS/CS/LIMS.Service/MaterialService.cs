using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.DAL;
using LIMS.Contract;
using LIMS.Model;
using MIS.Utility;
using System.Data;
using Shawoo.Core;
using Shawoo.Common;
using System.ServiceModel;
using System.Data.Common;

namespace LIMS.Service
{
    public class MaterialService : MarshalByRefObject, IMaterial
    {
        private MaterialDAL materalDal = new MaterialDAL();

        public bool CreateMaterial(QCMaterial material)
        {
            return materalDal.CreateMaterial(material);
        }

        public bool UpdateMaterial(QCMaterial material)
        {
            return materalDal.UpdateMaterial(material);
        }

        public bool DeleleMaterial(QCMaterial material)
        {
            return materalDal.DelMaterial(material);
        }




        public DataTable GetMaterialByCondition(string sampleName, string query)
        {
            return materalDal.GetMaterial(Shawoo.GenuineChannels.GenuineUtility.CurrentSession["UID"].ToString(), sampleName, query);
        }

        public QCMaterial GetMaterial(string sampleName, string lotNo)
        {
            return materalDal.GetMaterial(sampleName, lotNo);
        }

       
    }
}