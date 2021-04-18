using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FEPVWebApiOwinHost.Models.Gate
{
   
    [Table("AC_Ponderation")]
    public class AC_Ponderation 
    {
        [Required, Key, Column(Order = 0)]
        public string PonderationID { get; set; }
      
        public string ContainerNO { get; set; }
      
        public string PermissionID { get; set; }
       
        public DateTime? WeightOneTime { get; set; }
     
        public decimal? WeightOne { get; set; }
      
        public DateTime? WeightTwoTime { get; set; }
       
        public decimal? WeightTwo { get; set; }

        public DateTime? ContainerOutTime { get; set; }
       
        public DateTime Stamp { get; set; }

        /// <summary>
        /// 柜号的状态 X  是否出去
        /// </summary>
       
        public string State { get; set; }



       
        public string UserID { get; set; }

        /// <summary>
        /// 空柜出厂备注
        /// </summary>
       
        public string CRemark { get; set; }

        /// <summary>
        /// 一磅车号 过磅车辆 ，空车过磅车号
        /// </summary>
        
        public string OutTruckNO { get; set; }

    }
}
