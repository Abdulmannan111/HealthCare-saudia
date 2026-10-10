using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class FirebaseMessage
    {
        public string To { get; set; } = string.Empty;  
        public Notification? Notification { get; set; }
    }
}
