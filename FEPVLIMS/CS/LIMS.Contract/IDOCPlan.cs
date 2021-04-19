using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    public interface IDOCPlan
    {
        bool InsertPlan(DOCPlan doc, ref string VoucherID, out string msg);

        bool DeletePlan(string VoucherID);
    }
}
