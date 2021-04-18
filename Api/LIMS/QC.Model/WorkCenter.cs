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
    public class WorkCenter
    {
        public WorkCenter()
        {
            Address = string.Empty;
            LabID = 0;
            Manager = string.Empty;
            Tel = string.Empty;
            LabName = string.Empty;
        }

        public string Address { get; set; }

        public string LabName { get; set; }

        public int LabID { get; set; }

        public string Manager { get; set; }

        public string Tel { get; set; }

    }
}
