namespace Application.Entities
{
    public class LoginRequest
    {
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public bool captcha { get; set; }
        public string otp { get; set; } = string.Empty;
        public bool otpState { get; set; }
    }
}
