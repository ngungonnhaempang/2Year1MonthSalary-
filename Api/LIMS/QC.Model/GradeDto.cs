using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QC.Model
{
   public class GradeMaterialDto
    {
       public string ID { get; set; }
       public string SampleName { get; set; }
       public string LOT_NO { get; set; }
       public string Grades { get; set; }

       public string Grade{ get; set; }


       public DateTime ValidDate { get; set; }

       public DateTime? ValidTODate { get; set; }

       public string UserID { get; set; }

       public int Version { get; set; }
        public string Remark { get; set; }

       public List<GradeDto> GradesDto { get; set; }
    }


   public class GradeDto
   {


     

       public string PropertyName { get; set; }
    

       public bool Enable { get; set; }

       public decimal? MaxValue { get; set; }

       public decimal? MinValue { get; set; }

       public int Prec { get; set; }

       public string ValueSpec { get; set; }
        public string Remark { set; get; }
   }
}
