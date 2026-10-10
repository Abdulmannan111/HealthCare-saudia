using Application.DTOs.Form;
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
    public class ModuleDto
    {
        public string _id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<string> UserIds { get; set; } = new List<string>();
        public FormOptionsDto? form_options { get; set; }
        public string Slug { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? created_at => CreatedDate;
        public bool active { get; set; }
        public string reference_name { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
