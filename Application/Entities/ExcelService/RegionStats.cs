using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.ExcelService
{
    public class RegionStats
    {
        public HashSet<string> Users { get; set; }
        public int Comply { get; set; }
        public int NotComply { get; set; }
        public int Na { get; set; }
    }
}
