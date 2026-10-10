using Application.Entities.Facility;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IFacilityTypeRepository
    {
        Task<IEnumerable<FacilityType>> GetAllAsync();
        Task<FacilityType> GetByIdAsync(string id);
        Task CreateAsync(FacilityType facilityType);
        Task UpdateAsync(string id, FacilityType facilityType);
        Task DeleteAsync(string id);

    }
}
