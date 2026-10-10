using Application.IRepositories;
using MongoDB.Bson;
using MongoDB.Driver;
using Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Application.Entities.Facility;

namespace Domain.Repositories
{
    public class FacilityTypeRepository : IFacilityTypeRepository
    {
        private readonly MongoDBContext _context;

        public FacilityTypeRepository(MongoDBContext context)
        {
           _context = context;
        }

        public async Task<IEnumerable<FacilityType>> GetAllAsync()
        {
            return await _context._facilityTypes.Find(f => true).ToListAsync();
        }

        public async Task<FacilityType> GetByIdAsync(string id)
        {
            return await _context._facilityTypes.Find(f => f.ID.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(FacilityType facilityType)
        {
            await _context._facilityTypes.InsertOneAsync(facilityType);
        }

        public async Task UpdateAsync(string id, FacilityType facilityType)
        {
            await _context._facilityTypes.ReplaceOneAsync(f => f.ID.ToString() == id, facilityType);
        }

        public async Task DeleteAsync(string id)
        {
            await _context._facilityTypes.DeleteOneAsync(f => f.ID.ToString() == id);
        }
    }
}
