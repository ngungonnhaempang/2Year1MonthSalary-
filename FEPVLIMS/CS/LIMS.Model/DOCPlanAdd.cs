using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIMS.Model
{
     [Serializable]
    [Table("PlanAdd")]
    public class DOCPlanAdd : ORM
    {
        public DOCPlanAdd()
        {
        }

  
        public int VoucherID { get; set; }

        [Column("SampleName")]
        public string SampleName { get; set; }

        [Column("LOT_NO")]
        public string LOT_NO { get; set; }

        [Column("LINE")]
        public string LINE { get; set; }

        [Column("SheetDate")]
        public DateTime SheetDate { get; set; }

        [Column("ProdDate")]
        public DateTime ProdDate { get; set; }

        [Column("SampleID")]
        public string SampleID { get; set; }

        [Column("State")]
        public string State { get; set; }

        [Column("Purpose")]
        public string Purpose { get; set; }

        [Column("CompanyOfferSample")]
        public string CompanyOfferSample { get; set; }

        [Column("CompanyProduce")]
        public string CompanyProduce { get; set; }

        [Column("CreateType")]
        public int CreateType { get; set; }

        [Column("GradeVersion")]
        public Guid GradeVersion { get; set; }

        [Column("UserID")]
        public string UserID { get; set; }
    }
}