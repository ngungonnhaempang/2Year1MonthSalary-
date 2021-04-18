using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace FEPVWebApiOwinHost.Models
{
    public class ContractorDto
    {
        public string IdCard { get; set; }        
        public string ContractorName { get; set; }
        public string ContractorID { get; set; }        
        public string Name { get; set; }        
        public string Mark { get; set; }
        public string Remark { get; set; }    
        public string UserId { get; set; }        
        public bool IsUpdate { get; set; }
        public DateTime? AppointmentDate { get; set; }    
        public DateTime? TrainDate { get; set; }
        public string Status { get; set; }
        public DateTime? Birthday { get; set; }       
        public string Job { get; set; }     
        public string Sex { get; set; }
        public string Region { get; set; }
        public string PreStatus { get; set; }
        public string VoucherID { get; set; }
        public string ReasonReturn { get; set; }
        public string EmployeeID { get; set; }
        public DateTime? InsuranceDuration { get; set; }
        public DateTime? SafetyCerDuration { get; set; }
        public int? IsForeign { get; set; }
        public string Phone { get; set; }

        /* Foreigner */
        public DateTime? PassPort_Expiry { get; set; }
        public string PassPort_Nationality { get; set; }
        public string WorkPermit_No { get; set; }
        public DateTime? WorkPermit_Start { get; set; }
        public DateTime? WorkPermit_End { get; set; }
        public string CategoryCard { get; set; }
        public string Card_Type { get; set; }
        public string Card_No { get; set; }
        public DateTime? Card_Start { get; set; }
        public DateTime? Card_End { get; set; }

        /*ContractorEmployeeFile*/
        public string FileName { get; set; }
    }
    public class EmployeeList
    {
        public List<ContractorDto> _employeeList { get; set; }
    }
}
