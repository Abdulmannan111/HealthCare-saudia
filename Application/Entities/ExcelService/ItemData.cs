using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.ExcelService
{
    public class ItemData
    {
        //public string ProjectId { get; set; }
        //public string Id { get; set; }
        //public string Region { get; set; }
        //public string Site { get; set; }
        //public string City { get; set; }
        //public string ReasonForVisiting { get; set; }
        //public string VisitDate { get; set; }
        //public string AvailableOnGround { get; set; }
        //public List<object> SupportIds { get; set; }
        //public List<object> StaffAvailability { get; set; }
        //public string H11hfpi1yq3 { get; set; }
        //public List<InspectionItemData> Ln8bz6rin3x { get; set; }
        //public string OtherData { get; set; }

        public int? No { get; set; }
        public string? ReportNo { get; set; }
        public string? SchemaVersion { get; set; }
        public string? Status { get; set; }
        public string? Site { get; set; }
        public string? Cluster { get; set; }
        public string? Region { get; set; }
        public string? InspectionType { get; set; }
        public List<string> StepName { get; set; } = new List<string> { "N/A" };
        public List<string> QuestionNumber { get; set; } = new List<string> { "N/A" };
        public List<string> Standard { get; set; } = new List<string> { "N/A" };
        public List<string> Category { get; set; } = new List<string> { "N/A" };
        public List<string> HealthcareQuality { get; set; } = new List<string> { "N/A" };
        public List<string> Likelihood { get; set; } = new List<string> { "N/A" };
        public List<string> Impact { get; set; } = new List<string> { "N/A" };
        public List<string> RiskValue { get; set; } = new List<string> { "N/A" };
        public List<string> Answer { get; set; } = new List<string> { "N/A" };
        public List<string> NotComplyReason { get; set; } = new List<string> { "N/A" };
        public string? InspectorsData { get; set; }
        public string? OtherData { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserId { get; set; }
        public string? created_at { get; set; }


    }
}
