using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RoleDto
    {
        public string _id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public List<string> health_directorate { get; set; } = new List<string>();
        public List<string> responsible_department { get; set; } = new List<string>();
        public List<string> permission_ids { get; set; } = new List<string>();
    }
}
