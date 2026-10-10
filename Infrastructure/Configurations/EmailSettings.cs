using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class EmailSettings
    {
        public string Name { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Dictionary<EmailTemplatePath, string> Paths { get; set; } = new Dictionary<EmailTemplatePath, string>();

    }

    public enum EmailTemplatePath
    {
        EmailCreationPath // Add more paths if needed in the future
    }
}
