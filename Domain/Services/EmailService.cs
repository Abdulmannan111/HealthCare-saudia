using MimeKit;
using MailKit.Security;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using RazorLight;
using Domain.Services;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(List<string> recipients, string subject, string body)
        {
            try
            {
                // Create the email message template
                var emailMessage = new MimeMessage();

                // Set sender email
                emailMessage.From.Add(new MailboxAddress(_emailSettings.Name, _emailSettings.Username));

                // Create the body of the email
                var bodyBuilder = new BodyBuilder { HtmlBody = body };
                emailMessage.Body = bodyBuilder.ToMessageBody();
                emailMessage.Subject = subject;

                // Using SMTP client for email sending
                using var client = new MailKit.Net.Smtp.SmtpClient();
                await client.ConnectAsync(_emailSettings.SmtpServer, 465, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);

                foreach (var recipient in recipients)
                {
                    try
                    {
                        // Set the recipient for this email
                        emailMessage.To.Clear();  // Clear any previous recipients
                        emailMessage.To.Add(new MailboxAddress("", recipient));

                        // Send the email to the current recipient
                        await client.SendAsync(emailMessage);
                        Console.WriteLine($"Email sent to: {recipient}");

                        // Wait for 5 seconds before sending to the next recipient
                        await Task.Delay(5000);  // 5000 milliseconds = 5 seconds
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send email to {recipient}: {ex.Message}");
                    }
                }

                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
            }
        }


        public async Task<string> GenerateUserCreationEmail(string name, string email, string password, string emailSettings)
        {
            try
            {
                // Get the current directory of the application
                string basePath = Directory.GetCurrentDirectory();

                // Remove the 'Al-Munazam' part from the base path
                string trimmedBasePath = "";

                // Now, build the correct path to the EmailTemplates folder
                string templatePath = Path.Combine(trimmedBasePath, emailSettings);
                var engine = new RazorLightEngineBuilder()
                    .UseEmbeddedResourcesProject(typeof(Program))
                    .UseMemoryCachingProvider()
                    .Build();

                // Read the HTML template
                string emailTemplate = await File.ReadAllTextAsync(templatePath);

                // Render the template with actual data
                string emailBody = await engine.CompileRenderStringAsync("user_creation_template", emailTemplate, new
                {
                    Name = name,
                    Email = email,
                    Password = password
                });

                return emailBody;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}