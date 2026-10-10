using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PermissionDto
    {
        public string _id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Version { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string createdFrom { get; set; } = string.Empty;
        public string updatedFrom { get; set; } = string.Empty;        
    }
}
