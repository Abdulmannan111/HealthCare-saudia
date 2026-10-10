using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Category
{
    [BsonIgnoreExtraElements]
    public class Category : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();

        [BsonElement("name")]
        public string name { get; set; } = string.Empty;

        public List<SubCategories>? sub_category { get; set; }
    }
}
