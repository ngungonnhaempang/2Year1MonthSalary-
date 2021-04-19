using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Shawoo.Core;


namespace LIMS.Model
{
    [Serializable]
    [Table("Material")]
    public partial class QCMaterial : ORM
    {
        [Column("SampleName", true)]
        public string SampleName { get; set; }

        [Column("LOT_NO", true)]
        public string LOT_NO { get; set; }

        [Column("Description_EN")]
        public string Description_EN { get; set; }

        [Column("Description_TW")]
        public string Description_TW { get; set; }

        [Column("Description_CN")]
        public string Description_CN { get; set; }

        [Column("Description_VN")]
        public string Description_VN { get; set; }

        /// <summary>
        /// 是否有等级规格的物料
        /// </summary>
        public bool IsGrade { get; set; }

    }
}
