using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models.Gate
{
    [Table("UnJointTruck")]
    public class UnJointTruck
    {
        /// <summary>
        /// 单据号
        /// </summary>
        [Required, Key, Column(Order = 0)]
        public string VoucherID { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// 车号    IEMS ContainerNo  IEMS规则使用 进厂前柜号为车号， 后面可以修改 填写车号或者柜号
        /// </summary>
        public string VehicleNO { get; set; }

        /// <summary>
        /// 送/提货厂商  IEMS CompanyCode
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// 订单号   IEMS  BLNo 提单号
        /// </summary>
        public string OrderNO { get; set; }

        /// <summary>
        /// 物料类别  IEMS TradeOfMaterial
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// 期望到厂日期
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
        /// 柜号  IEMS ContType + '-' + ContainerNo;
        /// </summary>
        public string StorageNO { get; set; }

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
        /// 确认装卸货完成时间
        /// </summary>
        public DateTime? Complete { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime? Stamp { get; set; }

        /// <summary>
        /// 创建者工号
        /// </summary>
        public string UserID { get; set; }


        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///IEMS规则使用 出厂是的柜号
        /// </summary>
        public string Container { get; set; }


        /// <summary>
        /// 进口批号
        /// </summary>
        public string ImportBatch { get; set; }

        /// <summary>
        /// P/O
        /// </summary>
        public string PO { get; set; }

        /// <summary>
        /// Temporary field
        /// </summary>
        public string Temp1 { get; set; }
        /// <summary>
        /// DeliveryCompany
        /// </summary>
        public string DeliveryCompany { get; set; }
        /// <summary>
        /// TransportCompany
        /// </summary>
        public string TransportCompany { get; set; }
        /// <summary>
        /// 车辆类型描述
        /// </summary>
        [NotMapped]
        public string VehicleTypeRemark { get; set; }

        /// <summary>
        /// 状态(说明)
        /// </summary>
        [NotMapped]
        public string StatusRemark { get; set; }
    }
}
