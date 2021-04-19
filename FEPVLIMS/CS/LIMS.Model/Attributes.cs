using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{
    [Serializable]
    [Table("Attribute")]
    public class Attributes:ORM
    {
        public Attributes()
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
        [Column("PropertyName",true)]
        public string PropertyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Unit")]
        public string Unit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Description_EN")]
        public string Description_EN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Description_TW")]
        public string Description_TW { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Description_CN")]
        public string Description_CN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Description_VN")]
        public string Description_VN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Prec")]
        public int Prec { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("OrderBy")]
        public int OrderBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Remark")]
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("EnRemark")]
        public string EnRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("JPNRemark")]
        public string JPNRemark { get; set; }
    }
}
