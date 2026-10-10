using Application.DTOs;
using Application.Entities.Facility;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Entities
{
    [BsonIgnoreExtraElements]
    public class Users : AuditInfoComplete
    {
        public string IdAsString => ID.ToString();
        [Required(ErrorMessage = "Name is a required field.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        [BsonElement("name")]
        public string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is a required field.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [EmailFormat(ErrorMessage = "Invalid email format. Please enter a valid email address.")]
        [UniqueEmail(ErrorMessage = "Email has already been taken.")]
        [BsonElement("email")]
        public string email { get; set; } = string.Empty;

        [BsonElement("password")]
        public string? password { get; set; }

        [BsonElement("gender")]
        public string? gender { get; set; }

        [StringLength(50)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [BsonElement("type")]
        public string type { get; set; } = string.Empty;

        [Required]
        [BsonElement("phone")]
        [BsonSerializer(typeof(PhoneSerializer))]
        public string phone { get; set; } = string.Empty;

        [BsonElement("module_ids")]
        public List<string> module_ids { get; set; } = new List<string>();

        [BsonElement("sector_id")]
        public string? sector_id { get; set; }

        [BsonElement("sub_region_id")]
        public string sub_region_id { get; set; } = string.Empty;

        [BsonElement("sub_sector_id")]
        public string sub_sector_id { get; set; } = string.Empty;

        [BsonElement("lang")]
        public string lang { get; set; } = string.Empty;

        [BsonElement("refreshToken")]
        public string refreshToken { get; set; } = string.Empty;

        [BsonElement("refreshToken_at")]
        public DateTime? RefreshTokenAt { get; set; }

        [BsonElement("category")]
        public string category { get; set; } = string.Empty;
      
        [BsonElement("default_region_id")]
        public string default_region_id { get; set; } = string.Empty;

        [BsonElement("nupco_clusters_ids")]
        public List<string> nupco_clusters_ids { get; set; } = new List<string>();

        [BsonElement("clusters")]
        public List<Clusters>? clusters { get; set; } = new List<Clusters>();
        [BsonElement("obligation_sites")]
        public List<Facilities>? facilities { get; set; } = new List<Facilities>();

        [BsonElement("subRegions")]
        public List<SubRegionsDto>? subRegions { get; set; } = new List<SubRegionsDto>();

        [BsonElement("subSector")]
        public List<SubSectorDto>? subSector { get; set; } = new List<SubSectorDto>();


        [BsonElement("department")]
        public string department { get; set; } = string.Empty;

        [BsonElement("region_ids")]
        public List<string>? region_ids { get; set; } = new List<string>();

        [BsonElement("obligation_sites_ids")]
        public List<BsonValue> obligation_sites_ids_ { get; set; } = new List<BsonValue>();

        [BsonIgnore]
        public List<string>? obligation_sites_ids
        {
            get
            {
                var result = new List<string>();
                if (obligation_sites_ids_ == null || !obligation_sites_ids_.Any())
                {
                    return result; // Return null if the list is empty or null
                }

                

                foreach (var item in obligation_sites_ids_)
                {
                    if (item.IsString)
                    {
                        result.Add(item.AsString); // Add the string if it's of type BsonString
                    }
                    else if (item.IsInt32)
                    {
                        result.Add(item.AsInt32.ToString()); // Convert BSON DateTime to string in ISO 8601 format
                    }
                    else
                    {
                        throw new Exception("Unexpected type for obligation_sites_ids element");
                    }
                }

                return result;
            }
            set
            {
                if (value == null || !value.Any())
                {
                    obligation_sites_ids_ = new List<BsonValue>(); // Reset to an empty list
                    return;
                }

                obligation_sites_ids_ = value.Select(v => (BsonValue)v).ToList();
            }
        }


        [BsonElement("modules")]
        public List<Modules> modules { get; set; } = new List<Modules>();

        [BsonElement("nupco_site_cluster_ids")]
        public List<string> nupco_site_cluster_ids { get; set; } = new List<string>();
        [BsonElement("cluster_ids")]
        public List<string> clusters_ids { get; set; } = new List<string>();

        [BsonElement("otp")]
        public string otp { get; set; } = string.Empty;

        [BsonElement("otp_time")]
        public DateTime? otp_time { get; set; }

        [BsonElement("active")]
        public bool active { get; set; }

        [BsonElement("status")]
        public string status { get; set; } = string.Empty;

        [BsonElement("SME")]
        public bool SME {  get; set; }

        [BsonElement("SME_regions")]
        public List<SmeRegions>? SME_regions { get; set; } = new List<SmeRegions>();

        [BsonElement("deleted_at")]
        public DateTime? deleted_at { get; set; }

        [BsonElement("regions")]
        public List<Regions>? region { get; set; }

        [BsonElement("profile_pic")]
        public string? ProfilePic { get; set; }

    }
}
