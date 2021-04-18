using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Http;

namespace FEPVWebApiOwinHost.Models
{
    [Table("SpecialTruck")]
    public class SpecialTruck
    {
        [Required, StringLength(20), Key, Column("VoucherID")]
        public string VoucherId { get; set; }

        [Required, StringLength(20)]
        public string VehicleType { get; set; }

        [Required, StringLength(20), Column("VehicleNO")]
        public string VehicleNo { get; set; }

        [StringLength(30), Column("OrderID")]
        public string OrderId { get; set; }

        [Required, StringLength(20)]
        public string Driver { get; set; }

        [Required, StringLength(30)]
        public string Contractor { get; set; }

        [Required, StringLength(50)]
        public string Location { get; set; }

        [Required,StringLength(20)]
        public string Director { get; set; }

        [Required]
        public DateTime ExpectIn { get; set; }

        [Required,StringLength(20)]
        public string LinkMan { get; set; }

        [Required,StringLength(20)]
        public string LinkPhone { get; set; }

        [Required]
        public DateTime ValidatePeriod { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        public DateTime? InTime { get; set; }

        public DateTime? OutTime { get; set; }

        [Required,StringLength(10)]
        public string Status { get; set; }

        [Required]
        public DateTime Stamp { get; set; }

        [Required,StringLength(20),Column("UserID")]
        public string UserId { get; set; }
    }
}
