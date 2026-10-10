using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class oauth_access_tokens : AuditInfoComplete
    {
        [BsonElement("id")]
        public string IdValue { get; set; } = string.Empty;

        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("client_id")]
        public string ClientId { get; set; } = string.Empty;

        [BsonElement("scopes")]
        public string Scopes { get; set; } = string.Empty;

        [BsonElement("revoked")]
        public bool Revoked { get; set; }

        [BsonElement("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
    }
}
