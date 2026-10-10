using Application.DTOs;
using Application.IRepositories;
using Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private MongoDBContext _mongoContext;   
        public SubCategoryRepository(MongoDBContext mongoDBContext)
        {
            _mongoContext = mongoDBContext;
        }
        Task<IEnumerable<SubCategoryDto>> ISubCategoryRepository.GetAllSubCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        Task<SubCategoryDto> ISubCategoryRepository.GetSubCategoryByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
