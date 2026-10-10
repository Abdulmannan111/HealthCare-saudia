using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Domain
{
    [BsonIgnoreExtraElements]
    public class Domains : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();

        [BsonElement("name")]
        public string name { get; set; } = string.Empty;

        [BsonElement("slug")]
        public string slug { get; set; } = string.Empty;
    }
}
