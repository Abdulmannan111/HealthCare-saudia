namespace Application.DTOs.User
{
    public class UserUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Should be hashed
        public string Role { get; set; }
        public string Phone { get; set; }
        public List<string> Region { get; set; }
        public string Cluster { get; set; }
        public List<string> Sites { get; set; }
    }
}
