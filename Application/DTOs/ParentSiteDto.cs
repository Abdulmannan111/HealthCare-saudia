using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ParentSiteDto
    {
        public string? _id { get; set; }
        public string? value => _id;
        public string? name { get; set; }
        public string? name_ar { get; set; }
        public string? LicenseNumber { get; set; }
        public string? street { get; set; }
    }

}
