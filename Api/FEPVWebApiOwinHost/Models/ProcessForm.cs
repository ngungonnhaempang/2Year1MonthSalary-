using MongoDB.Bson.Serialization.Attributes;

namespace FEPVWebApiOwinHost.Models
{
    
    public class ProcessForm
    {
        [BsonElement("name")]
        [BsonSerializer(typeof(StringInt32BoolSerializer))]
        public object Name { get; set; }
        [BsonElement("value")]
        [BsonSerializer(typeof(StringInt32BoolSerializer))]
        public string Value { get; set; }

        public bool IsJson 
        {
            get
            {
                try
                {
                    Newtonsoft.Json.Linq.JArray.Parse(Value);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public Newtonsoft.Json.Linq.JArray JsonValue
        {
            get
            {
                try
                {
                    return Newtonsoft.Json.Linq.JArray.Parse(Value);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
