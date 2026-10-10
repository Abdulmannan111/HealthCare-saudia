using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Entities
{
    /// <summary>
    ///  Audit info for the different Entities
    /// </summary>
    /// 
    [BsonIgnoreExtraElements]
    public class AuditInfoBase
    {
        [Key]
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ID { get; set; }
    }

    /// <summary>
    ///  Audit info for the different Entities
    /// </summary>
    /// 
    [BsonIgnoreExtraElements]
    public class AuditInfoComplete: AuditInfoBase
    {
        /// <summary>
        /// Date the entity was created
        /// </summary>
        [BsonElement("created_at")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Date the entity was updated
        /// </summary>
        [BsonElement("updated_at")]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        ///  User creating the entity
        /// </summary>
        [BsonElement("createdFrom")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// User updating the entity
        /// </summary>
        [BsonElement("updatedFrom")]
        public string? UpdatedBy { get; set; }

        /// <summary>
        ///  Manage document version for concurrent access
        /// </summary>
        [BsonElement("version")]
        public int Version { get; set; } = 5;
    }
}
