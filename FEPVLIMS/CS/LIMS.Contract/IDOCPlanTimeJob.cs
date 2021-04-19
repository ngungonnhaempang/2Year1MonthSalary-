using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    public interface IDOCPlanTimeJob
    {
        bool CheckPlanJob(PlanTimeJob plan);

        List<string> GetItems(Guid job_id, Guid schedule_uid);

        bool Create(PlanTimeJob plan, List<string> propertylist);

        bool StartStopSchedule(PlanTimeJob plan);

        bool CreateSchedule(PlanTimeJob plan, List<string> propertylist);

        bool Update(PlanTimeJob plan, List<string> propertylist);

        bool Delete(PlanTimeJob plan);
   }
}
