using Application.Entities;
using Application.IRepositories;
using Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly MongoDBContext _context;
        public ActivityLogRepository(MongoDBContext context)
        {
                _context = context;
        }
        public async Task LogModificationAsync(ActivityLog history)
        {
            await _context._ActivityLog.InsertOneAsync(history);
        }
    }
}
