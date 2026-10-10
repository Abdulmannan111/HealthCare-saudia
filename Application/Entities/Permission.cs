using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class Permission : AuditInfoComplete
    {
        [Required]
        [StringLength(100)]
        [BsonElement("Name")]
        public string name { get; set; } = string.Empty;
    }
}
