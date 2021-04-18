using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models.Gate
{
    [Table("JointTruck")]
    public class JointTruck
    {        
        /// <summary>
        /// 单据号
        /// </summary>
        [Required, Key, Column(Order = 0)]
        public string VoucherID { get; set; }

        /// <summary>
        /// 发货通知单号
        /// </summary>
        public string ShippingOrder { get; set; }

        /// <summary>
        /// 运输公司
        /// </summary>
        public string TransferCompany { get; set; }

        /// <summary>
        /// 车号
        /// </summary>
        public string VehicleNO { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string MaterielType { get; set; }

        /// <summary>
        /// 司机
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// 司机电话
        /// </summary>
        public string DriverPhone { get; set; }

        /// <summary>
        /// 期望到厂时间
        /// </summary>
        public DateTime ExpectIn { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string VehicleShape { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime ValidatePeriod { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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
        /// 实际到厂时间
        /// </summary>
        public DateTime? ComeTime { get; set; }

        /// <summary>
        /// 进厂时间
        /// </summary>
        public DateTime? InTime { get; set; }

        /// <summary>
        /// 出厂时间
        /// </summary>
        public DateTime? OutTime { get; set; }

        /// <summary>
        /// 装货完成时间
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

        public string CompanyCode { get; set; }

        public string ContainerNO { get; set; }
        /// <summary>
        /// DeliveryCompany
        /// </summary>
        public string DeliveryCompany { get; set; }
        public virtual List<JointTruckItem> JointTruckItems { get; set; }


   
        /// <summary>
        /// 运输公司描述
        /// </summary>
        [NotMapped]
        public string TransferCompanyRemark { get; set; }

        /// <summary>
        /// 物料类型描述
        /// </summary>
        [NotMapped]
        public string MaterielTypeRemark { get; set; }

        /// <summary>
        /// 车型描述
        /// </summary>
        [NotMapped]
        public string VehicleShapeRemark { get; set; }

        /// <summary>
        /// 状态(说明)
        /// </summary>
        [NotMapped]
        public string StatusRemark { get; set; }
    }
}
