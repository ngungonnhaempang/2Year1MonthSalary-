using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{
    [Serializable]
    [Table("Draft")]
  public  class Draft:ORM
    {
        public Draft()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("DraftID", true)]
        public string DraftID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Owner")]
        public string Owner { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("CreateDate")]
        public string CreateDate { get; set; }
    }
}
