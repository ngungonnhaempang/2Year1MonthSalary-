using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{
    [Serializable]
    [Table("WorkCenter")]
  public  class WorkCenter:ORM
    {
        public WorkCenter()
        {
           
        }
        /// <summary>
        /// 
        /// </summary>
        [Column("LabID", true)]
        public int LabID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Lang", true)]
        public string Lang { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("LabName")]
        public string LabName { get; set; }

        
    }
}
