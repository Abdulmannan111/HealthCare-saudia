using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using Application.Entities.Form;

namespace Application.DTOs
{
    public class SmeRegionsDto
    {

        public string _id { get; set; } = string.Empty;
        public region region { get; set; } = new region();
        public string? start_date { get; set; }
    }
}
