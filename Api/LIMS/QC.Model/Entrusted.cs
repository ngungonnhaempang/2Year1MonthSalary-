using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QC.Model
{
    
    [Table("Entrusted")]
    public class Entrusted
    {
        [Required, Key, Column(Order = 0)]
        public string VoucherID { get; set; }

        public string SampleName { get; set; }

        public string UserID { get; set; }

        public string DraftID { get; set; }

        public string LOT_NO { get; set; }

        public string GetDate { get; set; }

        public string SendDate { get; set; }

        public string RequireDate { get; set; }

        public string SampleFrom { get; set; }

        public string Purpose { get; set; }

        public string Spec { get; set; }


        public Attribute[] Properties { get; set; }
    }

    public class Attribute {

        public string SampleName { get; set; }
        public string PropertyName { get; set; }
    }
}
