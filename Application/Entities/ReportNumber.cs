using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class ReportNumber
    {
        [Key]
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ID { get; set; }
        [BsonElement("reportCount")]
        public int reportCount { get; set; }
        [BsonElement("updated_at")]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
        [BsonElement("created_at")]
        public DateTime? created_at { get; set; }

    }
}
