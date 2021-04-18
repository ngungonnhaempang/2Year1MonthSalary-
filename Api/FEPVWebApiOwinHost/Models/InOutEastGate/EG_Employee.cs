using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models.InOutEastGate
{
    [Table("EG_Employee")]
    public class EG_Employee
    {
        [Required, Key, Column(Order = 0)]
        public string EmployeeID { get; set; }

        [Required, Key, Column(Order = 1)]
        public string VoucherID { get; set; }
        public string EmpID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string PreStatus { get; set; }
        public string CardNO { get; set; }
        public DateTime? ModifyDate { get; set; }

        public EG_Voucher EastGateVoucher { get; set; }

    }
}
