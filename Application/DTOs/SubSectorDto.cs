using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SubSectorDto
    {
        public string IdAsString { get; set; } = string.Empty;
        public string _id => IdAsString;
        public string name { get; set; } = string.Empty;
        public string sector_id { get; set; } = string.Empty;
        public DateTime? deleted_at { get; set; }

    }
}
