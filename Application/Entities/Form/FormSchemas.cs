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
    public class FormSchemas : AuditInfoComplete
    {


        [BsonElement("form_id")]
        public string FormId { get; set; } = string.Empty;


        [BsonElement("data")]
        public string Data { get; set; } = string.Empty;


        [BsonElement("schema_version")]
        public int SchemaVersion { get; set; }

    }
}
