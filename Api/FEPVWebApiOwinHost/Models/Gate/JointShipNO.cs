using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models.Gate
{
    [Table("JointShipNO")]
    public class JointShipNO
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
    }
}
