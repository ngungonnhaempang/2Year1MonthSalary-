using System;
using Shawoo.Core;

namespace LIMS.Model
{
    [Serializable]
    [Table("PlanTimeJob")]
    public class PlanTimeJob: ORM
    {
        /// <summary>
        /// Output after create a Schedule
        /// </summary>
        [Column("JobID", true)]
        public Guid JobID { get; set; }

        /// <summary>
        /// Output after create a Schedule
        /// </summary>
        [Column("ScheduleUID", true)]
        public Guid ScheduleUID { get; set; }

        /// <summary>
        /// Schedule Name
        /// </summary>
        [Column("ScheduleName")]
        public string ScheduleName { get; set; }

        /// <summary>
        /// Sample Name
        /// </summary>
        [Column("SampleName")]
        public string SampleName { get; set; }

        /// <summary>
        /// Line
        /// </summary>
        [Column("LINE")]
        public string LINE { get; set; }

        /// <summary>
        /// Material No
        /// </summary>
        [Column("LOT_NO")]
        public string LOT_NO { get; set; }

        /// <summary>
        /// 1.Once
        /// 2.Daily
        /// 8.Weekly
        /// 16.Monthly
        /// 32.Monthly with RecurrenceFactor
        /// </summary>
        [Column("Type")]
        public int Type { get; set; }

        /// <summary>
        /// Number Unit By Type
        /// </summary>
        [Column("Interval")]
        public int Interval { get; set; }

        /// <summary>
        /// 1. Specical Time
        /// 4. Minute
        /// 8. Hour
        /// </summary>
        [Column("DayUnit")]
        public int DayUnit { get; set; }

        /// <summary>
        /// Interval unit from DayUnit
        /// </summary>
        [Column("DayInterval")]
        public int DayInterval { get; set; }

        /// <summary>
        /// When frequency_type is set to 32 (monthly)
        /// </summary>
        [Column("RelativeInterval")]
        public int RelativeInterval { get; set; }

        /// <summary>
        /// Number of weeks or months 
        /// </summary>
        [Column("RecurrenceFactor")]
        public int RecurrenceFactor { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        [Column("StartTime")]
        public DateTime StartTime { get; set; }

        [Column("EndTime")]
        public DateTime EndTime { get; set; }

        [Column("CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("AccDate")]
        public DateTime AccDate { get; set; }

        /// <summary>
        /// Enabled
        /// </summary>
        [Column("Enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// UserID
        /// </summary>
        [Column("UserID")]
        public string UserID { get; set; }

        [Column("Remark")]
        public string Remark { get; set; }
    }
}