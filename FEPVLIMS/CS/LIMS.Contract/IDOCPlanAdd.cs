using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    public interface IDOCPlanAdd
    {
        bool InsertPlanByAdd(DOCPlan DOC, int AddID, out string VoucherID);

        bool RejectPlandAdd(string voucherID);

        bool InsertPlanAdd(DOCPlanAdd DOC);

        bool DeletePlandAdd(string voucherID);
   }
}
