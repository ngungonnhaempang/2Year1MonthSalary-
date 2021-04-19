using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    public interface IMaterial
    {
        bool CreateMaterial(QCMaterial Material);

        bool UpdateMaterial(QCMaterial Material);

        bool DeleleMaterial(QCMaterial Material);


        DataTable GetMaterialByCondition(string sampleName, string query);

        QCMaterial GetMaterial(string sampleName, string lotNo);
    }
}