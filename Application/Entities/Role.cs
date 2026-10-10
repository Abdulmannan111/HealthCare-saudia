using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class Role : AuditInfoComplete
    {
        [Required]
        [StringLength(100)]
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("health_directorate")]
        public List<string> HealthDirectorate { get; set; } = new List<string>();

        [BsonElement("responsible_department")]
        public List<string> ResponsibleDepartment { get; set; } = new List<string>();

        [BsonElement("permission_ids")]
        public List<string> PermissionIds { get; set; } = new List<string>();
    }
}
