using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models.HRMS
{
    [Table("FEPV_Employees_Data")]
    public class FEPV_Employees_Data
    {
        [Required, Key, Column(Order = 0)]
        public string EmployeeID { get; set; }

        public string EmployeeName { get; set; }
        public string CostCenter { get; set; }
        public string CardNo { get; set; }
        public bool State { get; set; }
        public string DepartmentName_EN { get; set; }
        public string DepartmentName_CN { get; set; }
        public string Email { get; set; }
        public string PositionName { get; set; }
        public DateTime? JoinDate { get; set; }
        public string Gender { get; set; }
        public string CompanyShortName { get; set; }
        public string CompanyFullName { get; set; }
        public string CompanyCode { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string Workplace { get; set; }
        public int NumberOfUpdate { get; set; }
        public DateTime? Birthday { get; set; }
        public string EmployeeName_Eng { get; set; }
    }
}
