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
    public class Medicines : AuditInfoComplete
    {

        [Required]
        [StringLength(100)]
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [BsonElement("genericCode")]
        public string GenericCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [BsonElement("slug")]
        public string Slug { get; set; } = string.Empty;

    }
}
