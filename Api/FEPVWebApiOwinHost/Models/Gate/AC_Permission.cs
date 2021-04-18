using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models.Gate
{
 
    [Table("AC_Permission")]
    public class AC_Permission 
    {

        [Required, Key, Column(Order = 0)]
        public string PermissionID { get; set; }



       
        public string TruckNO { get; set; }


       
        public string VehicleShape { get; set; }

       
        public string MaterialType { get; set; }

       
        public DateTime? CheckInTime { get; set; }

       
        public string UserId { get; set; }

        /// <summary>
        /// 车号的状态 X  是否出去
        /// </summary>
       
        public string State { get; set; }
       
        public DateTime? Stamp { get; set; }

       
        public string Driver { get; set; }
       
        public string DriverPhone { get; set; }
        
        public string TransferCompany { get; set; }

        
        public string RemarkContainerNO { get; set; }
       
        public string Remark { get; set; }
    }
}
