using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.SubRegion
{
    [BsonIgnoreExtraElements]
    public class SubRegions : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();

        [BsonElement("name")]
        public string name { get; set; } = string.Empty;

        [BsonElement("region_id")]
        public string sector_id { get; set; } = string.Empty;
    }
}
