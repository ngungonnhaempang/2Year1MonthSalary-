using LIMS.Contract;
using LIMS.Model;
using Shawoo.Common;
using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Service
{
    public class CourseService : MarshalByRefObject, ICourse
    {
        private DB db = new DB("University");

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool  AddCourse(Course2 _course)  
        {           
           try {
                return db.Insert(_course);             
            }
            catch (Exception e)
            {
                log.Error(e);
                Logger.Warnning(e);
                throw new Exception(e.Message);
            }
        }

        public bool UpdateCourse(Course2 _course)
        {
            
            try
            {
                return db.Update(_course);


            }
            catch (Exception e)
            {
                log.Error(e);
                Logger.Warnning(e);
                throw new Exception(e.Message);
            }
        }

        public bool DeleteCourse(Course2 _course)
        {
            try
            {
                return db.Delete(_course);


            }
            catch (Exception e)
            {
                log.Error(e);
                Logger.Warnning(e);
                throw new Exception(e.Message);
            }
        }

        public int FindCourseId(Course2 _course)
        {
            throw new NotImplementedException();
        }


   
    }
}
