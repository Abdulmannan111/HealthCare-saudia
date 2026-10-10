using Application.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IDomainRepository
    {
        Task<Domains> GetDomainByIdAsync(string id);
        Task<IEnumerable<Domains>> GetAllDomainsAsync();
    }
}
