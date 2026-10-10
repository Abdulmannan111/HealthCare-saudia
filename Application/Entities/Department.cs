using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class Department 
    {
        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("skipListing")]
        public bool skipListing { get; set; }

        [BsonElement("ranking")]
        public string Ranking { get; set; } = string.Empty;

        [BsonElement("attribute")]
        public string Attribute { get; set; } = string.Empty;

        [BsonElement("type")]
        public string Type { get; set; } = string.Empty;

        [BsonElement("isSkipOthers")]
        public bool isSkipOthers { get; set; }

        [BsonElement("options")]
        public List<string> Options { get; set; } = new List<string>();

        [BsonElement("arabic")]
        public string Arabic { get; set; } = string.Empty;

        [BsonElement("step")]
        public int step { get; set; }

        [BsonElement("remarksText")]
        public string remarksText { get; set; } = string.Empty;

        [BsonElement("risk")]
        public string risk { get; set; } = string.Empty;

        [BsonElement("min")]
        public int min { get; set; } 
    }
}
