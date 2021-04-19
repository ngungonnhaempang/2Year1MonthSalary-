using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LIMS.Model
{

  [Table("IEMS_Export")]
    public class IEMS_Export : ORM
    {
      /// <summary>
      /// 船期编号
      /// </summary>
     [Column("AESKD", true)]
      public string AESKD { get; set; }

       [Column("Stamp")]
      public DateTime Stamp { get; set; }

       [Column("InvoiceNO")]
      public string InvoiceNO { get; set; }

         [Column("InvoiceDate")]
      public DateTime InvoiceDate { get; set; }
       [Column("UserId")]
      public string UserId { get; set; }

        [Column("FinishedDate")]
      public DateTime FinishedDate { get; set; }

     [Column("SaleType")]
      public string SaleType { get; set; }

        [Column("Remark")]
      public string Remark { get; set; }
    }
}
