using System;
using Shawoo.Core;

namespace LIMS.Model
{
    [Serializable]
    [Table("PlanTimeJob_Items")]
    public class PlanTimeJob_Items : ORM
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
        /// ItemOrder
        /// </summary>
        [Column("ItemOrder")]
        public int ItemOrder { get; set; }

        /// <summary>
        /// SampleName
        /// </summary>
        [Column("SampleName")]
        public string SampleName { get; set; }

        /// <summary>
        /// PropertyName
        /// </summary>
        [Column("PropertyName")]
        public string PropertyName { get; set; }
    }
}