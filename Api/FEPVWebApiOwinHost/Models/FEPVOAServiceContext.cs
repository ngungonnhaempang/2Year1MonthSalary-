using FEPVWebApiOwinHost.Models.Meal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models
{
   public class FEPVOAServiceContext :DbContext
    {
        public FEPVOAServiceContext()
            : base("name=FEPVOA")
        { }
        public DbSet<Foreign_Employee_CardHistory> Foreign_Employee { get; set; }
    }
}
