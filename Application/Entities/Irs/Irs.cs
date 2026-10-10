using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Irs
{
    [BsonIgnoreExtraElements]
    public class Irs : AuditInfoComplete
    {

        [BsonElement("irs_type")]
        public string IrsType { get; set; } = string.Empty;

        [BsonElement("pharregion")]
        public string? Pharregion { get; set; }

        [BsonElement("pharsite")]
        public string? Pharsite { get; set; }

        [BsonElement("phcregion")]
        public string? Phcregion { get; set; }

        [BsonElement("phcsite")]
        public string? Phcsite { get; set; }

        [BsonElement("reporter_name")]
        public string ReporterName { get; set; } = string.Empty;

        [BsonElement("reporter_position")]
        public string? ReporterPosition { get; set; }

        [BsonElement("reporter_email")]
        public string ReporterEmail { get; set; } = string.Empty;

        [BsonElement("report_date_time")]
        public string ReportDateTime { get; set; } = string.Empty;

        [BsonElement("incident_date_time")]
        public string IncidentDateTime { get; set; } = string.Empty;

        [BsonElement("incident_type")]
        public string? IncidentType { get; set; }

        [BsonElement("incident_title")]
        public string? IncidentTitle { get; set; }

        [BsonElement("incident_description")]
        public string? IncidentDescription { get; set; }

        [BsonElement("dosage_strength")]
        public string DosageStrength { get; set; } = string.Empty;

        [BsonElement("generic_medicine_name")]
        public string? GenericMedicineName { get; set; }

        [BsonElement("trade_medicine_name")]
        public string? TradeMedicineName { get; set; }

        [BsonElement("action_taken")]
        public string? ActionTaken { get; set; }

        [BsonElement("attachments")]
        public string[]? Attachments { get; set; }

        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }


    }
}
