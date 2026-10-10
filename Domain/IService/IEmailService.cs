

namespace Domain.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<string> recipients, string subject, string body);
        Task<string> GenerateUserCreationEmail(string name, string email, string password, string emailSettings);
    }

}
