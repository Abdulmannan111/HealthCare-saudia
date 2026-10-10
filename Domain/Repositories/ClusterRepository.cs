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
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Entities.ApiSettings;
using Microsoft.Extensions.Options;

namespace Domain.Repositories
{
    public class ClusterRepository : IClusterRepository
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly string _baseUrl;

        public ClusterRepository(IOptions<ApiSettings> apiSettings, MongoDBContext context, IMapper mapper)
        {
           _context = context;
            _mapper = mapper;
            _baseUrl = apiSettings.Value.BaseUrl + "clusters/GetAllClusters";
        }

        public async Task<ApiResponse<ClusterDto>> GetAllAsync(int page, int pageSize, string? search, string submission_for)
        {
            try
            {
                // Validate page and pageSize to ensure they are within acceptable ranges
                page = page <= 0 ? 1 : page;
                pageSize = pageSize <= 0 ? 10 : pageSize;

                // Calculate the number of records to skip based on the page number
                var skip = (page - 1) * pageSize;

                // Initialize the filter for MongoDB queries
                var filter = Builders<Clusters>.Filter.Empty;
                if (!string.IsNullOrEmpty(submission_for) && submission_for.Trim().ToLower() == "hajj inspection")
                {
                    var allowedRegionNames = new[] { "Jeddah", "Makkah", "Madina", "Taif" };

                    // Fetch all region names from DB and then filter in memory
                    var allRegions = await _context._regions
                        .Find(r => r.deleted_at == null)
                        .ToListAsync();

                    var allowedRegionIds = allRegions
                        .Where(r => allowedRegionNames.Contains(r.name, StringComparer.OrdinalIgnoreCase))
                        .Select(r => r.ID.ToString())
                        .ToList();

                    filter &= Builders<Clusters>.Filter.In(c => c.region_id, allowedRegionIds);
                }

                // Apply search filter if a search term is provided
                if (!string.IsNullOrEmpty(search))
                {
                    var searchTerm = search.ToLower();
                    var matchingRegions = await _context._regions
                        .Find(f => Regex.IsMatch(f.name.ToLower(), searchTerm, RegexOptions.IgnoreCase))
                        .Project(f => f.ID.ToString())
                        .ToListAsync();
                    // Search in both cluster name and region name fields
                    filter = Builders<Clusters>.Filter.Or(
                        Builders<Clusters>.Filter.Where(x => x.name.ToLower().Contains(searchTerm)),
                        Builders<Clusters> .Filter.In(x => x.region_id, matchingRegions),
                        Builders<Clusters>.Filter.Where(x => x.region.name.ToLower().Contains(searchTerm)) // Assuming 'region_name' is present in the Clusters collection
                    );
                }
                filter &= Builders<Clusters>.Filter.Or(
    Builders<Clusters>.Filter.Exists(x => x.deleted_at, false),
    Builders<Clusters>.Filter.Eq(x => x.deleted_at, null)
);


                // Retrieve the clusters based on the search filter and pagination parameters
                var clusters = await _context._clusters
                    .Find(filter)
                    .SortByDescending(x => x.CreatedDate) // Sorting by creation date in descending order
                    .Skip(skip)
                    .Limit(pageSize)
                    .ToListAsync();

                // Retrieve the corresponding regions for the clusters
                var regionIds = clusters.Select(cluster => cluster.region_id).ToList();
                var regions = await _context._regions
                    .Find(region => regionIds.Contains(region.ID.ToString())) // Match regions based on their IDs
                    .ToListAsync();

                // Map the region information to each cluster
                foreach (var cluster in clusters)
                {
                    cluster.region = regions.FirstOrDefault(region => region.IdAsString == cluster.region_id);
                }

                // Calculate the total number of clusters matching the filter for pagination
                var totalRecords = await _context._clusters.CountDocumentsAsync(filter);
                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

                // Map the clusters to DTOs
                var clusterDtos = _mapper.Map<List<ClusterDto>>(clusters);

                // Return the paginated and mapped response
                return new ApiResponse<ClusterDto>
                {
                    current_page = page,
                    data = clusterDtos,
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
            catch(Exception ex)
            {
                return null;
            }
            }

        public ClusterDto GetById(string id)
        {
            var clusterData = _context._clusters.Find(cluster => cluster.ID.ToString() == id).FirstOrDefault();
            var clusters = _mapper.Map<ClusterDto>(clusterData);
            var region = _context._regions.Find(x => x.ID.ToString() == clusters.region_id).FirstOrDefault();
            clusters.region = _mapper.Map<RegionDto>(region);
            return clusters;
        }


        public async Task CreateAsync(ClusterDto cluster)
        {
            try
            {

                var allClusters = await _context._clusters.Find(x => x.region_id == cluster.region_id && x.name == cluster.name  && x.deleted_at == null).ToListAsync();
                if (allClusters.Any())
                {
                    throw new Exception("The name has already been taken.");
                }
                var clusterEntity = _mapper.Map<Clusters>(cluster);
                await _context._clusters.InsertOneAsync(clusterEntity);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAsync(string id, ClusterDto cluster)
        {
            try
            {
                if (!ObjectId.TryParse(id, out var objectId))
                {
                    throw new ArgumentException("Invalid ObjectId format.", nameof(id));
                }
                var allClusters = await _context._clusters.Find(x => x.region_id == cluster.region_id && x.name == cluster.name  && x.deleted_at == null).ToListAsync();
                if (allClusters.Any())
                {
                    throw new Exception("The name has already been taken.");
                }
                var clusterEntity = _mapper.Map<Clusters>(cluster);
                clusterEntity.ID = objectId;
                await _context._clusters.ReplaceOneAsync(c => c.ID.ToString() == id, clusterEntity);
                //await _context._ObligationSites.UpdateManyAsync(
                //    Builders<ObligationSites>.Filter.Eq(x => x.cluster_id, id),
                //    Builders<ObligationSites>.Update.Set(x => x.sector_, clusterEntity.sector)
                //);
            }
            catch (Exception ex)
            {

            }
        }


        public async Task DeleteAsync(string id)
        {
            var objectId = ObjectId.Parse(id);  
            var filter = Builders<Clusters>.Filter.Eq(c => c.ID, objectId);
            var update = Builders<Clusters>.Update
                .Set(c => c.deleted_at, DateTime.Now)
                .Set(c => c.UpdatedDate, DateTime.UtcNow);

            await _context._clusters.UpdateOneAsync(filter, update);
        }


        async Task<List<ClusterDto>> IClusterRepository.GetClustersByRegionIdAsync(string regionId)
        {
            var clusterData = await _context._clusters.Find(cluster => cluster.ID.ToString() == regionId).ToListAsync();
            var clusters = _mapper.Map<List<ClusterDto>>(clusterData);
            return clusters.ToList();
        }
    }
}
