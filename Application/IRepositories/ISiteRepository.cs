using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ISiteRepository 
    {
        Task<Sites> CreateAsync(Sites site);
         
        Task<Sites> GetById(string id);
    }
}
