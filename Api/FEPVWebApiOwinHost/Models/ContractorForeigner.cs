using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models
{
    [Table("ContractorForeigner")]
    public class ContractorForeigner
    {
        [Required, Key, Column(Order = 0)]
        public string EmployeeID { get; set; }
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
    }
}
