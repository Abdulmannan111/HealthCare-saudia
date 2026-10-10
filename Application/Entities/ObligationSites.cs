using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.Middlewares;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class ObligationSites : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();
        [Required]
        [StringLength(100)]
        [BsonElement("name")]
        public string name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [BsonElement("name_ar")]
        public string name_ar { get; set; } = string.Empty;


        [BsonElement("facilityType")]
        public string facilityType { get; set; } = string.Empty;

        [BsonElement("site_parent_id")]
        public string? site_parent_id { get; set; }

        [BsonElement("region_id")]
        public string region_id { get; set; } = string.Empty;

        [BsonElement("sub_region_id")]
        public string sub_region_id { get; set; } = string.Empty;

        [BsonElement("sector_id")]
        public string sector_id { get; set; } = string.Empty;

        [BsonElement("sub_sector_id")]
        public string sub_sector_id { get; set; } = string.Empty;

        [BsonElement("batch_id")]
        public int? batch_id { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }

        
        [BsonElement("status")]
        public string status { get; set; } = string.Empty;

        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; }

        [BsonElement("region")]
        public Regions? region { get; set; }

        [BsonElement("sector")]
        public Sector? Sector { get; set; }

        [BsonElement("sub_region")]
        public Sub_region? Sub_region { get; set; }

        [BsonElement("sub_sector")]
        public Sub_sector? Sub_sector { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class Sector
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = string.Empty;

        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; }

        [BsonElement("name")]
        public string name { get; set; } = string.Empty;
    }
    [BsonIgnoreExtraElements]
    public class Sub_sector
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = string.Empty;

        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; }

        [BsonElement("name")]
        public string name { get; set; } = string.Empty;
    }
    [BsonIgnoreExtraElements]
    public class Sub_region
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = string.Empty;

        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; }

        [BsonElement("name")]
        public string name { get; set; } = string.Empty;
    }
}