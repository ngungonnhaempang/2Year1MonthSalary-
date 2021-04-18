using System;

namespace FEPVWebApiOwinHost.Models
{
    public class SpecialTruckDto
    {
        public string VoucherId { get; set; }
        public string VehicleType { get; set; }
        public string VehicleNo { get; set; }
        public string OrderId { get; set; }
        public string Driver { get; set; }
        public string Contractor { get; set; }
        public string Location { get; set; }
        public string Director { get; set; }
        public DateTime ExpectIn { get; set; }
        public string LinkMan { get; set; }
        public string LinkPhone { get; set; }
        public DateTime ValidatePeriod { get; set; }
        public string Remark { get; set; }
        public string UserId { get; set; }
    }
}
