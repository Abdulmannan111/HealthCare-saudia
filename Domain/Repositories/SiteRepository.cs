using Application.Entities;
using Application.IRepositories;
using MongoDB.Bson;
using MongoDB.Driver;
using Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        private readonly MongoDBContext _context;

        public SiteRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<Sites> CreateAsync(Sites site)
        {
            await _context.sites.InsertOneAsync(site);
            return site;
        }

        public async Task<Sites> GetById(string id)
        {
            var data = await _context.sites.Find(s => s.ID.ToString() == id).FirstOrDefaultAsync();
            return data;
        }
    }
}
