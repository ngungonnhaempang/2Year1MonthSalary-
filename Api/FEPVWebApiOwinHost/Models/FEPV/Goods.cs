using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models.FEPV
{
    
    [Table("Goods")]
    public abstract class Goods : ORM
    {
        /// <summary>
        /// 包号
        /// </summary>
        [Column("BarCode", true)]
        public string BarCode { get; set; }

        /// <summary>
        /// 物料
        /// </summary>
        [Column("MaterialNO")]
        public string MaterialNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("ProdSpec")]
        public string ProdSpec { get; set; }

        /// <summary>
        /// 成本中心
        /// </summary>
        [Column("CenterID")]
        public string CenterID { get; set; }

        /// <summary>
        /// 工厂
        /// </summary>
        [Column("Plant")]
        public string Plant { get; set; }


        [Column("Loc")]
        public string Loc { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        [Column("Batch")]
        public abstract string Batch { get; }

        [Column("Num")]
        public Decimal Num { get; set; }

        /// <summary>
        /// 当道版本
        /// </summary>
        [Column("Version0")]
        public string Version0 { get; set; }

        /// <summary>
        /// 前道版本
        /// </summary>
        [Column("Version")]
        public string Version { get; set; }

        [Column("ProdDate")]
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column("State")]
        public string State { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("UserID")]
        public string UserID { get; set; }


        [Column("Counter")]
        public int Counter { get; set; }


        [Column("Stamp")]
        public DateTime Stamp { get; set; }

        [Column("Checker")]
        public Guid Checker { get; set; }

        [Column("LastVouID")]
        public string LastVouID { get; set; }

        [Column("PrevVouID")]
        public string PrevVouID { get; set; }

        [Column("TableName")]
        public abstract string TableName { get; }

        [Column("Remark")]
        public string Remark { get; set; }

        [Column("LabelDate")]
        public DateTime? LabelDate { get; set; }


        [Column("Store")]
        public string Store { get; set; }


    }
}
