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
    public class AttributeBiz
    {
        private readonly IAttribute proxy = ServiceFactory.Create<IAttribute>();

        public bool AddAttribute(QCAttribute attribute, out string msg)
        {
            return proxy.AddAttribute(attribute, out msg);
        }

        public bool UpdateAttribute(QCAttribute attribute)
        {
            return proxy.UpdateAttribute(attribute);
        }

        public  bool DelAttribute(string sampleName, string propertyName)
        {
            return proxy.DelAttribute(sampleName,propertyName);
        }
    }
}