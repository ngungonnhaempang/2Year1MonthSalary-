using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models.Gate
{
    [Table("Guest")]
    public class Guest
    {
        /// <summary>
        /// 单据号
        /// </summary>
        [Required, Key, Column(Order = 0)]
        public string VoucherID { get; set; }

        /// <summary>
        /// 事由
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否需受访确认
        /// </summary>
        public bool IsNeedConfirm { get; set; }

        /// <summary>
        /// 访客类型 一般访客/随车人员
        /// </summary>
        public string GuestType { get; set; }

        /// <summary>
        /// 造访区域（行政楼 / 厂区）
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 受访人
        /// </summary>
        public string Respondent { get; set; }

        /// <summary>
        /// 分机号
        /// </summary>
        public string ExtNO { get; set; }

        /// <summary>
        /// 访客单位
        /// </summary>
        public string Enterprise { get; set; }

        /// <summary>
        /// 期望到厂日期
        /// </summary>
        public DateTime ExpectIn { get; set; }

        /// <summary>
        /// 进厂时间
        /// </summary>
        public DateTime? InTime { get; set; }

        /// <summary>
        /// 出厂时间
        /// </summary>
        public DateTime? OutTime { get; set; }

        /// <summary>
        /// 确认受访完成时间
        /// </summary>
        public DateTime? Complete { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Stamp { get; set; }

        /// <summary>
        /// 创建者工号
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }


        public DateTime? ExpectOutTime { get; set; }

        public string VehicleNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 受访人部门
        /// </summary>
        public string DepartmentSpc { get; set; }

        /// <summary>
        /// 访客类型(说明)
        /// </summary>
        [NotMapped]
        public string GuestTypeRemark { get; set; }

        /// <summary>
        /// 访问区域(说明)
        /// </summary>
        [NotMapped]
        public string RegionRemark { get; set; }

        /// <summary>
        /// 状态(说明)
        /// </summary>
        [NotMapped]
        public string StatusRemark { get; set; }

        /// <summary>
        /// 详细访客信息
        /// </summary>   
        public virtual List<GuestItem> GuestItems { get; set; }


     

    }
}
