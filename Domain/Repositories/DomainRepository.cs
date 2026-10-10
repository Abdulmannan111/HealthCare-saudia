using Application.Entities.Domain;
using Application.IRepositories;
using Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class DomainRepository : IDomainRepository
    {
        private MongoDBContext _dbContext;
        public DomainRepository(MongoDBContext dbContext)
        {
                _dbContext = dbContext;
        }
        Task<IEnumerable<Domains>> IDomainRepository.GetAllDomainsAsync()
        {
            throw new NotImplementedException();
        }

        Task<Domains> IDomainRepository.GetDomainByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
