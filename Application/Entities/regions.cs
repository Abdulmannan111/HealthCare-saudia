using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Application.Entities
{

    [BsonIgnoreExtraElements]
    public class Regions : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();

        [Required]
        [StringLength(100)]
        [BsonElement("name")]
        public string name { get; set; } = string.Empty;  // Matches 'name' in RegionDto

        [BsonElement("phcRegion")]
        public bool phcRegion { get; set; }  // Matches 'phcRegion' in RegionDto

        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; }

        [BsonElement("user_ids")]
        public List<string> userIDs { get; set; } = new List<string>();  // Matches 'userIDs' in RegionDto

        [BsonElement("self_assessment_schedule_ids")]
        public List<string> selfAssessmentScheduleIds { get; set; } = new List<string>();  // Matches 'selfAssessmentScheduleIds' in RegionDto


    }
}
