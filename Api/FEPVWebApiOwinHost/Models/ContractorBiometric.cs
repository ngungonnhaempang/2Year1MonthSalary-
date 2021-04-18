using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models
{
    [Table("ContractorBiometric")]
    public class ContractorBiometric
    {
        [Required, Key, Column(Order = 0)]
        public string EmployeeID { get; set; }
        public string CardNo { get; set; }
        public string FaceTmp { get; set; }
        
    }
}
