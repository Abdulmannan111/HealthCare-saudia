using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Medication
{
    [BsonIgnoreExtraElements]
    public class MedicationDemands : AuditInfoComplete
    {

        [BsonElement("region_id")]
        public string RegionId { get; set; } = string.Empty;


        [BsonElement("site_id")]
        public string SiteId { get; set; } = string.Empty;


        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;


        [BsonElement("cluster_id")]
        public string ClusterId { get; set; } = string.Empty;


        [BsonElement("city")]
        public string City { get; set; } = string.Empty;


        [BsonElement("moh_code")]
        public string MohCode { get; set; } = string.Empty;


        [BsonElement("medication_wasfaty_list")]
        public string? MedicationWasfatyList { get; set; }


        [BsonElement("medication_items")]
        public string[]? MedicationItems { get; set; }


        [BsonElement("active")]
        public bool IsActive { get; set; }


        [BsonElement("message")]
        public string Message { get; set; } = string.Empty;


        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }



    }
}
