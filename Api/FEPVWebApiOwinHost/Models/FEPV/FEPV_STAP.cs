using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models.FEPV
{
   
    [Table("FEPV_STAP")]
    public class FEPV_STAP : Goods
    {
        [Column("LINE")]
        public string LINE { get; set; }

        [Column("GRADE")]
        public string GRADE { get; set; }


        [Column("GRADES")]
        public string GRADES { get; set; }


        [Column("GWT")]
        public decimal GWT { get; set; }
        [Column("RWT")]
        public decimal RWT { get; set; }
        [Column("Drang")]
        public decimal Drang { get; set; }

        string _Chip = "--";
        [Column("Chip")]
        public string Chip
        {
            get { return _Chip; }
            set { _Chip = value; }
        }

        public string MaxP { get; set; }
        public string MinP { get; set; }

        public override string TableName
        {
            get { return "FEPV_STAP"; }
        }

        public override string Batch
        {
            get
            {
                return GRADE.PadRight(2, '-') + GRADES.PadRight(2, '-') + LINE.PadRight(2, '-') + Chip.PadRight(2, '-');
            }
        }
        public override string[] COLUMNS
        {
            get { return new string[] { "BarCode", "Line", "GRADE", "GRADES", "GWT", "RWT", "Drang", "Chip" }; }
        }
        public override object[] VALUES
        {
            get
            {
                return new object[] { BarCode, LINE, GRADE, GRADES, GWT, RWT, Drang, Chip };
            }
        }
    }
}
