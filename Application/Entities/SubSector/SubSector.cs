using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.SubSector
{
    [BsonIgnoreExtraElements]
    public class SubSector : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();

        [BsonElement("name")]
        public string name { get; set; } = string.Empty;

        [BsonElement("sector_id")]
        public string sector_id { get; set; } = string.Empty;
    }
}
