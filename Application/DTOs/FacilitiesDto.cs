using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class FacilitiesDto
    {
        public string IdAsString { get; set; } = string.Empty;
        public string _id => IdAsString;
        public string name { get; set; } = string.Empty;
        public string name_ar { get; set; } = string.Empty;
    }
}
