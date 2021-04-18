
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models.Meal
{
    [Table("Foreign_Employee_CardHistory")]
    public class Foreign_Employee_CardHistory
    {

       
        public string CardID { get; set; }

        [Required, Key, Column(Order = 0)]
        public string EmployeeID { get; set; }
       
        public string EmployeeName { get; set; }
        
        public string Department_EN { get; set; }
       
        public string Department_CN { get; set; }
       
        public string DepartmentID { get; set; }
        
        public string Area { get; set; }
        
        public string CardNumber { get; set; }
        
        public DateTime? LastUpdate { get; set; }

    }
    public class Foreign_EmployeeHandler
    {
        public List<Foreign_Employee_CardHistory> listForeign_Employee { get; set; }
    }

}
