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
    public class DOCRequisitionBiz
    {
        private readonly IDOCRequisition proxy = ServiceFactory.Create<IDOCRequisition>();

        public bool Accept(string voucherID, out string msg)
        {
            return proxy.Accept(voucherID, out msg);
        }

        public bool Reject(string voucherID, out string msg)
        {
            return proxy.Reject(voucherID, out msg);
        }

    }
}