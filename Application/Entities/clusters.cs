using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class Clusters : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();
        [Required]
        [StringLength(100)]
        [BsonElement("name")]
        public string name { get; set; } = string.Empty;

        
        [BsonElement("region_id")]
        public string region_id { get; set; } = string.Empty;

        
        [BsonElement("sector")]
        public string sector { get; set; } = string.Empty;

        [BsonElement("is_active")]
        public bool is_active { get; set; }

        [BsonElement("user_ids")]
        public List<string> user_ids { get; set; } = new List<string>();

        [BsonElement("self_assessment_schedule_ids")]
        public List<string> self_assessment_schedule_ids { get; set; } = new List<string>();

        [BsonElement("region")]
        public Regions? region { get; set; }

        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; }
    }
}
