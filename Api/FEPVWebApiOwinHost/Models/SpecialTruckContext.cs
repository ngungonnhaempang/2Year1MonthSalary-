using System.Data.Entity;

namespace FEPVWebApiOwinHost.Models
{
    public class SpecialTruckContext : DbContext
    {
        public SpecialTruckContext()
            : base("name=Beling")
        {   
        }

        public DbSet<SpecialTruck> SpecialTrucks { get; set; }
    }
}
