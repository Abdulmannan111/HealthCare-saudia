using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Domain
{
    [BsonIgnoreExtraElements]
    public class DomainCounts : AuditInfoComplete
    {

        [BsonElement("domain_id")]
        public string DomainId { get; set; } = string.Empty;

        [BsonElement("comply_count")]
        public int ComplyCount { get; set; }

        [BsonElement("not_comply_count")]
        public int NotComplyCount { get; set; }

        [BsonElement("category_count_ids")]
        public List<string> CategoryCountIds { get; set; } = new List<string>();


    }
}
