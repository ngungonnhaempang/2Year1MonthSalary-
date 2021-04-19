using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LIMS.Model
{
    [Serializable]
    [Table("Receive")]
    public class DOCReceive:ORM
    {
        public DOCReceive()
        {
            
          
        }
      
        /// <summary>
        /// 单据号
        /// </summary>
        [Column("VoucherID", true)]
        public string VoucherID { get; set; }

        /// <summary>
        /// 进厂计划
        /// </summary>
         [Column("GatePlanID")]
        public string GatePlanID { get; set; }


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



        [Column("State")]
        public string State { get; set; }

      

        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("SheetDate")]
        public DateTime SheetDate { get; set; }


        [Column("Remark")]
        public string Remark { get; set; }


        [Column("GradeVersion")]
        public string GradeVersion { get; set; }


         [Column("TableName")]
        public string TableName { get; set; }

         [Column("UserID")]
         public string UserID { get; set; }

         /// <summary>
         /// 进料单据的资料
         /// </summary>
         [Column("RecfilePath")]
         public string RecfilePath { get; set; }
    }
}
