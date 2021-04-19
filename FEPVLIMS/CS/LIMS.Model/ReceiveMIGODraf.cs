using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LIMS.Model
{
    [Serializable]
    [Table("ReceiveMIGODraf")]
    public class ReceiveMIGODraf : ORM
    {
        [Column("VoucherID", true)]
        public string VoucherID { get; set; }


        [Column("RVoucherID", true)]
        public string RVoucherID { get; set; }

        [Column("UserID")]
        public string UserId { get; set; }




        [Column("Stamp")]
        public DateTime Stamp { get; set; }




    }
}
