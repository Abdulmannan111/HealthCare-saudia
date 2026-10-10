using AutoMapper;
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
using System.Text;
using System.Threading.Tasks;
using Application.Entities.ApiSettings;
using Microsoft.Extensions.Options;

namespace Domain.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly MongoDBContext _context;
        private readonly string _baseUrl;
        private IMapper mapper;

        public RegionRepository(IOptions<ApiSettings> apiSettings, MongoDBContext database , IMapper _mapper)
        {
            _context = database;
            mapper = _mapper;
            _baseUrl = apiSettings.Value.BaseUrl + "Regions/GetAll";
        }

        public RegionDto GetByIdAsync(string id)
        {
            //var region =  _context._regions.Find(r => r.ID.ToString() == id);

            //var regionData =  _context._regions.Find(r => r.ID.ToString() == id).FirstOrDefaultAsync();
            var regionData = _context._regions
                                .Find(r => r.ID.ToString() == id)
                                .FirstOrDefault();
            var regions  = mapper.Map<RegionDto>(regionData);
            return regions;
        }

        public async Task<ApiResponse<RegionDto>> GetAllAsync(int page, int pageSize, string submission_for)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;
            var skip = (page - 1) * pageSize;

            var filter = Builders<Regions>.Filter.And(
                Builders<Regions>.Filter.Eq(r => r.phcRegion, false),
                Builders<Regions>.Filter.Eq(r => r.deleted_at, null)
            );

            if (!string.IsNullOrEmpty(submission_for) && submission_for.ToLower() == "hajj inspection")
            {
                var allowedRegions = new[] { "Jeddah", "Makkah", "Madina", "Taif" };

                var regionFilters = allowedRegions.Select(region =>
                    Builders<Regions>.Filter.Regex(r => r.name, new BsonRegularExpression($"^{region}$", "i"))
                );

                var combinedRegionFilter = Builders<Regions>.Filter.Or(regionFilters);

                filter = Builders<Regions>.Filter.And(filter, combinedRegionFilter);
            }

            var regions = await _context._regions
                            .Find(filter)
                            .SortByDescending(r => r.CreatedDate)
                            .Skip(skip)
                            .Limit(pageSize)
                            .ToListAsync();

            var mapRegions = mapper.Map<List<RegionDto>>(regions);

            var totalRecords = await _context._regions.CountDocumentsAsync(filter);
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return new ApiResponse<RegionDto>
            {
                current_page = page,
                data = mapRegions.ToList(),
                first_page_url = $"{_baseUrl}?page=1",
                from = (page - 1) * pageSize + 1,
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
                per_page = pageSize,
                prev_page_url = page > 1 ? $"{_baseUrl}?page={page - 1}" : null,
                to = Math.Min(page * pageSize, (int)totalRecords),
                total = (int)totalRecords
            };
        }



        public async Task CreateAsync(RegionDto region)
        {
            var allRegions = await _context._regions.Find(x=> x.name==region.name && x.deleted_at == null).ToListAsync();
            if(allRegions.Any())
            {
                throw new Exception("The name has already been taken.");
            }
            var mapRegions = mapper.Map<Regions>(region);
            await _context._regions.InsertOneAsync(mapRegions);
        }

        public async Task UpdateAsync(string id, RegionDto region)
        {         
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentException("Invalid ObjectId format.", nameof(id));
            }
            var allRegions = await _context._regions.Find(x => x.name == region.name).ToListAsync();
            if (allRegions.Any())
            {
                throw new Exception("The name has already been taken.");
            }
            var mapRegions = mapper.Map<Regions>(region);
            mapRegions.ID = objectId;
            await _context._regions.ReplaceOneAsync(r => r.ID.ToString() == id, mapRegions);
        }

        public async Task DeleteAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<Regions>.Filter.Eq(c => c.ID, objectId);
            var update = Builders<Regions>.Update
                .Set(c => c.deleted_at, DateTime.Now)
                .Set(c => c.UpdatedDate, DateTime.UtcNow);

            await _context._regions.UpdateOneAsync(filter, update);
        }

        public async Task<List<RegionDto>> GetRegionsByMethodAsync(string method)
        {
            List<RegionDto> data1 = new List<RegionDto>();

            if (method == "Primary health care")
            {
                var data = await _context._regions.Find(r => r.phcRegion).ToListAsync();
                data1 = mapper.Map<List<RegionDto>>(data);
            }
            else if (method == "Pharmacy")
            {
                var data = await _context._regions.Find(r => !r.phcRegion).ToListAsync();
                data1 = mapper.Map<List<RegionDto>>(data);
            }

            return data1; 
        }


        public async Task<List<RegionDto>> GetRegions()
        {
            List<RegionDto> data1 = new List<RegionDto>();          
            var data = await _context._regions.Find(r => r.phcRegion).ToListAsync();
            data1 = mapper.Map<List<RegionDto>>(data);                    
            return data1;
        }

        public List<Regions> GetRegionsByIds(List<string> regionIds)
        {
            return _context._regions
                       .Find(r => regionIds.Contains(r.ID.ToString()))
                       .ToList();
        }
    }
}