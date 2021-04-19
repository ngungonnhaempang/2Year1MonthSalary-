using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{
    [Serializable]
    [Table("GradeVer")]
    public class GradeVer : ORM
    {
        public GradeVer()
        {
        }

        [Column("ID", true)]
        public Guid ID { get; set; }

        [Column("Version", true)]
        public int Version { get; set; }

        [Column("SampleName")]
        public string SampleName { get; set; }

        [Column("LOT_NO")]
        public string LOT_NO { get; set; }

        [Column("PropertyName")]
        public string PropertyName { get; set; }

        [Column("Grade")]
        public string Grade { get; set; }

        [Column("Grades")]
        public string Grades { get; set; }

        [Column("MaxValue")]
        public decimal? MaxValue { get; set; }

        [Column("MinValue")]
        public decimal? MinValue { get; set; }

        [Column("Prec")]
        public int Prec { get; set; }

        [Column("ValueSpec")]
        public string ValueSpec { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [Column("Enable")]
        public bool Enable { get; set; }

        [Column("CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("ValidDate")]
        public DateTime ValidDate { get; set; }

        [Column("ValidTODate")]
        public DateTime? ValidTODate { get; set; }
    }
}