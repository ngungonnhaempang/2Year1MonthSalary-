using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{

    /// <summary>
    /// 物料的类型，原辅料 成品 工序 工程材料
    /// </summary>
    [Serializable]
    [Table("Category")]
   public class Category:ORM
    {
        public Category()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        [Column("TypeID",true)]
        public int TypeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Lang",true)]
        public string Lang { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("TypeName")]
        public string TypeName { get; set; }
    }
}
