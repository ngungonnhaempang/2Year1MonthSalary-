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
using LIMS.DAL;

namespace LIMS.Service
{
    public class AttributeService : MarshalByRefObject, IAttribute
    {
        //FEPV LIMS
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DB db = new DB("LIMS");

        private AttributeDAL attributeDAL = new AttributeDAL();

        public bool AddAttribute(QCAttribute attribute, out string msg)
        {
            Console.WriteLine("AttributeService-AddAttribute():" + DateTime.Now);
            try
            {
                msg = "";
              
                Console.WriteLine("AttributeService-AddAttribute():" + DateTime.Now);
                attribute.OrderBy = (int)acQC.DbHelper.SelectScalar(@"
                                     SELECT ISNULL(max(OrderBy),0)+1 
                                      FROM [Attribute]
                                     WHERE SampleName = @SampleName",
                                              new object[] { attribute.SampleName});

                return attributeDAL.AddAttribute(attribute);
            }
            catch (Exception e)
            {
                log.Error(e);
                
                throw new Exception(e.Message);
            }
        }

        public bool UpdateAttribute(QCAttribute attribute)
        {
           
            Console.WriteLine("AttributeService-UpdateAttribute():" + DateTime.Now);
            return attributeDAL.UpdateAttribute(attribute);
        }

        public bool DelAttribute(string sampleName,string propertyName)
        {
            Console.WriteLine("AttributeService-DelAttribute():" + DateTime.Now);
            var att = db.Select<QCAttribute>(new QCAttribute { SampleName = sampleName, PropertyName = propertyName });
            return attributeDAL.DelAttribute(att);
        }
    }
}