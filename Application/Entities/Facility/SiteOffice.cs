using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Entities.Facility
{
    public class SiteOffice : AuditInfoComplete
    {
        public string value => ID.ToString();

        [Required]
        [StringLength(100)]
        [BsonElement("name")]
        public string? name { get; set; }
        [BsonElement("site_id")]
        public string site_id { get; set; } = string.Empty;
        [BsonElement("region_id")]
        public string region_id { get; set; } = string.Empty;
        [BsonElement("cluster_id")]
        public string cluster_id { get; set; } = string.Empty;
        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; }

        
        [BsonElement("obligation_sites")]
        public ObligationSites? obligation_sites { get; set; }
    }
}
