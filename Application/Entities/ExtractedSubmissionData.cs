using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class ExtractedSubmissionData : AuditInfoComplete
    {

        [BsonElement("step")]
        public string Step { get; set; } = string.Empty;

        [BsonElement("no")]
        public int No { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("Healthcare Quality")]
        public string HealthcareQuality { get; set; } = string.Empty;

        [BsonElement("Likelihood")]
        public int Likelihood { get; set; }

        [BsonElement("Impact")]
        public int Impact { get; set; }

        [BsonElement("Category")]
        public string Category { get; set; } = string.Empty;

        [BsonElement("Sub_Category")]
        public string SubCategory { get; set; } = string.Empty;

        [BsonElement("question")]
        public string Question { get; set; } = string.Empty;

        [BsonElement("parent")]
        public string Parent { get; set; } = string.Empty;

        [BsonElement("region_id")]
        public string RegionId { get; set; } = string.Empty;

        [BsonElement("site_id")]
        public string SiteId { get; set; } = string.Empty;

        [BsonElement("form_id")]
        public string FormId { get; set; } = string.Empty;

        [BsonElement("form_submission_id")]
        public string FormSubmissionId { get; set; } = string.Empty;

        [BsonElement("inspection_type")]
        public string InspectionType { get; set; } = string.Empty;

        [BsonElement("form_submission_created_at")]
        public DateTime? FormSubmissionCreatedAt { get; set; }


    }
}
