using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Application.Entities
{
    public class TicketSlas: AuditInfoComplete
    {
        public string IdAsString => ID.ToString();
        [BsonElement("form_id")]
        public string? FormId { get; set; }  // Unique

        [BsonElement("priority_levels")]
        public Dictionary<string, int>? PriorityLevels { get; set; }
        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; }

    }
}
