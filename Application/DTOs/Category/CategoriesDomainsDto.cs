using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Category
{
    public class CategoriesDomainsDto
    {
        public List<CategoryDto> category { get; set; } = new List<CategoryDto>();
        public List<DomainDto> domain { get; set; } = new List<DomainDto>();
    }
}
