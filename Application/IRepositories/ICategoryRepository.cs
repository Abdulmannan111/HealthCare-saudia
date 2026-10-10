using Application.DTOs.Category;
using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ICategoryRepository
    {
        Task<CategoryDto> GetCategoryByIdAsync(string id);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    }
}
