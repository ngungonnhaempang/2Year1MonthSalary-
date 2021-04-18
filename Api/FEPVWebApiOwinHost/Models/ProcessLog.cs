using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace FEPVWebApiOwinHost.Models
{
    [BsonIgnoreExtraElements]
    public class ProcessLog
    {
        [BsonElement("key")]
        [BsonSerializer(typeof(StringInt32BoolSerializer))]
        public string Key { get; set; }
        [BsonElement("keyname")]
        [BsonSerializer(typeof(StringInt32BoolSerializer))]
        public string KeyName { get; set; }
        [BsonElement("processInstanceId")]
        [BsonSerializer(typeof(StringInt32BoolSerializer))]
        public string ProcessInstanceId { get; set; }
        [BsonElement("taskid")]
        [BsonSerializer(typeof(StringInt32BoolSerializer))]
        public string TaskId { get; set; }
        [BsonElement("name")]
        [BsonSerializer(typeof(StringInt32BoolSerializer))]
        public string TaskName { get; set; }
        [BsonElement("description")]
        [BsonSerializer(typeof(StringInt32BoolSerializer))]
        public string TaskDescription { get; set; }
        [BsonElement("username")]
        [BsonSerializer(typeof(StringInt32BoolSerializer))]
        public string UserId { get; set; }
        [BsonSerializer(typeof(StringInt32BoolSerializer))]
        public string UserName { get; set; }
        [BsonElement("historyField")]
        public List<ProcessForm> HistoryField { get; set; }
        [BsonElement("sync")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Stamp { get; set; }

        public string FormatStamp
        {
            get
            {
                return Stamp.ToString("yyy-MM-dd HH:mm:ss");
            }
        }

        public string FormatStampMonthly
        {
            get
            {
                return Stamp.ToString("yyyy-MM");
            }
        }
    }
}
