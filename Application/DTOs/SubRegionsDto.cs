using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SubRegionsDto
    {
        public string IdAsString { get; set; } = string.Empty;
        public string _id => IdAsString;
        public string name { get; set; } = string.Empty;
        public string region_id { get; set; } = string.Empty;
        public DateTime? deleted_at { get; set; }
    }
}
