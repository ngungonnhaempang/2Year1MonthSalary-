using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models
{
    [Table("Contractors")]
    public class Contractor
    {     
        [Required, Key, Column(Order = 1)]
        public string ContractorID { get; set; }     
        public string IdCard { get; set; }
        public string Name { get; set; } 
        public string Mark { get; set; }
        public DateTime? Birthday { get; set; }
        public string Job { get; set; }     
        public string Sex { get; set; }
        public DateTime? AppointmentDate { get; set; }   
        public string Remark { get; set; }    
        public string Status { get; set; }
        public DateTime? TrainDate { get; set; }
        public DateTime? Stamp { get; set; }   
        public string UserId { get; set; }
        [Required, Key, Column(Order = 0)]      
        public string EmployeeID { get; set; }
        public string VoucherID { get; set; }
        public string PreStatus { get; set; }
        public string Region { get; set; }
        public string ReasonReturn { get; set; }
        public DateTime? InsuranceDuration { get; set; }
        public DateTime? SafetyCerDuration { get; set; }
        public int? IsForeign { get; set; }
        public int? IsUpload { get; set; }
        public string Phone { get; set; }
    }
}
