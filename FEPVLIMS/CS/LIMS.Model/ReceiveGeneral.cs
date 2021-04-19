using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LIMS.Model
{
    [Serializable]
    [Table("ReceiveGeneral")]
    public class ReceiveGeneral : ORM
    {
        /// <summary>
        /// 单据号
        /// </summary>
        [Column("VoucherID", true)]
        public string VoucherID { get; set; }
        /// <summary>
        /// 样品名称
        /// </summary>
        [Column("SampleName")]
        public string SampleName { get; set; }
        /// <summary>
        /// 物料号
        /// </summary>
        [Column("LOT_NO")]
        public string LOT_NO { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        [Column("Grade")]
        public string Grade { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("State")]
        public string State { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("SheetDate")]
        public DateTime SheetDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("Remark")]
        public string Remark { get; set; }
        /// <summary>
        /// 等级版本
        /// </summary>
        [Column("GradeVersion")]
        public string GradeVersion { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("UserID")]
        public string UserID { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        [Column("EBELN")]
        public string EBELN { get; set; }
        /// <summary>
        /// 项次
        /// </summary>
        [Column("EBELP")]
        public string EBELP { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        [Column("Vendor")]
        public string Vendor { get; set; }
        /// <summary>
        /// 样品数量
        /// </summary>
        [Column("SampleQuantity")]
        public decimal SampleQuantity { get; set; }
      
        /// <summary>
        /// 物料凭证号
        /// </summary>
        [Column("GR_NO")]
        public string GR_NO { get; set; }
        /// <summary>
        /// 收货数量
        /// </summary>
        [Column("Quatity")]
        public decimal Quatity { get; set; }

        [Column("Stamp")]
        public DateTime Stamp { get; set; }
        /// <summary>
        /// 收货单位
        /// </summary>
        [Column("GR_Unit")]
        public string GR_Unit { get; set; }
        /// <summary>
        /// 进料单据的资料
        /// </summary>
        [Column("RecfilePath")]
        public string RecfilePath { get; set; }
        /// <summary>
        /// Isaac
        /// </summary>
        [Column("DateOfSample")]
        public DateTime DateOfSample { get; set; }
    }
}
