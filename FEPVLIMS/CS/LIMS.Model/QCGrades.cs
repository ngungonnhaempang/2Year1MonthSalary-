using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Shawoo.Core;


namespace LIMS.Model
{


    [Serializable]
    [Table("GradeMaterial")]
    public class GradeMaterial : ORM
    {

        [Column("ID",true)]
        public string ID { get; set; }

         [Column("SampleName")]
        public string SampleName { get; set; }

        [Column("LOT_NO")]
        public string LOT_NO { get; set; }

        [Column("Version")]
        public int Version { get; set; }

        //特代 00  
        [Column("Grades")]
        public string Grades { get; set; }
       

        //状态 X is delete ,Create N ,check F ,publish S
        [Column("Status")]
        public string Status { get; set; }


        //版本编号  ISO
        [Column("VersionSpc")]
        public string VersionSpc { get; set; }


        /// <summary>
        /// modify time
        /// </summary>
        [Column("Stamp")]
        public DateTime Stamp { get; set; }

        //create userid
        [Column("UserID")]
        public string UserID { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        [Column("Remark")]
        public string Remark { get; set; }
    }

    [Serializable]
    [Table("Grades")]
    public class QCGrades : ORM
    {
        public QCGrades()
        {
        }

        [Column("ID",true)]
        public string ID { get; set; }


       

        [Column("PropertyName", true)]
        public string PropertyName { get; set; }


        //等级规格 A, AA
        [Column("Grade")]
        public string Grade { get; set; }
       

        //是否判等

        [Column("Enable")]
        public bool Enable { get; set; }


        [Column("MaxValue")]
        public decimal? MaxValue { get; set; }

        [Column("MinValue")]
        public decimal? MinValue { get; set; }

        //精度
        [Column("Prec")]
        public int Prec { get; set; }

        //规格描述
        [Column("ValueSpec")]
        public string ValueSpec { get; set; }

       
        //create 日期
        [Column("CreateDate")]
        public DateTime CreateDate { get; set; }

          
        //生效日期
        [Column("ValidDate")]
        public DateTime ValidDate { get; set; }


        //结束生效日期
        [Column("ValidTODate")]
        public DateTime? ValidTODate { get; set; }

        /// <summary>
        /// modify time
        /// </summary>
          [Column("Stamp")]
        public DateTime Stamp { get; set; }

        //create userid
         [Column("UserID")]
        public string UserID { get; set; }


         //
         [Column("Remark")]
         public string Remark { get; set; }
    }
}
