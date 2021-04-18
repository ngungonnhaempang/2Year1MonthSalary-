using System.Collections.Generic;

namespace FEPVWebApiOwinHost.Models
{
    public class ProcessDTO
    {
        public string KeyName { get; set; }

        public string Month { get; set; }

        public List<ProcessLog> Logs { get; set; }
    }
}
