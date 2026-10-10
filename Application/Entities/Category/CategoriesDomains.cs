using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities.Domain;

namespace Application.Entities.Category
{
    public class CategoriesDomains
    {
        public List<Category> category { get; set; } = new List<Category>();
        public List<Domains> domain { get; set; } = new List<Domains>();
    }
}
