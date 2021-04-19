using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LIMS.Contract;
using LIMS.Model;
using MIS.Utility;
using System.Data;
using Shawoo.Core;
using Shawoo.Common;
using System.ServiceModel;
using System.Data.Common;
using CN.Teddy.DesignByContract;

namespace LIMS.Service
{
    public class DOCPlanTimeJobService : MarshalByRefObject, IDOCPlanTimeJob
    {
        //FEPV LIMS
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        private readonly DB db = new DB("LIMS");

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool CheckPlanJob(PlanTimeJob plan)
        {
            Console.WriteLine("DOCPlanTimeJobDAL-CheckPlanJob():" + DateTime.Now);

            try
            {
                int count = acQC.SelectScalar<int>(@"SELECT COUNT(*) FROM PlanTimeJob 
                                              WHERE SampleName = @SampleName 
                                              AND LINE = @LINE
                                              AND LOT_NO = @LOTNO ",
                           new object[] { plan.SampleName, plan.LINE, plan.LOT_NO });
                return count <= 0;
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }

        public List<string> GetItems(Guid job_id, Guid schedule_uid)
        {
            Console.WriteLine("DOCPlanTimeJobDAL-GetItems():" + DateTime.Now);

            try
            {
                var dt = acQC.DbHelper.Select("SELECT ItemOrder, PropertyName FROM PlanTimeJob_Items WHERE JobID = @JobID AND ScheduleUID = @ScheduleUID", new object[] { job_id, schedule_uid }).Tables[0];
                var ItemList = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    ItemList.Add(dr["PropertyName"].ToString());
                }

                return ItemList;
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }

        public bool Create(PlanTimeJob plan, List<string> propertylist)
        {
            Console.WriteLine("DOCPlanTimeJobDAL-Create():" + DateTime.Now);
            var trans = acQC.BeginTransaction();
            try
            {
                plan.UserID = Shawoo.GenuineChannels.GenuineUtility.CurrentSession["UID"].ToString();
                //1.Create JobID
                if (CheckPlanJob(plan))
                    plan.JobID = Guid.NewGuid();
                else
                {
                    DataTable dt = acQC.DbHelper.Select(@"SELECT JobID FROM PlanTimeJob 
                                                        WHERE SampleName = @SampleName 
                                                        AND LINE = @LINE
                                                        AND LOT_NO = @LOTNO", new object[] { plan.SampleName, plan.LINE, plan.LOT_NO }).Tables[0];
                    if(dt.Rows.Count <= 0)
                        throw new Exception("Error when get JobID");
                    plan.JobID = new Guid(dt.Rows[0]["JobID"].ToString());
                }

                //2.Create ScheduleUID and ScheduleName
                CreateSchedule(plan, propertylist);

                trans.Commit();
                return true;
            }
            catch (Exception e)
            {
                trans.Rollback();
                log.Error(e);
              
                throw new Exception(e.Message);
            }
        }

        public bool Update(PlanTimeJob plan, List<string> propertylist)
        {
            Console.WriteLine("DOCPlanTimeJobDAL-Update():" + DateTime.Now);

            try
            {
                if (CheckPlanJob(plan))
                {
                    throw new Exception(string.Format("Plan already Exist! {0} - {1} - {2}", plan.SampleName, plan.LOT_NO, plan.LINE));
                }

                return db.Update(plan);
            }
            catch (Exception e)
            {
                log.Error(e);
             
                throw new Exception(e.Message);
            }
        }

        public bool StartStopSchedule(PlanTimeJob plan)
        {
            Console.WriteLine("DOCPlanTimeJobDAL-Start/StopSchedule():" + DateTime.Now);
            Console.WriteLine("{0}................Schedule + {1} " + DateTime.Now, plan.Enabled ? "Stop" : "Start", plan.ScheduleName);
            try
            {
                string UserID = Shawoo.GenuineChannels.GenuineUtility.CurrentSession["UID"].ToString();
                acQC.DbHelper.ExecuteNonQuery("UPDATE PlanTimeJob SET Enabled = @Enabled WHERE JobID = @JobID AND ScheduleUID = @ScheduleUID", new object[] { !plan.Enabled, plan.JobID, plan.ScheduleUID });
                
                acQC.DbHelper.ExecuteStoredProcedureNonQuery("WF_JobSchedule_SaveLog",
                    new string[] {"UserID", "JobID", "ScheduleUID", "IsSuccess", "Message"},
                    new object[] { UserID, plan.JobID, plan.ScheduleUID, true, string.Format("{0} {1} schedule at: {2:dd-MM-yyyy HH:mm:ss}", UserID, plan.Enabled ? "Stop" : "Start", DateTime.Now)});
                return true;
            }
            catch (Exception e)
            {
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }

        public bool CreateSchedule(PlanTimeJob plan, List<string> propertylist)
        {
            Console.WriteLine("DOCPlanTimeJobDAL-CreateSchedule():" + DateTime.Now);
            var trans = acQC.BeginTransaction();
            try
            {
                plan.ScheduleName = string.Format("Schedule_{0:yyyyMMdd_HHmmss}", DateTime.Now);
                plan.ScheduleUID = Guid.NewGuid();
                plan.UserID = Shawoo.GenuineChannels.GenuineUtility.CurrentSession["UID"].ToString();
                plan.Remark = CreateRemark(plan);
                //1.Create Schedule
                db.Insert(plan, trans);

                //2.Save To PlanTimeJob_Items
                for (int i = 0; i < propertylist.Count; i++)
                {
                    var planitem = new PlanTimeJob_Items
                    {
                        JobID = plan.JobID,
                        ScheduleUID = plan.ScheduleUID,
                        ItemOrder = i + 1,
                        SampleName = plan.SampleName,
                        PropertyName = propertylist[i]
                    };
                    db.Insert(planitem, trans);
                }
                trans.Commit();
                return true;
            }
            catch (Exception e)
            {
                trans.Rollback();
                log.Error(e);
               
                throw new Exception(e.Message);
            }
        }

        public bool Delete(PlanTimeJob plan)
        {
            Console.WriteLine("DOCPlanTimeJobDAL-Delete():" + DateTime.Now);
            var trans = acQC.BeginTransaction();
            try
            {
                //Delete Logs
                acQC.DbHelper.ExecuteNonQuery("DELETE PlanTimeJob_Logs WHERE JobID=@JobID AND ScheduleUID=@ScheduleUID", new object[] { plan.JobID, plan.ScheduleUID });
                //Delete All Properties
                acQC.DbHelper.ExecuteNonQuery("DELETE PlanTimeJob_Items WHERE JobID = @JobID AND ScheduleUID=@ScheduleUID", new object[] { plan.JobID, plan.ScheduleUID });
                //Delete Schedule
                db.Delete(plan);
                trans.Commit();
                return true;
            }
            catch (Exception e)
            {
                trans.Rollback();
                log.Error(e);
                throw new Exception(e.Message);
            }
        }

        private string CreateRemark(PlanTimeJob plan)
        {
            try
             {
                var dt = acQC.DbHelper.ExecuteStoredProcedure("WF_JobSchedule_GetDescription",
                    new string[] { "Type", "Interval", "DayUnit", "DayInterval", "RelativeInterval", "RecurrenceFactor", "StartDate", "EndDate", "StartTime", "EndTime" },
                    new object[] { plan.Type, plan.Interval, plan.DayUnit, plan.DayInterval, plan.RelativeInterval, plan.RecurrenceFactor, plan.StartDate, plan.EndDate, plan.StartTime, plan.EndTime }).Tables[0];

                return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not create Schedule Description:" + ex.Message);
                log.Error(ex);
                return "";
            }
        }
    }
}