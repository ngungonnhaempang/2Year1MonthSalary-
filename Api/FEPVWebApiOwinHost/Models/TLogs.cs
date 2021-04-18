using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models
{

       [Table("Logs")]
    public class TLogs
    {
           [Required, Key, Column(Order = 0)]
           public string LogTitile { get; set; }

           [Required, Key, Column(Order = 1)]
           public DateTime Stamp { get; set; }

           [Required, Key, Column(Order = 2)]
           public string UserId { get; set; }


           public string Remark { get; set; }

           public string Code { get; set; }

          
           
         public string DoDate { get; set; }

         public string UploadFile { get; set; }


         public string ShaoDate { get; set; }

         public string SaveDate { get; set; }

    }
}
