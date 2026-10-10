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
    public class SubCategories : AuditInfoComplete  
    {
        public string IdAsString => ID.ToString();

        [BsonElement("name")]
        public string name { get; set; } = string.Empty;

        [BsonElement("category_id")]
        public string category_id { get; set; } = string.Empty;

    }
}
