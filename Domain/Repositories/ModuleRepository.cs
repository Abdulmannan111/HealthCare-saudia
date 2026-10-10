using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using Application.Pagination;
using MongoDB.Bson;
using MongoDB.Driver;
using Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Entities.ApiSettings;
using Microsoft.Extensions.Options;

namespace Domain.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly MongoDBContext _context;
        private IMapper _mapper;
        private readonly string _baseUrl;

        public ModuleRepository(IOptions<ApiSettings> apiSettings, MongoDBContext context, IMapper mapper)
        {
               _mapper = mapper;
              _context = context;
            _baseUrl = apiSettings.Value.BaseUrl + "modules/module-index";
        }
        public async Task<List<Modules>> GetAllModulesAsync(string id)
        {
            var User = await _context.Users.Find(x => x.ID.ToString() == id ).FirstOrDefaultAsync();
            if (User == null)
            {
                return new List<Modules>(); 
            }

            var moduleIds = User.module_ids;

            if (moduleIds == null || !moduleIds.Any())
            {
                return new List<Modules>();
            }
            var modules = await _context._modules.Find(m => moduleIds.Contains(m.ID.ToString()))
            .ToListAsync();

            return modules;
        }

        public async Task<ModuleDto> GetByIdAsync(string id)
        {
            var data = await _context._modules.Find(m => m.ID.ToString() == id).FirstOrDefaultAsync();
            var mapModules = _mapper.Map<ModuleDto>(data);
            return mapModules;

        }

        public async Task<ApiResponse<ModuleDto>> GetAllAsync(int page , int per_page , string? search)
        {
            if (page <= 0) page = 1;
            if (per_page <= 0) per_page = 10;

            var skip = (page - 1) * per_page;
            var modules = new List<Modules>();
             modules = await _context._modules.Find(x => x.deleted_at == null)
                                                 .SortByDescending(x => x.CreatedDate)
                                                 .Skip(skip)
                                                 .Limit(per_page)
                                                 .ToListAsync();
            var totalRecords = await _context._modules.CountDocumentsAsync(x => x.deleted_at == null);
            var totalPages = (int)Math.Ceiling(totalRecords / (double)per_page);
            var mapModules = _mapper.Map<List<ModuleDto>>(modules);

            return new ApiResponse<ModuleDto>
            {
                current_page = page,
                data = mapModules.ToList(),
                first_page_url = $"{_baseUrl}?page=1",
                from = (page - 1) * per_page + 1,
                last_page = totalPages,
                last_page_url = $"{_baseUrl}?page={totalPages}",
                links = new List<Link>
        {
            new Link
            {
                url = page > 1 ? $"{_baseUrl}?page={page - 1}" : null,
                label = "&laquo; Previous",
                active = false
            },
            new Link
            {
                url = page < totalPages ? $"{_baseUrl}?page={page + 1}" : null,
                label = "Next &raquo;",
                active = false
            }
        },
                next_page_url = page < totalPages ? $"{_baseUrl}?page={page + 1}" : null,
                path = _baseUrl,
                per_page = per_page,
                prev_page_url = page > 1 ? $"{_baseUrl}?page={page - 1}" : null,
                to = Math.Min(page * per_page, (int)totalRecords),
                total = (int)totalRecords
            };
        }


        public async Task CreateAsync(ModuleDto module)
        {
            var mapModules = _mapper.Map<Modules>(module);
            await _context._modules.InsertOneAsync(mapModules);
           
        }

        public async Task UpdateAsync(string id, ModuleDto module)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentException("Invalid ObjectId format.", nameof(id));
            }

            // Map the DTO to the entity
            var mapModules = _mapper.Map<Modules>(module);

            // Ensure the ObjectId is set to the mapped module
            mapModules.ID = objectId;

            // Update or insert the document
            var result = await _context._modules.ReplaceOneAsync(
                m => m.ID == objectId,
                mapModules,
                new ReplaceOptions { IsUpsert = true } // Ensure upsert if the document doesn't exist
            );

            if (result.ModifiedCount == 0 && result.MatchedCount == 0)
            {
                throw new Exception("No documents matched the provided ID.");
            }
        }


        public async Task DeleteAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<Modules>.Filter.Eq(c => c.ID, objectId);
            var update = Builders<Modules>.Update
                .Set(c => c.deleted_at, DateTime.Now)
                .Set(c => c.UpdatedDate, DateTime.UtcNow);
            await _context._modules.UpdateOneAsync(filter, update);
        }

        private string GenerateSlug(string name)
        {
            // Implement slug generation logic here
            return name.ToLower().Replace(" ", "-");
        }

        public List<Modules> GetModulesByIds(List<string> moduleIds)
        {
            return _context._modules
                .Find(module => moduleIds.Contains(module.IdAsString))
                .ToList();
        }
    }
}
