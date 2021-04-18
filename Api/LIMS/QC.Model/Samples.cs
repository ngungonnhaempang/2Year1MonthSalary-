using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QC.Model
{
    [Serializable]

    public class Samples
    {
        public Samples()
        {
            Description = string.Empty;
            LabID = 0;
            SampleName = string.Empty;
            TypeID = 0;
            TypeName = string.Empty;
        }
        public string Description { get; set; }

        public int LabID { get; set; }

        public string TypeName { get; set; }

        public string SampleName { get; set; }

        public int TypeID { get; set; }
    }
}
