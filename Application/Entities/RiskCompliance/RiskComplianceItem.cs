using Application.DTOs;
using Application.Entities.Domain;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.Entities.RiskCompliance
{
    [BsonIgnoreExtraElements]
    public class RiskComplianceItem : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();

        [BsonElement("step")]
        public string step { get; set; } = string.Empty;

        [BsonElement("legal_descriptions")]
        public string legal_descriptions { get; set; } = string.Empty;

        [BsonElement("standard")]
        public string standard { get; set; } = string.Empty;

        [BsonElement("domain_id")]
        public string domain_id { get; set; } = string.Empty;

        [BsonElement("category_id")]
        public string category_id { get; set; } = string.Empty;

        [BsonElement("subCategory_id")]
        public string subCategory_id { get; set; } = string.Empty;

        [BsonElement("likelihood")]
        public decimal likelihood { get; set; }

        [BsonElement("impact")]
        public decimal impact { get; set; }

        [BsonElement("likelihood_postfix")]
        public int likelihood_postfix { get; set; }

        [BsonElement("impact_prefix")]
        public int? impact_prefix { get; set; }

        [BsonElement("score")]
        public decimal? score { get; set; }

        [BsonElement("weightage")]
        public decimal? weightage { get; set; }

        [BsonElement("risk")]
        public decimal? risk { get; set; }

        [BsonElement("identifier")]
        public string identifier { get; set; } = string.Empty;

        [BsonElement("active")]
        public bool active { get; set; }

        [BsonElement("category")]
        public Category.Category? category { get; set; }

        [BsonElement("domain")]
        public Domains? domain { get; set; }

        [BsonElement("sub_category")]
        public SubCategories? sub_category { get; set; }

        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; } = DateTime.UtcNow;

        [BsonElement("activity")]
        public List<activity_log>? activity { get; set; }

    }
}
