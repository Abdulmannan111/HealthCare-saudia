using Application.DTOs.User;
using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class activity_logDto
    {
        public string _id { get; set; } = string.Empty;
        public string log_name { get; set; } = string.Empty;
        public string properties { get; set; } = string.Empty;
        public string causer_id { get; set; } = string.Empty;
        public string causer_type { get; set; } = string.Empty;
        public string batch_uuid { get; set; } = string.Empty;
        public string event_ { get; set; }= string.Empty;
        public string subject_id { get; set; } = string.Empty;
        public string subject_type { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserDto? causer { get; set; }

    }

    public class PropertiesDto
    {
        public AttributesDto attributes { get; set; }
    }

    public class AttributesDto
    {
        public string standard { get; set; }
        public DateTime updated_at { get; set; }
    }
}
