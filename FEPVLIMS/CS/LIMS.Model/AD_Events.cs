using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Model
{
    [Serializable]
    [Table("AD_Events")]
    public class AD_Events:ORM
    {
        public AD_Events()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        [Column("EventType")]
        public string EventType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("SQLCommand")]
        public string SQLCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("ClientIP")]
        public string ClientIP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("ClientName")]
        public string ClientName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Stamp")]
        public string Stamp { get; set; }
    }
}
