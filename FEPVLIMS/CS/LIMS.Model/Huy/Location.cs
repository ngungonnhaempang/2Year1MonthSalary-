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
    [Table("Location")]
    public class Location
    {
        [Required,Key,Column(Order=0)]
        public string Loc_ID {get; set;}

        public string Loc_Name {get; set;}

        public string Description { get; set; }

        public List<University> _getUniversity { get; set; }

        public List<Registration> _getRegistLoc { get; set; }
    }
}
