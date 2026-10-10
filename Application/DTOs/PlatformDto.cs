using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PlatformDto
    {
        public string? _id { get; set; }
        public string? name { get; set; }
        public string? name_ar { get; set; }
        public string? laravel_url { get; set; }
        public string? dotnet_url { get; set; }
        public string? frontend_url { get; set; }
        public string? icon { get; set; }
        public Dictionary<string, bool> config { get; set; } = new();
    }
}
