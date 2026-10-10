using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using Application.Pagination;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using Infrastructure.Mongo;
using System.Collections.Immutable;
using System.Numerics;
using System.Text.Json;
using Application.DTOs.User;
using Application.Entities.ApiSettings;
using Microsoft.Extensions.Options;
using static System.Collections.Specialized.BitVector32;
using System.ComponentModel;
using System.Xml.Linq;
using System;

namespace Domain.Repositories
{
    public class FacilitySiteRepository : IFacilitySiteRepository
    {
        private readonly MongoDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;
        private readonly IRegionRepository _regionRepository;
        private readonly IClusterRepository _clusterRepository;
        private readonly string _baseUrl;
        public FacilitySiteRepository(IOptions<ApiSettings> apiSettings, MongoDBContext context, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IMapper mapper, IRegionRepository regionRepository, IClusterRepository clusterRepository)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
            _regionRepository = regionRepository;
            _clusterRepository = clusterRepository;
            _baseUrl = apiSettings.Value.BaseUrl + "FacilitySite/GetAllFacilitySites";
        }

        public async Task<ResultCount> GetCounts()
        {
            var activeCount = await _context._ObligationSites.CountDocumentsAsync(x =>
            x.status == "active" &&
            x.deleted_at == null &&
            (x.Version == 5 || x.Version.ToString().Contains("5")));
            var inactiveCount = await _context._ObligationSites.CountDocumentsAsync(x => 
            x.status == "in_active" && 
            x.deleted_at == null &&
            (x.Version == 5 || x.Version.ToString().Contains("5")));
            var pendingCount = await _context._ObligationSites.CountDocumentsAsync(x => 
            x.status == "pending" && 
            x.deleted_at == null &&
            (x.Version == 5 || x.Version.ToString().Contains("5")));
            var rejectedCount = await _context._ObligationSites.CountDocumentsAsync(x => 
            x.status == "rejected" && 
            x.deleted_at == null &&
            (x.Version == 5 || x.Version.ToString().Contains("5")));
            var totalRecords = await _context._ObligationSites.CountDocumentsAsync(_ => true &&
            _.deleted_at == null &&
            (_.Version == 5 || _.Version.ToString().Contains("5")));


            var resultCount = new ResultCount
            {
                ACTIVE = activeCount,
                INACTIVE = inactiveCount,
                PENDING = pendingCount,
                REJECTED = rejectedCount,
                ALL = totalRecords
            };


            return resultCount;
        }
        public async Task<ObligationSitesDto> GetFacilitySiteByIdAsync(string id)
        {
            var data = await _context._ObligationSites.Find(site => site.ID.ToString() == id).FirstOrDefaultAsync();
            var result = MapToDto(data);
            return result;
        }

        public async Task<List<ParentSiteDto>> GetParentSiteAsync(
    string regionid,
    string clusterid,
    string? search,
    int skip,
    int take)
        {
            var filters = new List<FilterDefinition<ObligationSites>>
    {
        Builders<ObligationSites>.Filter.Eq(site => site.region_id, regionid),
        Builders<ObligationSites>.Filter.Eq(site => site.deleted_at, null),
        Builders<ObligationSites>.Filter.Eq(site => site.Version, 5)
    };

            if (!string.IsNullOrEmpty(search))
            {
                var searchFilter = Builders<ObligationSites>.Filter.Or(
                    Builders<ObligationSites>.Filter.Regex(site => site.name, new BsonRegularExpression(search, "i")),
                    Builders<ObligationSites>.Filter.Regex(site => site.name_ar, new BsonRegularExpression(search, "i"))
                );

                filters.Add(searchFilter);
            }

            var finalFilter = Builders<ObligationSites>.Filter.And(filters);

            var projection = Builders<ObligationSites>.Projection
                .Include(s => s.ID)
                .Include(s => s.name)
                .Include(s => s.name_ar);

            var data = await _context._ObligationSites
                .Find(finalFilter)
                .Project<ObligationSites>(projection)
                .Skip(skip)
                .Limit(take)
                .ToListAsync();

            return data.Adapt<List<ParentSiteDto>>();
        }


        public async Task<ApiResponse<ObligationSitesDto>> GetAllFacilitySitesAsync(
    int page, int pageSize, string? search, string? region_id,
    string? facility, int? version, string? status, string submission_for)
        {
            try
            {
                pageSize = Math.Max(pageSize, 1);
                page = Math.Max(page, 1);
                var skip = (page - 1) * pageSize;

                var user = await _userRepository.GetUserFromHttpContextAsync();

                // 🔹 Fetch all regions & clusters (in parallel for efficiency)
                var regionsTask = _context._regions.Find(_ => true).ToListAsync();
                var sectorTask = _context.Sector.Find(_ => true).ToListAsync();
                var sub_sectorTask = _context.Sub_sector.Find(_ => true).ToListAsync();
                var sub_regionTask = _context.Sub_region.Find(_ => true).ToListAsync();
                await Task.WhenAll(regionsTask, sectorTask, sub_sectorTask, sub_regionTask);

                var allRegions = regionsTask.Result;
                var allSectors = sectorTask.Result;
                var allsub_sectors = sub_sectorTask.Result;
                var allsub_regions = sub_regionTask.Result;
                var filter = Builders<ObligationSites>.Filter.Empty;
                var combinedFilter = Builders<ObligationSites>.Filter.Empty;
                if (!string.IsNullOrEmpty(submission_for) && submission_for.Trim().ToLower() == "hajj inspection")
                {
                    var allowedRegionNames = new[] { "Jeddah", "Makkah", "Madina", "Taif" };
                    var allowedRegionIds = allRegions
                        .Where(r => allowedRegionNames.Contains(r.name, StringComparer.OrdinalIgnoreCase))
                        .Select(r => r.ID.ToString())
                        .ToList();

                    filter &= Builders<ObligationSites>.Filter.In(x => x.region_id, allowedRegionIds);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    combinedFilter &= Builders<ObligationSites>.Filter.Eq(x => x.status, status.ToLower());
                }
                

                // 🔹 Search Filters (Regions & Clusters)
                if (!string.IsNullOrEmpty(search))
                {
                    var filteredRegionIds = allRegions
                        .Where(r => !r.deleted_at.HasValue && r.name.Contains(search, StringComparison.OrdinalIgnoreCase))
                        .Select(r => r.ID.ToString())
                        .ToList();

                    var filteredSectorIds = allSectors
                        .Where(c => !c.deleted_at.HasValue && c.name.Contains(search, StringComparison.OrdinalIgnoreCase))
                        .Select(c => c._id)
                        .ToList();

                    var filteredSub_SectorIds = allsub_sectors
                        .Where(c => !c.deleted_at.HasValue && c.name.Contains(search, StringComparison.OrdinalIgnoreCase))
                        .Select(c => c._id)
                        .ToList();

                    var filteredSub_RegionIds = allsub_regions
                        .Where(c => !c.deleted_at.HasValue && c.name.Contains(search, StringComparison.OrdinalIgnoreCase))
                        .Select(c => c._id)
                        .ToList();

                    filter &= Builders<ObligationSites>.Filter.Or(
                        Builders<ObligationSites>.Filter.Regex(x => x.name, new BsonRegularExpression(search, "i")),
                        Builders<ObligationSites>.Filter.Regex(x => x.name_ar, new BsonRegularExpression(search, "i")),
                        Builders<ObligationSites>.Filter.Regex(x => x.facilityType, new BsonRegularExpression(search, "i")),
                        Builders<ObligationSites>.Filter.In(x => x.region_id, filteredRegionIds),
                        Builders<ObligationSites>.Filter.In(x => x.sub_region_id, filteredSub_RegionIds),
                        Builders<ObligationSites>.Filter.In(x => x.sub_sector_id, filteredSub_SectorIds),
                        Builders<ObligationSites>.Filter.In(x => x.sector_id, filteredSectorIds)
                    );
                }
                if (!string.IsNullOrEmpty(region_id))
                {
                    filter &= Builders<ObligationSites>.Filter.Eq(x => x.region_id, region_id.Trim());
                }


                if (user.type != "admin" && user.type != "sof admin")
                {
                    filter &= Builders<ObligationSites>.Filter.In(x => x.region_id, user.region_ids);
                }

                if (!string.IsNullOrEmpty(facility))
                {
                    filter &= Builders<ObligationSites>.Filter.Eq(x => x.facilityType, facility.Trim());
                }

                if (version.HasValue)
                {
                    filter &= Builders<ObligationSites>.Filter.Or(
                        Builders<ObligationSites>.Filter.Eq(x => x.Version, version),
                        Builders<ObligationSites>.Filter.Regex(x => x.Version, new BsonRegularExpression(version.Value.ToString()))
                    );
                }
                else
                {
                    filter &= Builders<ObligationSites>.Filter.Or(
                        Builders<ObligationSites>.Filter.Eq(x => x.Version, 5),
                        Builders<ObligationSites>.Filter.Regex(x => x.Version, new BsonRegularExpression("5"))
                    );
                }
                filter &= Builders<ObligationSites>.Filter.Eq(x => x.deleted_at, null);
                combinedFilter = Builders<ObligationSites>.Filter.And(filter, combinedFilter);
                // 🔹 Projection (Optimize Query Performance)
               
                var projection = Builders<ObligationSites>.Projection
                .Include(x => x.ID)
                .Include(x => x.name)
                .Include(x => x.name_ar)
                .Include(x => x.facilityType)
                    .Include(x => x.CreatedDate)
                    .Include(x => x.sector_id)
                    .Include(x => x.sub_sector_id)
                    .Include(x => x.sub_region_id)
                    .Include(x => x.region_id);

                // 🔹 Fetch paginated sites
                var sites = await _context._ObligationSites
                    .Find(combinedFilter)
                    .Project<ObligationSites>(projection)
                    .SortByDescending(x => x.CreatedDate)
                    .Skip(skip)
                    .Limit(pageSize)
                    .ToListAsync();

                // 🔹 Map region & cluster info
                var regionDict = allRegions.ToDictionary(r => r.ID.ToString());
                var sectorDict = allSectors.ToDictionary(c => c._id);
                var sub_regionsDict = allsub_regions.ToDictionary(c => c._id);
                var sub_sectorsDict = allsub_sectors.ToDictionary(c => c._id);

                foreach (var site in sites)
                {
                    site.region = regionDict.GetValueOrDefault(site.region_id);
                    site.Sector = sectorDict.GetValueOrDefault(site.sector_id);
                    site.Sub_region = sub_regionsDict.GetValueOrDefault(site.sub_region_id);
                    site.Sub_sector = sub_sectorsDict.GetValueOrDefault(site.sub_sector_id);
                }

                // 🔹 Get total record count & paginate
                var totalRecords = await _context._ObligationSites.CountDocumentsAsync(combinedFilter);
                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

                // 🔹 Convert to DTOs
                var siteDtos = sites.Adapt<List<ObligationSitesDto>>();

                // 🔹 Count records by status (Parallel Execution)
                var statusCountsTask = Task.WhenAll(
                    _context._ObligationSites.CountDocumentsAsync(filter & Builders<ObligationSites>.Filter.Eq(x => x.status, "active")),
                    _context._ObligationSites.CountDocumentsAsync(filter & Builders<ObligationSites>.Filter.Eq(x => x.status, "in_active")),
                    _context._ObligationSites.CountDocumentsAsync(filter & Builders<ObligationSites>.Filter.Eq(x => x.status, "pending")),
                    _context._ObligationSites.CountDocumentsAsync(filter & Builders<ObligationSites>.Filter.Eq(x => x.status, "rejected"))
                );
                await statusCountsTask;

                var resultCount = new ResultCount
                {
                    ACTIVE = statusCountsTask.Result[0],
                    INACTIVE = statusCountsTask.Result[1],
                    PENDING = statusCountsTask.Result[2],
                    REJECTED = statusCountsTask.Result[3],
                    ALL = statusCountsTask.Result.Sum()
                };

                // 🔹 Return paginated response
                return new ApiResponse<ObligationSitesDto>
                {
                    current_page = page,
                    data = siteDtos,
                    total = (int)totalRecords,
                    per_page = pageSize,
                    last_page = totalPages,
                    next_page_url = page < totalPages ? $"{_baseUrl}?page={page + 1}" : null,
                    prev_page_url = page > 1 ? $"{_baseUrl}?page={page - 1}" : null,
                    resultCount_ = resultCount
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching facility sites", ex);
            }
        }





        public async Task CreateFacilitySiteAsync(ObligationSitesDto site)
        {
            var currentuser = await _userRepository.GetUserFromHttpContextAsync(); 
            site.status = _userRepository.IsTopThree(currentuser.type) ? "active" : "pending";
            if(site.status == "active")
            site.status = site.Active ? "active" : "in_active";
            var siteEntity = MapToEntity(site);
            await _context._ObligationSites.InsertOneAsync(siteEntity);
        }

        public async Task UpdateFacilitySiteAsync(string id, ObligationSitesDto site)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentException("Invalid ObjectId format.", nameof(id));
            }
            var currentuser = await _userRepository.GetUserFromHttpContextAsync(); 
            site.status = _userRepository.IsTopThree(currentuser.type) ? "active" : "pending";
            if (site.status == "active")
            site.status = site.Active ? "active" : "in_active";
            var siteEntity = MapToEntity(site);
            siteEntity.ID = objectId;
            await _context._ObligationSites.ReplaceOneAsync(s => s.ID == siteEntity.ID, siteEntity);
        }


        private ObligationSitesDto MapToDto(ObligationSites site)
        {
            if (site == null) return null;

            return new ObligationSitesDto
            {
                IdAsString = site.ID.ToString(),
                region_id = site.region_id,
                name_ar = site.name_ar,
                name = site.name,
                Active = site.Active,
                facilityType = site.facilityType,
                site_parent_id = site.site_parent_id,
                status = site.status,
                region = _context._regions
                    .Find(r => r.ID.ToString() == site.region_id)
                    .Project(r => new RegionDto_
                    {
                        IdAsString = site.region_id,
                        name = r.name
                    })
                    .FirstOrDefault(),

            };
        }

        private ObligationSites MapToEntity(ObligationSitesDto siteDto)
        {
            bool isParsedR = ObjectId.TryParse(siteDto.region_id, out var objectIdR);
            return new ObligationSites
            {
                name = siteDto.name,
                name_ar = siteDto.name_ar,
                status = siteDto.status,
                Active = siteDto.Active,
                facilityType=siteDto.facilityType,
                site_parent_id = siteDto.site_parent_id,
                Version = 5,
                region_id = siteDto.region_id
            };
        }

        public async Task<ObligationSites> GetByIdAsync(string id)
        {
            return await _context._ObligationSites.Find(site => site.ID.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, ObligationSites site)
        {
            await _context._ObligationSites.ReplaceOneAsync(s => s.ID.ToString() == id, site);
        }


        public async Task DeleteAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<ObligationSites>.Filter.Eq(c => c.ID, objectId);
            var update = Builders<ObligationSites>.Update
                .Set(c => c.deleted_at, DateTime.Now)
                .Set(c => c.UpdatedDate, DateTime.UtcNow);
            await _context._ObligationSites.UpdateOneAsync(filter, update);
        }

    }
}
