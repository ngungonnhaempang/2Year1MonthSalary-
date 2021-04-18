using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models.InOutEastGate
{
    [Table("EG_Voucher")]
    public class EG_Voucher
    {
        [Required, Key, Column(Order = 0)]
        public string VoucherID { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RegisterReason { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CostCenter { get; set; }
        public string Creator { get; set; }
        public string InternalNumber { get; set; }
        public virtual ICollection<EG_Employee> EmployeeList { set; get; }

    }
}
