using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class ResultCount
    {
        [JsonPropertyName("ACTIVE")]
        public long ACTIVE { get; set; }
        [JsonPropertyName("INACTIVE")]
        public long INACTIVE { get; set; }
        [JsonPropertyName("PENDING")]
        public long PENDING { get; set; }
        public long REJECTED { get; set; }
        public long ALL { get; set; }
        public long deleted { get; set; }
        public long notDeleted { get; set; }
    }
}
