using MongoDB.Bson.Serialization.Attributes;

namespace Application.Entities.Form
{
    [BsonIgnoreExtraElements]
    public class Form
    {
        public string id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string icon { get; set; } = string.Empty;
        public string? start_date { get; set; }
        public string? end_date { get; set; }
        public string projectId { get; set; } = string.Empty;
        public object? meta_data { get; set; } = new object(); // Adjust this type based on your specific structure
        public string? campaign_title { get; set; }
        public string slug { get; set; } = string.Empty;
        public string createdAt { get; set; } = string.Empty;
        public string savedIcon { get; set; } = string.Empty;
        public bool isInspectionEngine { get; set; }
        public bool? is_campaign { get; set; }
        public bool? is_support { get; set; }
        public bool? status { get; set; }
    }
}
