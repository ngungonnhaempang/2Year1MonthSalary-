using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{
    [Serializable]
    [Table("AD_MessageLogs")]
    public class AD_MessageLogs:ORM
    {
        public AD_MessageLogs()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        [Column("ID", true)]
        public string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("IP")]
        public string IP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("HostName")]
        public string HostName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("TCode")]
        public string TCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("UserAccount")]
        public string UserAccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("MessageType")]
        public string MessageType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Message")]
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Stamp")]
        public string Stamp { get; set; }
        
        
    }

}
