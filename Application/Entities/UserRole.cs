using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class UserRole : AuditInfoComplete
    {
        [BsonElement("userID")]
        public string UserID { get; set; } = string.Empty;

        [BsonElement("RoleID")]
        public string RoleID { get; set; } = string.Empty;
    }
}
