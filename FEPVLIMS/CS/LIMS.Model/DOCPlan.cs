using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LIMS.Model
{
    [Serializable]
    [Table("Plans")]
    public partial class DOCPlan: ORM
    {
       
        [Column("VoucherID", true)]
        public string VoucherID { get; set; }

       
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

        /// <summary>
        /// 样品编号
        /// </summary>
        [Column("SampleID")]
        public string SampleID { get; set; }

        [Column("State")]
        public string State { get; set; }

        [Column("Grade")]
        public string Grade { get; set; }

        [Column("Remark")]
        public string Remark { get; set; }

        [Column("CreateType")]
        public int createType { get; set; }
         [Column("HasChart")]
        public string HasChart { get; set; }

        [Column("CompanyOfferSample")]
        public string companyOffer { get; set; }

        [Column("CompanyProduce")]
        public string companyProd { get; set; }

        [Column("filePath")]
        public string filePath { get; set; }

        [Column("POY_LOT_NO")]
        public string poyLotNo { get; set; }

        [Column("Grades")]
        public string grades { get; set; }

        [Column("PreVoucherID")]
        public string PreVoucherID { get; set; }

        [Column("GradeVersion")]
        public string GradeVersion { get; set; }

        [Column("UserID")]
        public string UserID { get; set; }
        /// <summary>
        /// 加测单据编号
        /// </summary>
        [Column("PlanAddID")]
        public int PlanAddID { get; set; }
    }
}
