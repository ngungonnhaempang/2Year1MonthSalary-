using LIMS.Contract;
using LIMS.Model;
using Shawoo.Core;

namespace LIMS.Service
{
    public class DOCPlanAddBiz
    {
        private readonly IDOCPlanAdd proxy = ServiceFactory.Create<IDOCPlanAdd>();

        public bool DeletePlandAdd(string voucherID)
        {
            return proxy.DeletePlandAdd(voucherID);
        }

        public bool InsertPlanByAdd(DOCPlan DOC, int AddID, out string VoucherID)
        {
            return proxy.InsertPlanByAdd(DOC, AddID, out VoucherID);
        }

        public bool RejectPlandAdd(string voucherID)
        {
            return proxy.RejectPlandAdd(voucherID);
        }

        public bool InsertPlanAdd(DOCPlanAdd DOC)
        {
            return proxy.InsertPlanAdd(DOC);
        }
    }
}