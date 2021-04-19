using LIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Contract
{
    public interface ICourse
    {
        bool AddCourse(Course2 _course);

        bool UpdateCourse(Course2 _course);

        bool DeleteCourse(Course2 _course);

        int FindCourseId(Course2 _course);
    }
}
