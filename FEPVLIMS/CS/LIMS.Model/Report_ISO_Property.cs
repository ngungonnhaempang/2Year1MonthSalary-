using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{


    public class Report_ISO_Detail
    {

        public string VoucherID { get; set; }
        public string Status { get; set; }
        public string UserID { get; set; }


        public List<Report_ISO_Property> ListProperty { get; set; }
    }
    public class Report_ISO_Property
    {

        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }

      
    }
    
}
