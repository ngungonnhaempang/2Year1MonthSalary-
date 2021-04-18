using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models.Gate
{
    [Table("JointTruckItem")]
    public class JointTruckItem
    {
        /// <summary>
        /// 单据号
        /// </summary>
        [Required, Key, Column(Order = 0)]
        public string VoucherID { get; set; }

        /// <summary>
        /// 发货通知单号
        /// </summary>
        [Required, Key, Column(Order = 1)]
        public string ShippingOrder { get; set; }

        /// <summary>
        /// D’: 发货 ‘R’: 退货    ，VH 内销 、外销（袋装，散装）
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// 物料品品名規格
        /// </summary>
        public string MaterialSpc { get; set; }

        /// <summary>
        /// 预发量
        /// </summary>
        public decimal PRquan { get; set; }

        //实际发货量
        public decimal Requan { get; set; }

        // (KG)(KG)(KG)(
        public string Unit { get; set; }

        /// <summary>
        /// 空白 :尚未打印 ,’ Y’ :已
        /// 过磅时判断 是否过账，计算量比对磅差
        /// </summary>
        public string Isprn { get; set; }

        public DateTime? Stamp { get; set; }

    }
}
