using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TicketSlasDto
    {
        public string _id { get; set; }
        public string? FormId { get; set; }  // Unique
        public Dictionary<string, int>? PriorityLevels { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
