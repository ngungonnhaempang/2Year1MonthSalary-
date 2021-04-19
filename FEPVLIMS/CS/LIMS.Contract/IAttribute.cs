using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    public interface IAttribute
    {
        bool AddAttribute(QCAttribute attribute, out string msg);

        bool UpdateAttribute(QCAttribute attribute);

        bool DelAttribute(string sampleName, string propertyName);
    }
}
