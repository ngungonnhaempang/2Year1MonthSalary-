using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIMS.Model
{
    [Serializable]
    [Table("Samples")]
    public class QCSample : ORM
    {
        public QCSample()
        {
            SampleName = string.Empty;
            Description_EN = string.Empty;
            Description_TW = string.Empty;
            Description_CN = string.Empty;
            Description_VN = string.Empty;
        }

        [Column("SampleName", true)]
        public string SampleName { get; set; }

        // public string LabName { get; set; }

        [Column("LabID")]
        public int LabID { get; set; }

        // public string TypeName { get; set; }

        [Column("TypeID")]
        public int TypeID { get; set; }

        [Column("Description_EN")]
        public string Description_EN { get; set; }

        [Column("Description_TW")]
        public string Description_TW { get; set; }

        [Column("Description_CN")]
        public string Description_CN { get; set; }

        [Column("Description_VN")]
        public string Description_VN { get; set; }

         [Column("AB")]
        public string AB { get; set; }

       // public string LabName { get; set; }

       // public string TypeName { get; set; }
    }
}