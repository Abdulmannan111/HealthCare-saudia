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
    [BsonIgnoreExtraElements]
    public class Settings : AuditInfoComplete
    {

        [Required]
        [StringLength(50)]
        [BsonElement("key")]
        public string Key { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [BsonElement("value")]
        public string Value { get; set; } = string.Empty ;

    }
}
