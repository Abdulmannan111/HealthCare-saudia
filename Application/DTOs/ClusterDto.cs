using Application.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ClusterDto
    {
        public string IdAsString { get; set; } = string.Empty;
        public string _id => IdAsString;
        public string name { get; set; } = string.Empty;
        public string region_id { get; set; } = string.Empty;
        public string sector { get; set; } = string.Empty;
        public bool is_active { get; set; }
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime? deleted_at { get; set; } 
        public List<string> user_ids { get; set; } = new List<string>();
        public List<string> self_assessment_schedule_ids { get; set; } = new List<string>();
        public RegionDto? region { get; set; }
        public int Version { get; set; }

    }
}
