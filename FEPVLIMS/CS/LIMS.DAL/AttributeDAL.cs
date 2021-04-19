using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;
using Shawoo.Core;
using Shawoo.Common;


using System.Data.Common;

namespace LIMS.DAL
{
    public class AttributeDAL
    {
        //FEPV LIMS
        protected static NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private DB db = new DB("LIMS");

        public bool AddAttribute(QCAttribute attribute)
        {
            Console.WriteLine("AttributeService-AddAttribute():" + DateTime.Now.ToString());
            try
            {
                return db.Insert(attribute);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }

        public bool UpdateAttribute(QCAttribute attribute)
        {

            Console.WriteLine("AttributeService-UpdateAttribute():" + DateTime.Now.ToString() + attribute.Description_EN);

            try
            {
                return db.Update(attribute);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }

        public bool DelAttribute(QCAttribute attribute)
        {
            Console.WriteLine("AttributeService-DelAttribute():" + DateTime.Now.ToString());

            try
            {

                string msg = gate.DbHelper.ExecuteStoredProcedure("I_CheckDeleteAttibute",
                    new string[] { "SampleName", "PropertyName" },
                    new object[] { attribute.SampleName, attribute.PropertyName }).Tables[0].Rows[0][0].ToString();


                     if (string.IsNullOrEmpty(msg))
                     {
                         return db.Delete(Select(attribute));
                     }
                     else
                     {
                         throw new Exception(msg);

                     }
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }

        public DataTable GetAttribute(string sampleName)
        {
            try
            {
                DataTable dt = gate.DbHelper.Select("Select * From Attribute WHERE SampleName = @sampleName ORDER BY OrderBy", new object[] { sampleName }).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                log.Error(e);
                return null;
            }
        }

        protected QCAttribute Select(QCAttribute attribute)
        {
            try
            {
                return db.Select<QCAttribute>(new QCAttribute { SampleName = attribute.SampleName, PropertyName = attribute.PropertyName });
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message);
            }
        }
    }
}