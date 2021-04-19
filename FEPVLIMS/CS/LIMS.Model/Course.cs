using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{
    [Serializable]
    [Table("Course2")]
    public class Course2: ORM
    {
        [Column("CourseID", true)]
        public int CourseID { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("Credits")]
        public int Credits { get; set; }

    }
}
