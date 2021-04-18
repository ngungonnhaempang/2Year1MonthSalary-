using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QC.Model
{
 [Table("ChartQuery")]
  public  class ChartQuery
    {
      public string BeginTime { get; set; }
      public string EndTime { get; set; }
      public string TypeID { get; set; }
      public string SampleName { get; set; }
      public string Line { get; set; }
      public string LOT_NO { get; set; }

      public string Property { get; set; }

      public string Charttype { get; set; }


      public string AB { get; set; }
      public string Grades { get; set; }

      public bool Isdelegate { get; set; }

     
    }
}
