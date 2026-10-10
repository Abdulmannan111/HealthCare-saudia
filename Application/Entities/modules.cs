using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Application.Entities.Form;

namespace Application.Entities
{
    [BsonIgnoreExtraElements] 
    public class Modules : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();
        [Required]
        [StringLength(100)]
        [BsonElement("name")]
        public string name { get; set; } = string.Empty;

        [BsonElement("user_ids")]
        public List<string> user_ids { get; set; } = new List<string>();

        [BsonElement("form_options")]
        public FormOptions? FormOptions { get; set; }

        [BsonElement("slug")]
        public string slug { get; set; } = string.Empty;

        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; }

        [BsonElement("active")]
        public bool active { get; set; }

        [BsonElement("reference_name")]
        public string reference_name { get; set; } = string.Empty;

        [BsonElement("roles")]
        public List<string> Roles { get; set; } = new List<string>();
    }
}
