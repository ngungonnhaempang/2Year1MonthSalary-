using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    public interface IGrade
    {
        GradeVer SelectGrade(Guid gradeid, int version);

        bool AddGrade(GradeVer grade, out string msg);

        bool UpdateGrade(GradeVer grade);

        bool DeleteGrade(Guid id);

        bool DeleteVersion(GradeVer grade);
    }
}