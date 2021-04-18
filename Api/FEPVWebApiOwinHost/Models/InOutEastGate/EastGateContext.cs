using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models.InOutEastGate
{
    public class EastGateContext : DbContext
    {
        public EastGateContext()
           : base("name=Beling")
        { }
        public DbSet<EG_Voucher> EastGateVouchers { get; set; }
        public DbSet<EG_Employee> EastGateEmployees { get; set; }
    }
}
