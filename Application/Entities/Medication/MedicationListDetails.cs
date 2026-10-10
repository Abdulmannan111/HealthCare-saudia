using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities.Medication
{
    [BsonIgnoreExtraElements]
    public class MedicationListDetails : AuditInfoComplete
    {


        [BsonElement("medicine_id")]
        public string MedicineId { get; set; } = string.Empty;


        [BsonElement("available_in_pharmacy")]
        public string AvailableInPharmacy { get; set; } = string.Empty;

        [BsonElement("low_stock_in_shelf")]
        public string LowStockInShelf { get; set; } = string.Empty;

        [BsonElement("high_in_demand")]
        public string HighInDemand { get; set; } = string.Empty;


        [BsonElement("medication_submission_id")]
        public string MedicationSubmissionId { get; set; } = string.Empty;

    }
}
