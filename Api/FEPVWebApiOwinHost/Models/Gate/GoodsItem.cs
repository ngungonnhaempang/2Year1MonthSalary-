using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace FEPVWebApiOwinHost.Models.Gate
{
    [Table("GoodsItem")]
    public class GoodsItem
    {
        /// <summary>
        /// 单据号
        /// </summary>
        [Required, Key, Column(Order = 0)]
        public Guid ID { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal GoodsAmount { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        ///  物品出厂单据号
        /// </summary>
        public string VoucherID { get; set; }


        [NotMapped]
        /// <summary>
        /// 物品描述
        /// </summary>
        public string UnitRemark { get; set; }

        [NotMapped]
        /// <summary>
        /// 物品描述
        /// </summary>
        public string ReasonRemark { get; set; }

    }
}
