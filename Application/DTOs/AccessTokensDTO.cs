namespace Application.DTOs
{
    public class AccessTokensDTO
    {
        public string _id { get; set; } = string.Empty;
        public string id { get; set; } = string.Empty;
        public string user_id { get; set; } = string.Empty;
        public string client_id { get; set; } = string.Empty;
        public string scopes { get; set; } = string.Empty;
        public bool revoked { get; set; }
        public DateTime expires_at { get; set; }
        public string name { get; set; } = string.Empty;
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
