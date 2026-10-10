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
    public class AdminMedicines : AuditInfoComplete
    {
        
        [BsonElement("medicines_data")]
        public string MedicinesData { get; set; } = string.Empty;

        [BsonElement("is_active")]
        public bool IsActive { get; set; }

       
        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("medic")]
        public Object? Medic { get; set; }

    }
}
