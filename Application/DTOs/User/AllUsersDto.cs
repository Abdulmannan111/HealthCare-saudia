namespace Application.DTOs.User
{
    public class AllUsersDto
    {
        public string? search { get; set; }
        public formInfo? formInfo { get; set; } = new formInfo();
        public string? module { get; set; }

    }
    public class formInfo
    {
        public string? name { get; set; }
        public string? projectId { get; set; }
        public string? slug { get; set; }
        public string? version { get; set; }
        public string? type { get; set; } = string.Empty;
        public string? _id { get; set; }
        public object? meta_data { get; set; } = new object(); // Adjust this type based on your specific structure
        public object? template { get; set; } = new object(); // Adjust this type based on your specific structure
    }

}
