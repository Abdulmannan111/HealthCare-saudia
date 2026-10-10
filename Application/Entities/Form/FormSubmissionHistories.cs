using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities.Form
{
    [BsonIgnoreExtraElements]
    public class FormSubmissionHistories : AuditInfoComplete
    {


        [BsonElement("form_id")]
        public string FormId { get; set; } = string.Empty;

        [BsonElement("reason")]
        public string? Reason { get; set; }


        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;


        [BsonElement("status")]
        public string Status { get; set; } = string.Empty;

    }
}
