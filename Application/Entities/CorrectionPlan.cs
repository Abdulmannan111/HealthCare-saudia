using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class CorrectionPlan : AuditInfoComplete
    {

        [BsonElement("submission_id")]
        public string? submission_id { get; set; }

        [BsonElement("form_id")]
        public string? form_id { get; set; }

        [BsonElement("user_id")]
        public string? user_id { get; set; }

        [BsonElement("report_no")]
        public string? report_no { get; set; }

        [BsonElement("region_id")]
        public string? region_id { get; set; }

        [BsonElement("site_id")]
        public string? site_id { get; set; }

        [BsonElement("status")]
        public string? status { get; set; }

        [BsonElement("cluster_id")]
        public string? cluster_id { get; set; }

        [BsonElement("due_date")]
        public DateTime? due_date { get; set; }
    }
}
