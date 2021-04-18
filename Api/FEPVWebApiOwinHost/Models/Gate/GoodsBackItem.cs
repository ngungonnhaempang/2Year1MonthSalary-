using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models.Gate
{
    [Table("GoodsBackItem")]
    public class GoodsBackItem
    {
        /// <summary>
        /// 单据号
        /// </summary>
        [Required, Key, Column(Order = 0)]
        public Guid ItemID { get; set; }

        /// <summary>
        ///  物品出厂单据号
        /// </summary>
        public string VoucherID { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int GoodsAmount { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 进厂时间
        /// </summary>
        public DateTime? InTime { get; set; }

        /// <summary>
        /// 确认完成时间
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
    }
}
