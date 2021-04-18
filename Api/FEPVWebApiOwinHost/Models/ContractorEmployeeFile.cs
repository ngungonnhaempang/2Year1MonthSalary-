using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FEPVWebApiOwinHost.Models
{
    [Table("ContractorEmployeeFile")]
    public class ContractorEmployeeFile
    {
        [Required, Key, Column(Order = 0)]
        public string VoucherID { get; set; }
        public string FileName { get; set; }
        public DateTime? Stamp { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
