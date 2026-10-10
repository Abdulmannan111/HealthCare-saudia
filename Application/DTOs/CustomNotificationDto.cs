using Application.DTOs.User;
using Application.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CustomNotificationDto
    {
        public string _id { get; set; } = string.Empty;
        public string subject { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
        public bool is_individual { get; set; }
        public string user_id { get; set; } = string.Empty;
        public List<string> user_emails { get; set; } = new List<string>();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int? Version { get; set; }
        public List<string> user_roles { get; set; } = new List<string>();
        public UserDto? user { get; set; }
    }
}
