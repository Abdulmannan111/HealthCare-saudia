using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.User;
using Application.Entities;
using Application.Entities.Facility;
using Application.IRepositories;
using Application.Pagination;
using AutoMapper;
using Infrastructure.Mongo;
using Mapster;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Domain.Repositories
{
    public class SiteOfficeRepository : ISiteOfficeRepository
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        public SiteOfficeRepository(MongoDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateSiteOfficeAsync(List<SiteOfficeDto> siteOfficeDtos)
        {
            try
            {
                var siteOfficeEntities = _mapper.Map<List<SiteOffice>>(siteOfficeDtos);
                var insertList = new List<SiteOffice>();

                foreach (var entity in siteOfficeEntities)
                {
                    // Build filter for the (name, site_id) combination
                    var filter = Builders<SiteOffice>.Filter.And(
                        Builders<SiteOffice>.Filter.Eq(o => o.name, entity.name),
                        Builders<SiteOffice>.Filter.Eq(o => o.site_id, entity.site_id)
                    );

                    var existingOffice = await _context._SiteOffice.Find(filter).FirstOrDefaultAsync();

                    if (existingOffice != null)
                    {
                        throw new Exception($"Office '{entity.name}' already exists for Site ID: {entity.site_id}");
                    }

                    entity.CreatedDate = entity.UpdatedDate = DateTime.UtcNow;
                    insertList.Add(entity);
                }

                if (insertList.Any())
                {
                    await _context._SiteOffice.InsertManyAsync(insertList);
                }
            }
            catch (Exception ex)
            {
                // Optional: log ex
                throw;
            }
        }



        public async Task<ApiResponse<SiteOfficeDto>> GetAllAsync(
    int page, int pageSize,
    string? search, string? region_id,
    string? facility, int? version, string? status)
        {
            try
            {

                pageSize = pageSize <= 0 ? 10 : pageSize;
                page = page <= 0 ? 1 : page;
                var skip = (page - 1) * pageSize;

                var filter = Builders<SiteOffice>.Filter.Eq(x => x.deleted_at, null);

                var siteOffices = await _context._SiteOffice
                    .Find(filter)
                    .SortByDescending(x => x.CreatedDate) // Sorting by creation date in descending order
                    .Skip(skip)
                    .Limit(pageSize)
                    .ToListAsync();

                if (!siteOffices.Any())
                    return new ApiResponse<SiteOfficeDto>();

                var siteOfficeDtos = _mapper.Map<List<SiteOfficeDto>>(siteOffices);
                foreach (var dto in siteOfficeDtos)
                {

                    var region = await _context._regions.Find(site => site.ID.ToString() == dto.region_id).FirstOrDefaultAsync();
                    var cluster = await _context._clusters.Find(site => site.ID.ToString() == dto.cluster_id).FirstOrDefaultAsync();
                    if (cluster != null)
                    {
                        dto.Cluster._id = cluster.ID.ToString();
                        dto.Cluster.label = cluster.name;
                    }
                    if (region != null)
                    {
                        dto.region._id = region.ID.ToString();
                        dto.region.label = region.name;
                    }
                    if (dto.site_id != string.Empty)
                    {
                        var obligation_site = await _context._ObligationSites.Find(site => site.ID.ToString() == dto.site_id).FirstOrDefaultAsync();
                        dto.obligation_sites = _mapper.Map<ParentSiteDto>(obligation_site);
                    }

                }
                // Now filter in-memory
                if (!string.IsNullOrWhiteSpace(search))
                {
                    siteOfficeDtos = siteOfficeDtos.Where(dto =>
                        (!string.IsNullOrEmpty(dto.name) && dto.name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(dto.region?.label) && dto.region.label.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(dto.Cluster?.label) && dto.Cluster.label.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(dto.obligation_sites?.name) && dto.obligation_sites.name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(dto.obligation_sites?.name_ar) && dto.obligation_sites.name_ar.Contains(search, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }
                // Count total records
                var totalRecords = await _context._SiteOffice.CountDocumentsAsync(filter);
                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                return new ApiResponse<SiteOfficeDto>
                {
                    current_page = page,
                    data = siteOfficeDtos,
                    from = (page - 1) * pageSize + 1,
                    last_page = totalPages,

                    per_page = pageSize,
                    to = Math.Min(page * pageSize, (int)totalRecords),
                    total = (int)totalRecords
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        

        public async Task UpdateSiteOfficeAsync(string id, SiteOfficeDto siteOffice)
        {
            var update = Builders<SiteOffice>.Update
                .Set(x => x.name, siteOffice.name)
                .Set(x => x.site_id, siteOffice.site_id)
                .Set(x => x.region_id, siteOffice.region_id)
                .Set(x => x.cluster_id, siteOffice.cluster_id)
                .Set(x=> x.UpdatedDate, DateTime.UtcNow);
            await _context._SiteOffice.UpdateOneAsync(x => x.ID.ToString() == id, update);
        }
        public async Task DeleteAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<SiteOffice>.Filter.Eq(x => x.ID, objectId);
            var update = Builders<SiteOffice>.Update
                .Set(c => c.deleted_at, DateTime.Now)
                .Set(c => c.UpdatedDate, DateTime.UtcNow);
            await _context._SiteOffice.UpdateOneAsync(filter, update);
        }
        public async Task<SiteOfficeDto> GetByIdAsync(string id)
        {
            var siteoffice= await _context._SiteOffice.Find(x => x.ID.ToString() == id).FirstOrDefaultAsync();
            siteoffice.obligation_sites = await _context._ObligationSites.Find(site => site.ID.ToString() == siteoffice.site_id).FirstOrDefaultAsync();
            var region = await _context._regions.Find(site => site.ID.ToString() == siteoffice.region_id).FirstOrDefaultAsync();
            var cluster = await _context._clusters.Find(site => site.ID.ToString() == siteoffice.cluster_id).FirstOrDefaultAsync();

            var siteOfficeDto = _mapper.Map<SiteOfficeDto>(siteoffice);
            if(cluster!=null)
            {
                siteOfficeDto.Cluster._id = cluster.ID.ToString();
                siteOfficeDto.Cluster.label = cluster.name;
            }
            if(region!=null)
            {
                siteOfficeDto.region._id = region.ID.ToString();
                siteOfficeDto.region.label = region.name;
            }
            return siteOfficeDto;
        }
        
    }
}
