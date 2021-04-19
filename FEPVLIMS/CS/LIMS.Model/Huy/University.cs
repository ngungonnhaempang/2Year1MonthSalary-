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
    [Table("University")]
    public class University
    {
        [Required, Key, Column(Order = 0)]
        public string Uni_ID { get; set; }
       
        public string Loc_ID { get; set; }

        public string Uni_Name { get; set; }

        public string Remark { get; set; }

        public List<Registration> _getRegistUni { get; set; }
    }
}
