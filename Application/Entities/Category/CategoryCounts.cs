using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Category
{
    [BsonIgnoreExtraElements]
    public class CategoryCounts : AuditInfoComplete
    {

        [BsonElement("category_id")]
        public string CategoryId { get; set; } = string.Empty;

        [BsonElement("comply_count")]
        public int ComplyCount { get; set; }

        [BsonElement("not_comply_count")]
        public int NotComplyCount { get; set; }

        [BsonElement("DomainCountIds")]
        public List<string> DomainCountIds { get; set; } = new List<string>();


    }
}
