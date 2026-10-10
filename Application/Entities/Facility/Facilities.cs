using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Entities.Facility
{
    [BsonIgnoreExtraElements]
    public class Facilities : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();
        [StringLength(100)]
        [BsonElement("name")]
        public string name { get; set; } = string.Empty;

        [StringLength(100)]
        [BsonElement("name_ar")]
        public string name_ar { get; set; } = string.Empty;

        [BsonElement("user_ids")]
        public List<string> user_ids { get; set; } = new List<string>();
    }
}
