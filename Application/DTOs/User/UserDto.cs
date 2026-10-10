using System.ComponentModel.DataAnnotations;
using Application.DTOs;
using Application.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.DTOs.User
{
    public class UserDto
    {
        public string IdAsString { get; set; } = string.Empty;
        public string _id => IdAsString;
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; } = string.Empty;
        public string? gender { get; set; }

        public List<string> region_ids { get; set; } = new List<string>();
        public List<string>? region_id { get; set; } = new List<string>();
        public List<RegionDto> region { get; set; } = new List<RegionDto>();
        public bool IsActive { get; set; }
        public string status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public string phone { get; set; } = string.Empty;
        public int Version { get; set; } = 5;
        public List<SmeRegionsDto>? SME_regions { get; set; } = new List<SmeRegionsDto>();
        public List<ModuleDto> modules { get; set; } = new List<ModuleDto>();
        public List<string> module_ids { get; set; } = new List<string>();
        public List<string>? obligation_sites_ids { get; set; } = new List<string>();
      //  public object? lastLogin { get; set; }
        public string otp { get; set; } = string.Empty;
        public DateTime? otp_time { get; set; }
        public string refreshToken { get; set; } = string.Empty;
        public string? password { get; set; } = string.Empty;
        public string? type { get; set; } = string.Empty;
        public bool SME { get; set; }
        public string default_region_id { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string lang { get; set; } = string.Empty;
        public DateTime? RefreshTokenAt { get; set; }
        public string category { get; set; } = string.Empty;
        public List<string> nupco_clusters_ids { get; set; } = new List<string>();
        public List<string> clusters_ids { get; set; } = new List<string>();
        public List<string> cluster_id { get; set; } = new List<string>();
        public List<string> facility_id { get; set; } = new List<string>();
        public string? sector_id { get; set; }
        public string sub_region_id { get; set; } = string.Empty;
        public string sub_sector_id { get; set; } = string.Empty;
        public string department { get; set; } = string.Empty;
        public List<string> nupco_site_cluster_ids { get; set; } = new List<string>();
        public List<ClusterDto>? clusters { get; set; } = new List<ClusterDto>();
        public List<SubRegionsDto>? subRegions { get; set; } = new List<SubRegionsDto>();
        public List<SubSectorDto>? subSector { get; set; } = new List<SubSectorDto>();

        public List<FacilitiesDto>? facilities { get; set; } = new List<FacilitiesDto>();
        public bool active { get; set; }
        public bool isNupcoUser { get; set; }
        public DateTime? deleted_at { get; set; }
        public string? ProfilePic { get; set; }

        // Role-based methods
        public bool IsAdmin() => type?.Equals("admin", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsSupervisor() => type?.Equals("supervisor", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsRegional() => type?.Equals("regional representatives", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsAgent() => type?.Equals("agent", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsFacility() => type?.Equals("facility", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsFacilityManager() => type?.Equals("facility manager", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsPharmacist() => type?.Equals("pharmacist", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsAscendTeam() => type?.Equals("ascend team", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsClusterManager() => type?.Equals("cluster manager", StringComparison.OrdinalIgnoreCase) == true;
        public bool IsExpert() => type?.Equals("expert", StringComparison.OrdinalIgnoreCase) == true;
        public bool isHoldCo() => type?.Equals("hold_co", StringComparison.OrdinalIgnoreCase) == true;

        public bool IsSelfSubmissions() =>
            IsAgent() || IsPharmacist() || IsAscendTeam() || IsFacility() || IsExpert();

        public bool IsTopThree() =>
            IsAdmin() || IsRegional() || IsSupervisor();

    }

}
