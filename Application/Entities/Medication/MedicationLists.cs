using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities.Medication
{
    [BsonIgnoreExtraElements]
    public class MedicationLists : AuditInfoComplete
    {

        [BsonElement("region_id")]
        public string RegionId { get; set; } = string.Empty;


        [BsonElement("site_id")]
        public string SiteId { get; set; } = string.Empty;


        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
