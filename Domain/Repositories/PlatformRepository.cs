using Application.DTOs;
using Application.Entities;
using Application.Entities.Facility;
using Application.IRepositories;
using AutoMapper;
using Infrastructure.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Domain.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;

        public PlatformRepository(MongoDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PlatformDto>> GetAllAsync()
        {
            var platforms = await _context._Platforms.Find(_ => _.deleted_at==null).ToListAsync();
            return _mapper.Map<List<PlatformDto>>(platforms);
        }

        public async Task<PlatformDto?> GetByIdAsync(string id)
        {
            var platform = await _context._Platforms.Find(x => x.ID.ToString() == id).FirstOrDefaultAsync();
            return _mapper.Map<PlatformDto>(platform);
        }

        public async Task CreateAsync(PlatformDto dto)
        {
            var entity = _mapper.Map<Platform>(dto);
            await _context._Platforms.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, PlatformDto dto)
        {
            var update = Builders<Platform>.Update
                .Set(x => x.name, dto.name)
                .Set(x => x.name_ar, dto.name_ar)
                .Set(x => x.dotnet_url, dto.dotnet_url)
                .Set(x => x.laravel_url, dto.laravel_url)
                .Set(x => x.frontend_url, dto.frontend_url)
                .Set(x => x.icon, dto.icon)
                //.Set(x => x.config, new BsonDocument(dto.config))
                .Set(x => x.UpdatedDate, DateTime.UtcNow);

            await _context._Platforms.UpdateOneAsync(x => x.ID.ToString() == id, update);
        }

        public async Task DeleteAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<Platform>.Filter.Eq(x => x.ID, objectId);
            var update = Builders<Platform>.Update
                .Set(c => c.deleted_at, DateTime.Now)
                .Set(c => c.UpdatedDate, DateTime.UtcNow);
            await _context._Platforms.UpdateOneAsync(filter, update);
        }
    }
}
