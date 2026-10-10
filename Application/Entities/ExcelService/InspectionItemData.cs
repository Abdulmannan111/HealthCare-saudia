using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.ExcelService
{
    public class InspectionItemData
    {
        public int No { get; set; }
        public string Step { get; set; }
        public string Title { get; set; }
        public string HealthcareQuality { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int Impact { get; set; }
        public int Likelihood { get; set; }
        public bool Required { get; set; }
        public string Id { get; set; }
        public int LikelihoodPostfix { get; set; }
        public int ImpactPrefix { get; set; }
        public double Score { get; set; }
        public double Weightage { get; set; }
        public double Risk { get; set; }
        public string Identifier { get; set; }
        public bool IsUpdated { get; set; }
        public string Question { get; set; }
        public string Reason { get; set; }
        public List<FileData> Files { get; set; } // Assuming files are objects that you want to process
    }
}
