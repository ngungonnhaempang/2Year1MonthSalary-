using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models.Meal
{
   public class EmployeeHandle
    {
        public List<EmployeeHandleDetail> empDetail { get; set; }
    }
    
    [Table("EmployeeHandle")]
   public class EmployeeHandleDetail : ORM
    {
        [Column("EmployeeID",true)]
        public string EmployeeID_Old { get; set; }

        [Column("Date",true)]
        public DateTime DateSwipe { get; set; }
        [Column("Type",true)]
        public string Type { get; set; }
        [Column("Reason")]
        public string Reason { get; set; }
        [Column("Stamp")]
        public DateTime Stamp { get; set; }

      

      
    }
}
