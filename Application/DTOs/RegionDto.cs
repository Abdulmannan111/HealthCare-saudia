using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RegionDto
    {
        public string IdAsString { get; set; } = string.Empty;     
        public string _id => IdAsString;     
        public string name { get; set; } = string.Empty;
        public bool phcRegion { get; set; }
        public List<string> userIDs { get; set; } = new List<string>();
        public List<string> selfAssessmentScheduleIds { get; set; } = new List<string>();
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
        public DateTime? deleted_at { get; set; } 
        public int? Version { get; set; }
    }
}
