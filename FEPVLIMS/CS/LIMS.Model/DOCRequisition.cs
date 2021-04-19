using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LIMS.Model
{
    [Serializable]
    [Table("Requisition")]
    public partial class DOCRequisition: ORM
    {
        public DOCRequisition()
        {
            VoucherID = string.Empty;
            SampleName = string.Empty;
            LOT_NO = string.Empty;
            Purpose = string.Empty;
            SampleFrom = string.Empty;
            RequireDate = DateTime.Now;
            SendDate = DateTime.Now;
            GetDate = DateTime.Now;
            State = string.Empty;
            Remark = string.Empty;
            POY_LOT_NO = string.Empty;
            IsOtherCompany = false;
        }

        [Column("VoucherID", true)]
        public string VoucherID { get; set; }

        [Column("SampleName")]
        public string SampleName { get; set; }

        [Column("LOT_NO")]
        public string LOT_NO { get; set; }

        [Column("Purpose")]
        public string Purpose { get; set; }

        [Column("SampleFrom")]
        public string SampleFrom { get; set; }

        [Column("RequireDate")]
        public DateTime RequireDate { get; set; }

        [Column("SendDate")]
        public DateTime SendDate { get; set; }

        [Column("GetDate")]
        public DateTime GetDate { get; set; }

        [Column("State")]
        public string State { get; set; }

        [Column("Remark")]
        public string Remark { get; set; }

        [Column("POY_LOT_NO")]
        public string POY_LOT_NO { get; set; }

        [Column("IsOtherCompany")]
        public bool IsOtherCompany { get; set; }
         [Column("GradeVersion")]
        public Guid GradeVersion { get; set; } 
        
    }
}
