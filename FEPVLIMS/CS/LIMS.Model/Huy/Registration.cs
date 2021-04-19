using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{
    [Serializable]
    [Table("Registration")]
    public class Registration
    {
        [Required, Key, Column(Order = 0)]
        public string ID { get; set; }

        public string  Uni_ID{ get; set; }
  
        public string Loc_ID { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

    }
}
