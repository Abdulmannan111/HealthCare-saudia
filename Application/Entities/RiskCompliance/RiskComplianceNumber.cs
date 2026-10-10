using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.RiskCompliance
{
    [BsonIgnoreExtraElements]
    public class RiskComplianceNumber : AuditInfoComplete
    {
        public string idAsString => ID.ToString();

        [BsonElement("riskCount")]
        public int RiskCount { get; set; } // Stores the current risk count
    }
}
