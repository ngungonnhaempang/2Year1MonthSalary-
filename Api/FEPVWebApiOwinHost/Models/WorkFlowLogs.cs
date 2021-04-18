using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models
{
    [Table("WorkFlowLogs")]
    public class WorkFlowLogs
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required, Key, Column(Order = 0)]
        public Guid ID { get; set; }
        /// <summary>
        /// 进程ID
        /// </summary>
        public Guid ProcessInstanceId { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 签核时间
        /// </summary>
        public DateTime Stamp { get; set; }
        /// <summary>
        /// 签核人
        /// </summary>
        public string Checker { get; set; }
        /// <summary>
        /// 签核人姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string WorkFlow { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
