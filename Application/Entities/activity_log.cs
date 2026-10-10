using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class activity_log : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();

        [BsonElement("log_name")]
        public string log_name { get; set; } = string.Empty;

        [BsonElement("properties")]
        public string properties { get; set; } = string.Empty;

        [BsonElement("causer_id")]
        public string causer_id { get; set; } = string.Empty;

        [BsonElement("causer_type")]
        public string causer_type { get; set; } = string.Empty;

        [BsonElement("batch_uuid")]
        public string batch_uuid { get; set; } = string.Empty;

        [BsonElement("event")]
        public string event_ { get; set; } = string.Empty;

        [BsonElement("subject_id")]
        public string subject_id { get; set; } = string.Empty;

        [BsonElement("subject_type")]
        public string subject_type { get; set; } = string.Empty;

        [BsonElement("description")]
        public string description { get; set; } = string.Empty;
        public Users? causer { get; set; }

    }

    public class Properties
    {
        public Attributes? attributes { get; set; }
    }

    public class Attributes
    {
        public string? standard { get; set; }
        public DateTime updated_at { get; set; }
    }
   
}
