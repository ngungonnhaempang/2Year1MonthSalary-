using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{
   
    [Table("Report_ISO_Result")]
    public class Report_ISO_Result : ORM
    {
        public string VoucherID { set; get; }
        public string SampleName { set; get; }
        public string LOT_NO { set; get; }
        public string Line { set; get; }
        public string ColorLabel { set; get; }
        public string VoucherNO { set; get; }
        
        public DateTime CreateDate { set; get; }
        public DateTime BeginDate { set; get; }
        public DateTime EndDate { set; get; }
        public DateTime Stamp { set; get; }
        public string Reason { set; get; }
        public string Solution { set; get; }
        public string Prevention { set; get; }
        public string CreateBy { set; get; }
        public string Remark { set; get; }

        public string Vendor { get; set; }
        public string Range { get; set; }


    }
 
}
