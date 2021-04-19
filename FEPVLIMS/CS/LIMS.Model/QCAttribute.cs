using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LIMS.Model
{
    [Serializable]
    [Table("Attribute")]
    public class QCAttribute:ORM
    {
        public QCAttribute()
        {
            SampleName = string.Empty;
            SampleDesc = string.Empty;
            PropertyName = string.Empty;
            Remark = string.Empty;
            Unit = string.Empty;
            Description_EN = string.Empty;
            Description_TW = string.Empty;
            Description_CN = string.Empty;
            Description_VN = string.Empty;
            EnRemark = string.Empty;
            JPNRemark = string.Empty;
            OrderBy = 0;
            Prec = 0;
        }

        [Column("SampleName", true)]
        public string SampleName { get; set; }

        [Column("PropertyName", true)]
        public string PropertyName { get; set; }

        [Column("Unit")]
        public string Unit { get; set; }

        [Column("Description_EN")]
        public string Description_EN { get; set; }

        [Column("Description_TW")]
        public string Description_TW { get; set; }

        [Column("Description_CN")]
        public string Description_CN { get; set; }

        [Column("Description_VN")]
        public string Description_VN { get; set; }

        [Column("Prec")]
        public int Prec { get; set; }

        [Column("OrderBy")]
        public int OrderBy { get; set; }

        [Column("Remark")]
        public string Remark { get; set; }

        [Column("EnRemark")]
        public string EnRemark { get; set; }

        [Column("JPNRemark")]
        public string JPNRemark { get; set; }

        public string SampleDesc { get; set; }
    }
}
