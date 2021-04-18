using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models.Gate
{
    [Table("PtaEgTruck")]
    public class PtaEgTruck
    {        
        /// <summary>
        /// 单据号
        /// </summary>
        [Required, Key, Column(Order = 0)]
        public string VoucherID { get; set; }

        /// <summary>
        /// 车号
        /// </summary>
        public string VehicleNO { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { get; set; }

        /// <summary>
        /// 厂商
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// 期望进厂日期
        /// </summary>
        public DateTime ExpectIn { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime ValidatePeriod { get; set; }

        /// <summary>
        /// 进厂联系人
        /// </summary>
        public string LinkMan { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }

        /// <summary>
        /// Pta/Eg
        /// </summary>
        public string PtaEg { get; set; }

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
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// DeliveryCompany
        /// </summary>
        public string DeliveryCompany { get; set; }

        public virtual List<PtaEgItem> PtaEgItems { get; set; }

        /// <summary>
        /// Pta/Eg描述
        /// </summary>
        [NotMapped]
        public string PtaEgRemark { get; set; }

        /// <summary>
        /// 状态(说明)
        /// </summary>
        [NotMapped]
        public string StatusRemark { get; set; }
    }
}
