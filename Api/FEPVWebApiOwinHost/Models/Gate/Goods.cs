using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models.Gate
{
    [Table("Goods")]
    public class Goods
    {
        /// <summary>
        /// 单据号
        /// </summary>
        [Required, Key, Column(Order = 0)]
        public string VoucherID { get; set; }

        /// <summary>
        /// 物品类型
        /// </summary>
        public string GoodsType { get; set; }

        /// <summary>
        /// 是否回厂
        /// </summary>
        public bool IsBack { get; set; }

        /// <summary>
        /// 期望回厂日期
        /// </summary>
        public DateTime? ExpectBack { get; set; }

        /// <summary>
        /// 携出人
        /// </summary>
        public string TakeOut { get; set; }

        /// <summary>
        /// 厂商
        /// </summary>
        public string TakeCompany { get; set; }

        /// <summary>
        /// 车号
        /// </summary>
        public string VehicleNO { get; set; }

        /// <summary>
        /// 预计出厂日期
        /// </summary>
        public DateTime? ExpectOut { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 出厂时间
        /// </summary>
        public DateTime? OutTime { get; set; }

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
        /// 图片名称
        /// </summary>
        public string FileNames { get; set; }

     
        public virtual List<GoodsItem> GoodsItems { get; set; }
        public virtual List<GoodsBackItem> GoodsBackItems { get; set; }

         [NotMapped]
        /// <summary>
        /// 物品描述
        /// </summary>
        public string GoodsTypeRemark { get; set; }

         [NotMapped]
         public virtual string StatusRemark { get; set; }
    }
}
