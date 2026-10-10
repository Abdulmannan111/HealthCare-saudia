using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Category
{
    public class CategoryDto
    {
        public string _id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public List<SubCategoryDto>? sub_category { get; set; }
    }
}
