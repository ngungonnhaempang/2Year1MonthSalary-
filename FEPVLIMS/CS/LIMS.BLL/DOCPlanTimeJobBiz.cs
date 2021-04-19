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
    public class DOCPlanTimeJobBiz
    {
        private readonly IDOCPlanTimeJob proxy = ServiceFactory.Create<IDOCPlanTimeJob>();

        public bool CheckPlanJob(PlanTimeJob plan)
        {
            return proxy.CheckPlanJob(plan);
        }

        public List<string> GetItems(Guid job_id, Guid schedule_uid)
        {
            return proxy.GetItems(job_id, schedule_uid);}

        public bool Create(PlanTimeJob plan, List<string> propertylist)
        {
            return proxy.Create(plan, propertylist);
        }

        public bool CreateSchedule(PlanTimeJob plan, List<string> propertylist)
        {
            return proxy.CreateSchedule(plan, propertylist);
        }

        public bool StartStopSchedule(PlanTimeJob plan)
        {
            return proxy.StartStopSchedule(plan);
        }

        public bool Update(PlanTimeJob plan, List<string> propertylist)
        {
            return proxy.Update(plan, propertylist);
        }

        public bool Delete(PlanTimeJob plan)
        {
            return proxy.Delete(plan);
        }
    }
}