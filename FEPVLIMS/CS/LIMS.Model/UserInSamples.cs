using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LIMS.Model
{

    [Serializable]
    [Table("UserInSamples")]
   public class UserInSamples:ORM
    {
        [Column("UserID", true)]
        public string UserID { get; set; }


        [Column("SampleName", true)]
        public string SampleName { get; set; }

    }
}
