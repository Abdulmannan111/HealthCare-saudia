using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RiskCompliance
{
    public class RiskComplianceRequestDto
    {

        public string category { get; set; } // category field (string representing ObjectId)
        public string healthcareDomain { get; set; } // healthcareDomain field (string representing ObjectId)
        public string? id { get; set; }  // id field (string)
        public string identifier { get; set; } // identifier field (string)
        public decimal? impact { get; set; } // impact field (decimal or null)
        public string legal_descriptions { get; set; } // legal_descriptions field (string)
        public decimal? likelihood { get; set; } // likelihood field (decimal or null)
        public int? prefixImpact { get; set; } // prefixImpact field (int or null)
        public int? prefixLikelihood { get; set; } // prefixLikelihood field (int or null)
        public decimal? risk { get; set; } // risk field (decimal or null)
        public decimal? score { get; set; } // score field (decimal or null)
        public string standard { get; set; } // standard field (string)
        public string subCategory { get; set; } // subCategory field (string representing ObjectId)
        public decimal? weightage { get; set; } // weightage field (decimal or null)
    }
}
