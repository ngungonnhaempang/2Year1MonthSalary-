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
    public class MaterialBiz
    {
        private readonly IMaterial proxy = ServiceFactory.Create<IMaterial>();

        public bool CreateMaterial(QCMaterial Material)
        {
            return proxy.CreateMaterial(Material);
        }

        public bool UpdateMaterial(QCMaterial Material)
        {
            return proxy.UpdateMaterial(Material);
        }

        public bool DeleteMaterial(QCMaterial Material)
        {
            return proxy.DeleleMaterial(Material);
        }

        public DataTable GetMaterialByCondition(string sampleName, string query)
        {
            return proxy.GetMaterialByCondition(sampleName, query);
        }

        public QCMaterial GetMaterial(string sampleName, string lotNo)
        {
            return proxy.GetMaterial(sampleName, lotNo);
        }
    }
}