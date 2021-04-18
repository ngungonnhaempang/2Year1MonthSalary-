using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace FEPVWebApiOwinHost.Models
{
    public class Structure
    {
        [BsonElement("_id")]
        [BsonId]
        public string DepartmentID { get; set; }
        [BsonElement("Checkers")]
        public List<string> Checkers { get; set; }
        [BsonElement("Safety")]
        public List<string> Safety { get; set; }
    }
}
