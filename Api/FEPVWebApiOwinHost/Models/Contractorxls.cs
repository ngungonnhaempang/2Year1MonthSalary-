using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models
{

    /// <summary>
    /// 从Excel导入的承揽商
    /// </summary>
  public  class Contractorxls
    {
        public string IdCard { get; set; }
        public string Employer { get; set; }


        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime ValidTo { get; set; }
        public string Remark { get; set; }

   
        //受训时数
        public string TrainTime { get; set; }
        public DateTime? TTValidTo { get; set; }

        public string MedicalInspection { get; set; }
        public DateTime? MIValidTo { get; set; }

        public DateTime? TrainDate { get; set; }



    }


  public class ContractorImport {


      //public string EmployerId { set; get; }
      public string userid { set; get; }


      public virtual List<Contractorxls> Contractorxls { get; set; }
  }
}
