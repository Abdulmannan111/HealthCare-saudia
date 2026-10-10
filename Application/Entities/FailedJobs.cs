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
    public class FailedJobs : AuditInfoComplete
    {

       
        [BsonElement("connection")]
        public string Connection { get; set; } = string.Empty;

       
        [BsonElement("queue")]
        public string Queue { get; set; } = string.Empty ;

       
        [BsonElement("payload")]
        public string PayLoad { get; set; } = string.Empty;

        
        [BsonElement("failed_at")]
        public int FailedAt { get; set; }

        
        [BsonElement("exception")]
        public string Exception { get; set; } = string.Empty;
    }
}
