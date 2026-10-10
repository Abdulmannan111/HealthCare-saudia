using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class ChangeStatusRequest
    {
        public string id { get; set; }
        public bool status { get; set; }
    }
}
