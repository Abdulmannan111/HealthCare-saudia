using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class DraftNupcoUsers : AuditInfoComplete
    {


        [BsonElement("nupco_id")]
        public string NupcoId {  get; set; } = string.Empty;

        
        [BsonElement("user_type")]
        public string UserType { get; set; } = string.Empty;

        [BsonElement("community_portal_user")]
        public string CommunityPortalUser { get; set; } = string.Empty ;

        [BsonElement("practitioners_in_call")]
        public string PractitionersInCall { get; set; } = string.Empty;

        [BsonElement("practitioners_specialty_description_eng")]
        public string PractitionersSpecialtyDescriptionEng { get; set; } = string.Empty;

        [BsonElement("practitioners_specialty_description_arb")]
        public string PractitionersSpecialtyDescriptionArb { get; set; } = string.Empty;

        [BsonElement("medical_license_for_practitioners")]
        public string MedicalLicenseForPractitioners { get; set; } = string.Empty;


        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;


        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
