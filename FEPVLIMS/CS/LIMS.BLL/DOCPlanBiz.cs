using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LIMS.Contract;
using Shawoo.Core;
using LIMS.Model;

namespace LIMS.Service
{
    public class DOCPlanBiz
    {
        private readonly IDOCPlan proxy = ServiceFactory.Create<IDOCPlan>();

        public bool InsertPlan(DOCPlan doc, ref string VoucherID, out string msg)
        {
            return proxy.InsertPlan(doc, ref VoucherID, out msg);
        }

        public bool DeletePlan(string VoucherID)
        {
            return proxy.DeletePlan(VoucherID);
        }
    }
}