using Application.DTOs.Category;
using Application.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RiskCompliance
{
    public class RiskComplianceItemDto
    {
        public string _id { get; set; } = string.Empty;
        public object step { get; set; }
        public string legal_descriptions { get; set; } = string.Empty;
        public string standard { get; set; } = string.Empty;
        public string domain_id { get; set; } = string.Empty;
        public string category_id { get; set; } = string.Empty;
        public string subCategory_id { get; set; } = string.Empty;
        public double? likelihood { get; set; }
        public double? impact { get; set; }
        public int? likelihood_postfix { get; set; }
        public int? impact_prefix { get; set; }
        public int? score { get; set; }
        public double weightage { get; set; }
        public double risk { get; set; }
        public string identifier { get; set; } = string.Empty;
        public bool active { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public CategoryDto? category { get; set; }
        public DomainDto? domain { get; set; }
        public SubCategoryDto? sub_category { get; set; }
        public DateTime? deleted_at { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<activity_logDto>? activity { get; set; }

    }
}
