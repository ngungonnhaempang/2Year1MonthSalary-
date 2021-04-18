using FEPVWebApiOwinHost.Models.Gate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models
{
    public class GateContext : DbContext
    {
        public GateContext()
            : base("name=Beling")
        { }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<GuestItem> GuestItems { get; set; }

        public DbSet<Goods> Goodss { get; set; }

        public DbSet<GoodsItem> GoodsItems { get; set; }

        public DbSet<GoodsBackItem> GoodsBackItems { get; set; }

        public DbSet<UnJointTruck> UnJointTrucks { get; set; }

        public DbSet<PtaEgTruck> PtaEgTrucks { get; set; }

     


        public DbSet<PtaEgItem> PtaEgItems { get; set; }

        public DbSet<JointTruck> JointTrucks { get; set; }


        public DbSet<JointShipNO> JointShipNOs { get; set; }
        public DbSet<JointTruckItem> JointTruckItems { get; set; }

        public DbSet<BL_Request> BL_Requests { get; set; }

        public DbSet<WorkFlowLogs> WorkFlowLogS { get; set; }
        public DbSet<AC_Permission> aC_Permissions { get; set; }
        public DbSet<AC_Ponderation> aC_Ponderations { get; set; }
      
    }
}
