
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QC.Model
{
  
    public class PlanAddDto 
    {
     

  
        public int VoucherID { get; set; }

     
        public string SampleName { get; set; }

      
        public string LOT_NO { get; set; }

      
        public string LINE { get; set; }

     
        public DateTime SheetDate { get; set; }

        
        public DateTime ProdDate { get; set; }

      
        public string SampleID { get; set; }

       
        public string State { get; set; }

       
        public string Purpose { get; set; }

     
        public string CompanyOfferSample { get; set; }

     
        public string CompanyProduce { get; set; }

      
        public int CreateType { get; set; }

      
        public Guid GradeVersion { get; set; }

       
        public string UserID { get; set; }
    }
}