using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models.Gate
{
    [Table("PtaEgItem")]
    public class PtaEgItem
    {
        /// <summary>
        ///  从表单号
        /// </summary>
        [Required, Key, Column(Order = 0)]
        public Guid ItemID { get; set; }

        /// <summary>
        /// 主表单号
        /// </summary>
        public string VoucherID { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public string CupboardNO { get; set; }

        /// <summary>
        /// 卸货地点
        /// </summary>
        public string Discharge { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 参考重量
        /// </summary>
        public decimal? ReferWeight { get; set; }

        /// <summary>
        /// 一次过磅重量
        /// </summary>
        public decimal? FirstWeight { get; set; }

        /// <summary>
        /// 一次过磅时间
        /// </summary>
        public DateTime? FirstTime { get; set; }

        /// <summary>
        /// 二次过磅重量
        /// </summary>
        public decimal? SecondWeight { get; set; }

        /// <summary>
        /// 二次过磅时间
        /// </summary>
        public DateTime? SecondTime { get; set; }

        /// <summary>
        /// 进厂时间
        /// </summary>
        public DateTime? InTime { get; set; }

        /// <summary>
        /// 出厂时间
        /// </summary>
        public DateTime? OutTime { get; set; }

        /// <summary>
        /// 现场确认完成时间
        /// </summary>
        public DateTime? Complete { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Stamp { get; set; }

        /// <summary>
        /// 创建者工号
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 异常原因
        /// </summary>
        public string Reason { get; set; }

      

        /// <summary>
        /// 状态(说明)
        /// </summary>
        [NotMapped]
        public string StatusRemark { get; set; }
    }
}
