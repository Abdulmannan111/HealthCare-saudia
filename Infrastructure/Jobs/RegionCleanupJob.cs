using Microsoft.Extensions.Logging;
using Application.Entities;
using MongoDB.Driver;
using Quartz;
using Infrastructure.Mongo;
namespace Infrastructure.Jobs
{
    public class RegionCleanupJob : IJob
    {
        private readonly MongoDBContext _context;
        private readonly ILogger<RegionCleanupJob> _logger;

        public RegionCleanupJob(MongoDBContext context, ILogger<RegionCleanupJob> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
                _logger.LogInformation("Starting Region Cleanup Job at {Time}", DateTime.UtcNow);

                var users = await _context.Users.Find(_ => true).ToListAsync();

            foreach (var user in users)
            {
                if (user == null)
                {
                    _logger.LogInformation("User {UserId} not found", user.ID.ToString());
                    return;
                }

                if (user.SME_regions == null || !user.SME_regions.Any())
                {
                    _logger.LogInformation("User {UserId} has no SME regions to clean up", user.ID.ToString());
                    return;
                }

                var expiredRegions = user?.SME_regions
                    .Where(r => r._start_date.HasValue && r._start_date.Value < DateTime.UtcNow)
                    .Select(r => r.region._id)
                    .Where(regionId => user.region_ids.Contains(regionId) && user.default_region_id != regionId)
                    .ToList();

                var expiredSMERegions = user?.SME_regions
                    .Where(r => r._start_date.HasValue && r._start_date.Value < DateTime.UtcNow)
                    .Select(r => r.region._id)
                    .Where(regionId => user.region_ids.Contains(regionId))
                    .ToList();

                if (expiredRegions.Any() || expiredSMERegions.Any())
                {
                    var updateUser = Builders<Users>.Update
                        .PullAll(u => u.region_ids, expiredRegions)
                        .PullFilter(u => u.SME_regions, r => expiredSMERegions.Contains(r.region._id));

                    await _context.Users.UpdateOneAsync(u => u.ID == user.ID, updateUser);

                    var updateRegions = Builders<Regions>.Update.Pull("user_ids", user.ID);
                    await _context._regions.UpdateManyAsync(r => expiredRegions.Contains(r.ID.ToString()), updateRegions);

                    _logger.LogInformation("Removed expired SME_regions and region_ids for user {UserId}", user.ID);
                }
                else
                {
                    _logger.LogInformation("No expired regions found for user {UserId}", user.ID);
                }
            }

            _logger.LogInformation("Completed Region Cleanup Job at {Time}", DateTime.UtcNow);
        }
    }
}
