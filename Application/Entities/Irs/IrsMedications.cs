using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities.Irs
{
    [BsonIgnoreExtraElements]
    public class IrsMedications : AuditInfoComplete
    {


        [BsonElement("code")]
        public int Code { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = string.Empty;

        [BsonElement("formula")]
        public string Formula { get; set; } = string.Empty;

    }
}
