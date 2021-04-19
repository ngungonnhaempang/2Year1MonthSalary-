using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIMS.Model
{
    [Serializable]
    [Table("ReceiveWRGG")]
    public class ReceiveWRGG : ORM
    {
         [Column("VoucherID", true)]
        public string VoucherID { get; set; }

         [Column("EBELN")]
         public string EBELN { get; set; }

		[Column("EBELP")]
		public string EBELP { get; set; }

		[Column("SampleName")]
         public string SampleName { get; set; }
		[Column("LOT_NO")]
		public string LOT_NO { get; set; }

		[Column("ImportBatch")]
         public string ImportBatch { get; set; }

         [Column("ReceiveDate")]
         public DateTime ReceiveDate { get; set; }

         [Column("Stamp")]
         public DateTime Stamp { get; set; }

         [Column("UserId")]
         public string UserId { get; set; }


         [Column("Status")]
         public string Status { get; set; }

          [Column("ReceiveNum")]
         public decimal ReceiveNum { get; set; }


         [Column("RecfilePath")]
         public string RecfilePath { get; set; }		
	}

  
}
