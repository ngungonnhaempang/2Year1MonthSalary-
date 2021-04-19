using LIMS.Contract;
using LIMS.Model;
using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Service
{
    public class CourseBiz: ICourse
    {
        private readonly ICourse proxy = ServiceFactory.Create<ICourse>();

        public bool AddCourse(Course2 _course)
        {
            return proxy.AddCourse(_course);
        }

        public bool UpdateCourse(Course2 _course)
        {
            return proxy.UpdateCourse(_course);
        }

        public bool DeleteCourse(Course2 _course)
        {
            return proxy.DeleteCourse(_course);
        }

        public int FindCourseId(Course2 _course)
        {
            return proxy.FindCourseId(_course);
        }
    }
}
