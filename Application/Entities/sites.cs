using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public class Sites : AuditInfoComplete
    {
        [Required]
        [StringLength(100)]
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [BsonElement("license")]
        public string License { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [BsonElement("location")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [BsonElement("distract")]
        public string Distract { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [BsonElement("latitude")]
        public string Latitude { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [BsonElement("longitude")]
        public string Longitude { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [BsonElement("street_name")]
        public string StreetName { get; set; } = string.Empty;

        [BsonElement("deleted_at")]
        public DateTime DeletedAt { get; set; }

    }
}
