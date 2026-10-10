using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.DTOs
{
    public class SiteOfficeDto
    {
        public string? _id { get; set; }
        public string? value => _id;
        public string? region_id { get; set; }
        public string? cluster_id { get; set; }
        public string? name { get; set; }

        public string? site_id { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public ParentSiteDto? obligation_sites { get; set; }
        public Region_Dto? region { get; set; } = new Region_Dto();
        public Cluster_Dto? Cluster { get; set; } = new Cluster_Dto();
        public class Region_Dto
        {
            public string _id { get; set; } = string.Empty;
            public string value => _id;
            public string label { get; set; } = string.Empty;
        }

        public class Cluster_Dto
        {
            public string _id { get; set; } = string.Empty;
            public string value => _id;
            public string label { get; set; } = string.Empty;

        }
    }
}
