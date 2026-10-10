using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Form
{
    public class FormSubmissionsDto
    {
        public string? project_id { get; set; }

        public string id { get; set; } = string.Empty;
        public dynamic? data { get; set; }

        public string? user_id { get; set; }


        public string? status { get; set; }

        public int? schema_version { get; set; }


        public int? report_no { get; set; }
        public List<string>? support_ids { get; set; }

        public DateTime? inspection_date { get; set; }

        public DateTime? DeletedAt { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
