using LIMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Service
{
    public class MyContext: DbContext
    {
        public MyContext() : base("name=University") { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Registration> Registrations { get; set; }
    }
}
