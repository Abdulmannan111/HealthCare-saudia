using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class ActivityLog : AuditInfoBase
    {
       
        [BsonElement("UserId")]
        public string UserId { get; set; } = string.Empty;
        
        [BsonElement("LogDescription")]
        public string LogDescription { get; set; } = string.Empty;

        [BsonElement("LogDate")]
        public DateTime LogDate { get; set; }

        [BsonElement("ActivityCode")]
        public int ActivityCode { get; set; } 

    }

    public enum ActivityCodeType
    { 
       FacilityInfo = 1,
       FacilitySuccess = 2,
       FacilityFailure = 3, 
    }
}
