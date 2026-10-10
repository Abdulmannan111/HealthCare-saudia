using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Application.Entities
{
    public class Platform : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();

        [BsonElement("name")]
        public string? name { get; set; }

        [BsonElement("name_ar")]
        public string? name_ar { get; set; }

        [BsonElement("dotnet_url")]
        public string? dotnet_url { get; set; }

        [BsonElement("laravel_url")]
        public string? laravel_url { get; set; }

        [BsonElement("frontend_url")]
        public string? frontend_url { get; set; }

        [BsonElement("icon")]
        public string? icon { get; set; }

        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; }

        [BsonElement("config")]
        public Dictionary<string, bool> config { get; set; } = new();
    }
}
