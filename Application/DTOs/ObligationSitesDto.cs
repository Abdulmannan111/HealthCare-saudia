using MongoDB.Bson;

namespace Application.DTOs
{
    public class ObligationSitesDto
    {
        public string IdAsString { get; set; } = string.Empty;
        public string _id => IdAsString;
        public string name { get; set; } = string.Empty;
        public string name_ar { get; set; } = string.Empty;
        public string facilityType { get; set; } = string.Empty;
        public string? site_parent_id { get; set; }
        public string region_id { get; set; } = string.Empty;
        public string sub_region_id { get; set; } = string.Empty;
        public string sector_id { get; set; } = string.Empty;
        public string sub_sector_id { get; set; } = string.Empty;
        public int? batch_id { get; set; }

        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public string status { get; set; } = string.Empty;
        public RegionDto_? region { get; set; } = new RegionDto_();
        public SectorDto? Sector { get; set; }
        public Sub_regionDto? Sub_region { get; set; }
        public Sub_sectorDto? Sub_sector { get; set; }

    }
    public class RegionDto_
    {
        public string IdAsString { get; set; } = string.Empty;
        public string _id => IdAsString;
        public string name { get; set; } = string.Empty;
    }

    public class SectorDto
    {
        public string? _id { get; set; }
        public string name { get; set; } = string.Empty;
    }

    public class Sub_sectorDto
    {
        public string? _id { get; set; }
        public string name { get; set; } = string.Empty;
    }

    public class Sub_regionDto
    {
        public string? _id { get; set; }
        public string name { get; set; } = string.Empty;
    }

}
