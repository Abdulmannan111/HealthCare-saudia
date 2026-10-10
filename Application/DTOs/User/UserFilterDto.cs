namespace Application.DTOs.User
{
    public class UserFilterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? Region { get; set; }
        public bool? IsActive { get; set; }
    }
}
