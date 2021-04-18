using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QC.Model
{
    [Table("StaplePickForGrade")]
    public class StaplePickForGrade :ORM
    {
        [Column("BarCode",true)]
        public string BarCode { get; set; }
        [Column("MaterialNO")]
        public string MaterialNO { get; set; }
        [Column("ProdDate")]
        public DateTime? ProdDate { get; set; }
        [Column("Batch")]
        public string Batch { get; set; }
        [Column("GroupBarCode")]
        public string GroupBarCode { get; set; }
        [Column("Grade")]
        public string Grade { get; set; }
        [Column("Status")]
        public string Status { get;set; }
        [Column("PickDate")]
        public DateTime? PickDate { get; set; }
        [Column("ProdSpec")]
        public string ProdSpec { get; set; }
        [Column("Line")]
        public string Line { get; set; }
        [Column("ModifyGradeDate")]
        public DateTime? ModifyGradeDate { get; set; }
        [Column("isModifyGrade")]
        public bool isModifyGrade { get; set; }
    }
    public class ListStaplePickForGrade
    {
        public List<StaplePickForGrade> list_StaplePickForGrade { get; set; }
    }
}
