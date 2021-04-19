using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class GradeService : MarshalByRefObject, IGrade
    {
        public GradeService()
        {
        }

        //FEPV LIMS
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        private DB db = new DB("LIMS");

        public GradeVer SelectGrade(Guid gradeid, int version)
        {
            Console.WriteLine("GradeDAL-SelectGrade():" + DateTime.Now.ToString());

            try
            {
                return db.Select(new GradeVer { ID = gradeid, Version = version});
            }
            catch (Exception ex)
            {
               
               
                throw new Exception(ex.Message);
            }
        }


        public bool AddGrade(GradeVer grade, out string msg)
        {
            Console.WriteLine("GradeDAL-AddGrade():" + DateTime.Now.ToString());
            msg ="";
            try
            {
                int countgrade = acQC.SelectScalar<int>("SELECT COUNT(*) FROM GradeVer WHERE SampleName = @SampleName AND LOT_NO = @LOT_NO",
                                new object[]{grade.SampleName,grade.LOT_NO});
                if (countgrade >0)
                {
                    msg = string.Format("Already Exist Grade For Material: {0}, you can only add the new version!", grade.LOT_NO);
                    return false;
                }

                return db.Insert(grade);
            }
            catch (Exception ex)
            {
           
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateGrade(GradeVer grade)
        {
            Console.WriteLine("GradeDAL-UpdateGrade():" + DateTime.Now.ToString());

            try
            {
                GradeVer oldGradeVer = db.Select<GradeVer>(new GradeVer { ID = grade.ID, Version = grade.Version });
                oldGradeVer.ValidTODate = grade.ValidDate;
                db.Update(oldGradeVer);
                grade.Version = acQC.SelectScalar<int>("SELECT MAX([Version])+1 FROM GradeVer WHERE ID = @ID", new object[] { grade.ID });
                return db.Insert(grade);
            }
            catch (Exception ex)
            {
               
            
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteGrade(Guid id)
        {
            Console.WriteLine("GradeDAL-DeleteVersion():" + DateTime.Now.ToString());
            try
            {
                acQC.DbHelper.Delete("GradeVer", "ID = @ID", new object[] { id });
                return true;
            }
            catch (Exception ex)
            {
           
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteVersion(GradeVer grade)
        {
            Console.WriteLine("GradeDAL-DeleteGrade():" + DateTime.Now.ToString());
            try
            {
                db.Delete(grade);
                return true;
            }
            catch (Exception ex)
            {
            
                throw new Exception(ex.Message);
            }
        }
    }
}