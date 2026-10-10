using Application.DTOs.Category;
using Application.IRepositories;
using Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private MongoDBContext _mongoContext;
        public CategoryRepository(MongoDBContext mongoContext)
        {
                _mongoContext = mongoContext;
        }
        Task<IEnumerable<CategoryDto>> ICategoryRepository.GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        Task<CategoryDto> ICategoryRepository.GetCategoryByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
