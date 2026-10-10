using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class UserToken : AuditInfoComplete
    {
        public string UserId { get; set; } = string.Empty;
        public string FcmToken { get; set; } = string.Empty;    
        public DateTime UpdatedAt { get; set; }
    }
}
