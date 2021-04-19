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
    public class GradeBiz
    {
        private readonly IGrade proxy = ServiceFactory.Create<IGrade>();

        public GradeVer SelectGrade(Guid gradeid, int version)
        {
            return proxy.SelectGrade(gradeid, version);
        }

        public bool AddGrade(GradeVer grade, out string msg)
        {
            return proxy.AddGrade(grade,out msg);
        }

        public bool UpdateGrade(GradeVer grade)
        {
            return proxy.UpdateGrade(grade);
        }

        public bool DeleteGrade(Guid id)
        {
            return proxy.DeleteGrade(id);
        }

        public bool DeleteVersion(GradeVer grade)
        {
            return proxy.DeleteVersion(grade);
        }
    }
}