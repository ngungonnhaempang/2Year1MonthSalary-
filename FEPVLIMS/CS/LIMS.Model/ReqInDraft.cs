using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{
    [Serializable]
    [Table("ReqInDraft")]
    public class ReqInDraft:ORM
    {
        public ReqInDraft()
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
        [Column("VoucherID", true)]
        public string VoucherID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Stamp")]
        public DateTime Stamp { get; set; }
    }
}
