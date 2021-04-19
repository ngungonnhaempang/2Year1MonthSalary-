using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{
    [Serializable]
    [Table("ChartUCL2LCL")]
    public class ChartUCL2LCL:ORM
    {
        public ChartUCL2LCL()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        [Column("SampleName", true)]
        public string SampleName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("LOT_NO", true)]
        public string LOT_NO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("LINE",true)]
        public string LINE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("PropertyName",true)]
        public string PropertyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("CreateDate")]
        public string CreateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("UCL")]
        public decimal UCL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("LCL")]
        public decimal LCL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("LastModify")]
        public string LastModify { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Remark")]
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        
    }
}
