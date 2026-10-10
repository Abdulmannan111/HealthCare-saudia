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
    public class MedicationAvailables : AuditInfoComplete
    {

        [BsonElement("region_id")]
        public string RegionId { get; set; } = string.Empty;


        [BsonElement("site_id")]
        public string SiteId { get; set; } = string.Empty;


        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;


        [BsonElement("longitude")]
        public double Longitude { get; set; }


        [BsonElement("latitude")]
        public double Latitude { get; set; }


        [Required]
        [BsonElement("phone")]
        public string Phone { get; set; } = string.Empty;


        [BsonElement("is_the_pharmacy_available")]
        public string IsThePharmacyAvailable { get; set; } = string.Empty;


        [BsonElement("look_feel")]
        public string LookFeel { get; set; } = string.Empty;


        [BsonElement("comments")]
        public string? Comments { get; set; }


        [BsonElement("image")]
        public string[]? Image { get; set; }


        [BsonElement("attachments")]
        public string[]? Attachments { get; set; }


        [BsonElement("medicine")]
        public string[]? Medicine { get; set; }


        [BsonElement("wasfaty_service_available")]
        public string? WasfatyServiceAvailable { get; set; }


        [BsonElement("pharmacy_connected_to_rased_system")]
        public string? PharmacyConnectedToRasedSystem { get; set; }


        [BsonElement("is_the_rassd_system_active_at_the_pharmacy")]
        public string? IsTheRassdSystemActiveAtThePharmacy { get; set; }


        [BsonElement("medication_instruction_label")]
        public string? MedicationInstructionLabel { get; set; }


        [BsonElement("reportNumber")]
        public string ReportNumber { get; set; } = string.Empty;


    }
}
