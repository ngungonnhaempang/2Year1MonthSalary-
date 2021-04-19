using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIMS.Model
{
    [Serializable]
    [Table("Profile")]
    public class Profile : ORM
    {
        public Profile()
        {
         
        }

        /// <summary>
        ///
        /// </summary>
        [Column("VoucherID", true)]
        public string VoucherID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("Item", true)]
        public int Item { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("SampleName")]
        public string SampleName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("PropertyName")]
        public string PropertyName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("Result")]
        public decimal? Result { get; set; }

        [Column("OriginalResult")]
        public string OriginalResult { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("State")]
        public string State { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Column("OverRange")]
        public bool OverRange { get; set; }


      

        /// <summary>
        ///
        /// </summary>
        public string LOT_NO { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Prec { get; set; }

        public string ResultSpec { get; set; }
     
    }
}