using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LIMS.Model
{
    [Serializable]
    [Table("ChartUCL2LCL")]
    public class QCChart: ORM
    {
        public QCChart()
        {
            SampleName = string.Empty;
            MaterialNo = string.Empty;
            LineNo = string.Empty;
            Property = string.Empty;
            UCL = 0;
            LCL = 0;
            Date = DateTime.Now;
        }

        [Column("SampleName", true)]
        public string SampleName { get; set; }

        [Column("LOT_NO", true)]
        public string MaterialNo { get; set; }

        [Column("LINE", true)]
        public string LineNo { get; set; }

        [Column("PropertyName", true)]
        public string Property { get; set; }

        [Column("UCL", true)]
        public decimal UCL { get; set; }

        [Column("LCL", true)]
        public decimal LCL { get; set; }

        [Column("CreateDate", true)]
        public DateTime Date { get; set; }

        [Column("LastModify", true)]
        public DateTime LastModify { get; set; }

        [Column("Remark", true)]
        public string Remark { get; set; }

    }
}
