using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class CustomNotification : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();

        [BsonElement("subject")]
        public string subject { get; set; } = string.Empty;

        [BsonElement("content")]
        public string content { get; set; } = string.Empty;

        [BsonElement("is_individual")]
        public bool is_individual { get; set; }

        [BsonElement("user_id")]
        public string user_id { get; set; } = string.Empty;

        [BsonElement("user_emails")]
        public List<string> user_emails { get; set; }  = new List<string>();

        [BsonElement("user_roles")]
        public List<string> user_roles { get; set; } = new List<string>();

        public Users? user { get; set; }

    }

}

