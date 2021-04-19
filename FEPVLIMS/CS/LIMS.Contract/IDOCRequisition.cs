using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    public interface IDOCRequisition
    {
        bool Accept(string voucherID, out string msg);

        bool Reject(string voucherID, out string msg);
    }
}
