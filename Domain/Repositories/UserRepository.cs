using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using Application.Pagination;
using MongoDB.Bson;
using MongoDB.Driver;
using Infrastructure.Mongo;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;
using Domain.Services;
using Infrastructure.Configurations;
using Application.DTOs.User;
using Application.Entities.Facility;
using Application.Entities.ApiSettings;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using Hangfire;
using System.Linq;


namespace Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly string _baseUrl;
        private readonly EmailSettings _emailSettings;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public UserRepository(IOptions<ApiSettings> apiSettings, MongoDBContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IEmailService emailService, EmailSettings emailSettings, IBackgroundJobClient backgroundJobClient)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _emailSettings = emailSettings;
            _baseUrl = apiSettings.Value.BaseUrl + "Users/user-index";
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<ResultCount> GetCounts()
        {
            var activeCount = await _context.Users.CountDocumentsAsync(x => x.status == "active" && x.deleted_at == null);
            var inactiveCount = await _context.Users.CountDocumentsAsync(x => x.status == "in_active" && x.deleted_at == null);
            var pendingCount = await _context.Users.CountDocumentsAsync(x => x.status == "pending" && x.deleted_at == null);
            var rejectedCount = await _context.Users.CountDocumentsAsync(x => x.status == "rejected" && x.deleted_at == null);
            var totalRecords = await _context.Users.CountDocumentsAsync(_ => true);


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




        public async Task<ApiResponse<UserDto>> GetAllUsersAsync(int page, int pageSize, string? statusFilter, string? searchFilter)
        {
            try
            {
                pageSize = pageSize <= 0 ? 20 : pageSize;
                page = page <= 0 ? 1 : page;
                var skip = (page - 1) * pageSize;

                statusFilter = statusFilter?.ToLower() == "inactive" ? "in_active" : statusFilter?.ToLower();
                searchFilter = searchFilter?.ToLower();

                var filter = Builders<Users>.Filter.Empty;
                var combinedFilter = Builders<Users>.Filter.Empty;

                var user = await GetUserFromHttpContextAsync();
                if (user.type != "admin")
                {
                    filter &= GetUserFilter(user.type, user.region_ids);
                }

                if (!string.IsNullOrEmpty(statusFilter))
                {
                    combinedFilter &= Builders<Users>.Filter.Eq(x => x.status, statusFilter);
                }

                filter &= Builders<Users>.Filter.Eq(x => x.deleted_at, null);

                if (!string.IsNullOrEmpty(searchFilter))
                {
                    var matchingClusters = await _context._clusters
                        .Find(c => Regex.IsMatch(c.name, searchFilter, RegexOptions.IgnoreCase))
                        .Project(c => c.ID.ToString())
                        .ToListAsync();

                    var matchingFacilities = await _context._Facilities
                        .Find(f => Regex.IsMatch(f.name, searchFilter, RegexOptions.IgnoreCase) ||
                                   Regex.IsMatch(f.name_ar, searchFilter, RegexOptions.IgnoreCase))
                        .Project(f => f.ID.ToString())
                        .ToListAsync();

                    var matchingRegions = await _context._regions
                        .Find(f => Regex.IsMatch(f.name, searchFilter, RegexOptions.IgnoreCase))
                        .Project(f => f.ID.ToString())
                        .ToListAsync();

                    //var matchingSubRegions = await _context._subRegions
                    //    .Find(sr => Regex.IsMatch(sr.name, searchFilter, RegexOptions.IgnoreCase))
                    //    .Project(sr => sr.ID.ToString())
                    //    .ToListAsync();

                    //var matchingSubSectors = await _context._subSector
                    //    .Find(ss => Regex.IsMatch(ss.name, searchFilter, RegexOptions.IgnoreCase))
                    //    .Project(ss => ss.ID.ToString())
                    //    .ToListAsync();

                    filter &= Builders<Users>.Filter.Or(
                        Builders<Users>.Filter.Regex(x => x.name, new BsonRegularExpression(searchFilter, "i")),
                        Builders<Users>.Filter.Regex(x => x.email, new BsonRegularExpression(searchFilter, "i")),
                        Builders<Users>.Filter.Regex(x => x.region, new BsonRegularExpression(searchFilter, "i")),
                        Builders<Users>.Filter.Regex(x => x.modules, new BsonRegularExpression(searchFilter, "i")),
                        (searchFilter == "compliance admin"
                            ? Builders<Users>.Filter.Eq(x => x.type, "admin")
                            : Builders<Users>.Filter.Regex(x => x.type, new BsonRegularExpression(searchFilter, "i"))),
                        Builders<Users>.Filter.AnyIn(x => x.clusters_ids, matchingClusters),
                        Builders<Users>.Filter.AnyIn(x => x.region_ids, matchingRegions),
                        Builders<Users>.Filter.AnyIn(x => x.obligation_sites_ids_, matchingFacilities.Select(id => (BsonValue)id))
                        //Builders<Users>.Filter.AnyIn(x => x.sub_region_id, matchingSubRegions),
                       // Builders<Users>.Filter.AnyIn(x => x.sub_sector_id, matchingSubSectors)
                    );
                }

                combinedFilter = Builders<Users>.Filter.And(filter, combinedFilter);

                var projection = Builders<Users>.Projection
                    .Include(x => x.ID)
                    .Include(x => x.name)
                    .Include(x => x.email)
                    .Include(x => x.type)
                    .Include(x => x.status)
                    .Include(x => x.phone)
                    .Include(x => x.region_ids)
                    .Include(x => x.clusters_ids)
                    .Include(x => x.obligation_sites_ids_)
                    .Include(x => x.sub_region_id)
                    .Include(x => x.sub_sector_id);

                var users = await _context.Users
                    .Find(combinedFilter)
                    .SortByDescending(x => x.CreatedDate)
                    .Skip(skip)
                    .Limit(pageSize)
                    .Project<Users>(projection)
                    .ToListAsync();

                var regionIds = users.SelectMany(u => u.region_ids).Distinct().ToList();
                var clusterIds = users.SelectMany(u => u.clusters_ids).Distinct().ToList();
                var siteIds = users.SelectMany(u => u.obligation_sites_ids_).Distinct().ToList();
                var subRegionIds = user.sub_region_id.Split(',');     // This is correct
                var subSectorIds = user.sub_sector_id.Split(',');     // This is also correct

                var sitesTask = _context._Facilities
                    .Find(c => siteIds.Contains(c.ID.ToString()))
                    .Project(c => new { c.ID, c.name, c.name_ar })
                    .ToListAsync();

                var regionsTask = _context._regions
                    .Find(r => regionIds.Contains(r.ID.ToString()))
                    .Project(r => new { r.ID, r.name })
                    .ToListAsync();

                var clustersTask = _context._clusters
                    .Find(c => clusterIds.Contains(c.ID.ToString()))
                    .Project(c => new { c.ID, c.name })
                    .ToListAsync();
               
                var subRegionTask = _context._subRegions
                      .Find(r => subRegionIds.Contains(r.ID.ToString()))
                      .Project(r => new { r.ID, r.name })
                      .ToListAsync();

                var subSectorTask = _context._subSector
                     .Find(c => subSectorIds.Contains(c.ID.ToString()))
                     .Project(c => new { c.ID, c.name })
                     .ToListAsync();
                await Task.WhenAll(sitesTask, regionsTask, clustersTask);

                var sites = sitesTask.Result;
                var regions = regionsTask.Result;
                var clusters = clustersTask.Result;
                var subRegions = subRegionTask.Result;
                var subSectors = subSectorTask.Result;

                var userDtos = _mapper.Map<List<UserDto>>(users);

                foreach (var userDto in userDtos)
                {
                    userDto.region = userDto.region_ids
                        .Select(regionId => regions.FirstOrDefault(r => r.ID.ToString() == regionId))
                        .Where(r => r != null)
                        .Select(r => new RegionDto { IdAsString = r.ID.ToString(), name = r.name })
                        .ToList();

                    userDto.facilities = userDto.obligation_sites_ids
                        .Select(siteId => sites.FirstOrDefault(s => s.ID.ToString() == siteId))
                        .Where(s => s != null)
                        .Select(s => new FacilitiesDto { IdAsString = s.ID.ToString(), name = s.name, name_ar = s.name_ar })
                        .ToList();

                    userDto.clusters = userDto.clusters_ids
                        .Select(clusterId => clusters.FirstOrDefault(c => c.ID.ToString() == clusterId))
                        .Where(c => c != null)
                        .Select(c => new ClusterDto { IdAsString = c.ID.ToString(), name = c.name })
                        .ToList();

                    //Mapping subRegions
                   
                    // Then map them
                    userDto.subRegions = subRegionIds
                        .Select(subRegionId => subRegions.FirstOrDefault(sr => sr.ID.ToString() == subRegionId))
                        .Where(sr => sr != null)
                        .Select(sr => new SubRegionsDto { IdAsString = sr.ID.ToString(), name = sr.name })
                        .ToList();

                    userDto.subSector = subSectorIds
                        .Select(subSectorId => subSectors.FirstOrDefault(ss => ss.ID.ToString() == subSectorId))
                        .Where(ss => ss != null)
                        .Select(ss => new SubSectorDto { IdAsString = ss.ID.ToString(), name = ss.name })
                        .ToList();

                }

                var totalRecords = await _context.Users.CountDocumentsAsync(combinedFilter);
                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                page = Math.Min(page, totalPages);

                var activeCount = await _context.Users.CountDocumentsAsync(filter & Builders<Users>.Filter.Eq(x => x.status, "active"));
                var inactiveCount = await _context.Users.CountDocumentsAsync(filter & Builders<Users>.Filter.Eq(x => x.status, "in_active"));
                var pendingCount = await _context.Users.CountDocumentsAsync(filter & Builders<Users>.Filter.Eq(x => x.status, "pending"));
                var rejectedCount = await _context.Users.CountDocumentsAsync(filter & Builders<Users>.Filter.Eq(x => x.status, "rejected"));

                var resultCount = new ResultCount
                {
                    ACTIVE = activeCount,
                    INACTIVE = inactiveCount,
                    PENDING = pendingCount,
                    REJECTED = rejectedCount,
                    ALL = activeCount + inactiveCount + pendingCount + rejectedCount
                };

                return new ApiResponse<UserDto>
                {
                    current_page = page,
                    data = userDtos,
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
                    total = (int)totalRecords,
                    resultCount_ = resultCount
                };
            }
            catch (Exception ex)
            {
                // Consider logging the exception
                return null;
            }
        }



        public async Task<ApiResponse<UserDto>> ExportAsync(int page, int pageSize, string? s, string? search)
        {
            try {
                // Default page size is 20 if not provided
                if (pageSize <= 0) pageSize = 20;
                if (page <= 0) page = 1;

                var skip = (page - 1) * pageSize;

                // Handle status filtering
                var statusFilter = string.IsNullOrEmpty(s) ? "" : s.ToLower();

                if (statusFilter == "inactive")
                {
                    statusFilter = "in_active";  // Transform "inactive" into "in_"
                }

                // Prepare search filters
                var searchFilter = string.IsNullOrEmpty(search) ? "" : search.ToLower();

                // Build the filter conditions
                var filter = Builders<Users>.Filter.Empty;

                // Apply status filter if provided
                if (!string.IsNullOrEmpty(statusFilter))
                {
                    filter &= Builders<Users>.Filter.Eq(x => x.status, statusFilter) & Builders<Users>.Filter.Eq(x => x.deleted_at, null);
                }

                // Apply search filter if provided
                if (!string.IsNullOrEmpty(searchFilter))
                {
                    filter &= Builders<Users>.Filter.Or(
                        Builders<Users>.Filter.Regex(x => x.name, new BsonRegularExpression(searchFilter, "i")),
                        Builders<Users>.Filter.Regex(x => x.email, new BsonRegularExpression(searchFilter, "i")),
                        Builders<Users>.Filter.Regex(x => x.region, new BsonRegularExpression(searchFilter, "i")), // Adjust if needed based on your structure
                        Builders<Users>.Filter.Regex(x => x.modules, new BsonRegularExpression(searchFilter, "i")) // Adjust module field as needed
                     );
                }
                var projection = Builders<Users>.Projection
               .Include(x => x.ID)    // Include essential fields only
               .Include(x => x.name)
               .Include(x => x.type)
               .Include(x => x.email)
               .Include(x => x.status)
              // .Include(x => x.lastLogin)
               .Include(x => x.obligation_sites_ids_)

               .Include(x => x.region_ids)
               .Include(x => x.clusters_ids);

                // Fetch users with pagination and sorting
                var users = new List<Users>();
                try
                {
                    users = await _context.Users
                    .Find(filter)
                    .Project<Users>(projection)
                    .SortByDescending(x => x.CreatedDate)
                    .Skip(skip)
                    .Limit(pageSize)
                    .ToListAsync();

                }
                catch (Exception ex) { }

                // Fetch regions and modules based on the region_ids and module_ids
                var regionIds = users.SelectMany(u => u.region_ids).Distinct().ToList();
                var moduleIds = users.SelectMany(u => u.module_ids).Distinct().ToList();
                var clusterIds = users.SelectMany(s => s.clusters_ids).Distinct().ToList();
                var sitesids = users.SelectMany(s => s.obligation_sites_ids).Distinct().ToList();


                var sitesTask = _context._Facilities
                    .Find(c => sitesids.Contains(c.ID.ToString()))
                    .Project(c => new { c.ID, c.name, c.name_ar })
                    .ToListAsync();

                var regionsTask = _context._regions
                    .Find(r => regionIds.Contains(r.ID.ToString()))
                    .Project(c => new { c.ID, c.name })
                    .ToListAsync();

                var modulesTask = _context._modules
                    .Find(m => moduleIds.Contains(m.ID.ToString()))
                    .Project(c => new { c.ID, c.name })
                    .ToListAsync();

                var clustersTask = _context._clusters
                    .Find(c => clusterIds.Contains(c.ID.ToString()))
                    .Project(c => new { c.ID, c.name })
                    .ToListAsync();

                await Task.WhenAll(sitesTask, regionsTask, modulesTask, clustersTask);

                var sites = sitesTask.Result;
                var regions = regionsTask.Result;
                var modules = modulesTask.Result;
                var clusters = clustersTask.Result;


                // Map users with AutoMapper, including their regions and modules
                var userDtos = _mapper.Map<List<UserDto>>(users);

                foreach (var userDto in userDtos)
                {
                    // Map regions based on region_ids
                    userDto.region = userDto.region_ids
                        .Select(regionId => regions.FirstOrDefault(r => r.ID.ToString() == regionId))
                        .Where(r => r != null)
                        .Select(r => new RegionDto { IdAsString = r.ID.ToString(), name = r.name })
                        .ToList();

                    // Map modules based on module_ids
                    userDto.modules = userDto.module_ids
                        .Select(moduleId => modules.FirstOrDefault(m => m.ID.ToString() == moduleId))
                        .Where(m => m != null)
                        .Select(m => new ModuleDto { _id = m.ID.ToString(), Name = m.name })
                        .ToList();

                    userDto.facilities = userDto.obligation_sites_ids
                       .Select(siteId => sites.FirstOrDefault(s => s.ID.ToString() == siteId))
                       .Where(s => s != null)
                       .Select(s => new FacilitiesDto { IdAsString = s.ID.ToString(), name = s.name, name_ar = s.name_ar })
                       .ToList();

                    userDto.clusters = userDto.clusters_ids
                        .Select(clusterId => clusters.FirstOrDefault(c => c.ID.ToString() == clusterId))
                        .Where(c => c != null)
                        .Select(c => new ClusterDto { IdAsString = c.ID.ToString(), name = c.name })
                        .ToList();
                }

                // Count active, inactive, and other statuses
                var activeCount = await _context.Users.CountDocumentsAsync(x => x.status == "active" && x.deleted_at == null);
                var totalRecords = await _context.Users.CountDocumentsAsync(x => x.deleted_at == null);
                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

                // Ensure the page number does not exceed total pages
                if (page > totalPages) page = totalPages;

                return new ApiResponse<UserDto>
                {
                    current_page = page,
                    data = userDtos,
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
                    total = (int)activeCount,  // Adjust this count logic based on your requirements

                };
            }
            catch(Exception ex)
            {
                return null;
            }
            }
        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            try
            {
                // ✅ Validate ObjectId
                if (!ObjectId.TryParse(id, out var objectId))
                    throw new ArgumentException("Invalid ObjectId format.", nameof(id));

                // ✅ Fetch user from DB
                var user = await _context.Users.Find(u => u.ID == objectId).FirstOrDefaultAsync();
                if (user == null) return null;  // Return null or handle exception
                if (user.region_ids.Count>0)
                    if (user.region_ids[0] == user.default_region_id)
                        // ✅ Ensure region list initialization
                        user.region_ids = new List<string> { user.default_region_id };

                // ✅ Map user to DTO
                var userDto = _mapper.Map<UserDto>(user);

                // ✅ SME Regions: Add future-start-date regions
                if (userDto.SME_regions?.Any() == true)
                {
                    foreach (var smeRegion in userDto.SME_regions)
                    {
                        if (DateTime.TryParse(smeRegion.start_date, out DateTime startDate) && startDate > DateTime.UtcNow)
                        {
                            userDto.region_ids.Add(smeRegion.region._id);
                        }
                    }
                }

                // ✅ Load related entities in parallel
                var regionTask = _context._regions.Find(r => userDto.region_ids.Contains(r.ID.ToString())).ToListAsync();
                var clusterTask = _context._clusters.Find(c => userDto.clusters_ids.Contains(c.ID.ToString())).ToListAsync();
                var facilityTask = _context._Facilities.Find(f => userDto.obligation_sites_ids.Contains(f.ID.ToString())).ToListAsync();
                var moduleTask = _context._modules.Find(m => userDto.module_ids.Contains(m.ID.ToString())).ToListAsync();
                var subRegion = _context._subRegions.Find(r => userDto.sub_region_id.Contains(r.ID.ToString())).ToListAsync();
                var subSector = _context._subSector.Find(c => userDto.sub_sector_id.Contains(c.ID.ToString())).ToListAsync();

                await Task.WhenAll(regionTask, clusterTask, facilityTask, moduleTask, subRegion, subSector);

                // ✅ Map related entities
                userDto.region = regionTask.Result.Select(_mapper.Map<RegionDto>).ToList();
                userDto.clusters = clusterTask.Result.Select(_mapper.Map<ClusterDto>).ToList();
                userDto.facilities = facilityTask.Result.Select(_mapper.Map<FacilitiesDto>).ToList();
                userDto.subRegions = subRegion.Result.Select(_mapper.Map<SubRegionsDto>).ToList();
                userDto.subSector = subSector.Result.Select(_mapper.Map<SubSectorDto>).ToList();

                // ✅ Remove `user_ids` from modules
                var modules = moduleTask.Result;
                foreach (var module in modules) module.user_ids.Clear();

                userDto.modules = modules.Select(_mapper.Map<ModuleDto>).ToList();


                return userDto;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<UserDto> GetUserProfileAsync(string id)
        {
            try
            {
                // ✅ Validate ObjectId
                if (!ObjectId.TryParse(id, out var objectId))
                    throw new ArgumentException("Invalid ObjectId format.", nameof(id));

                // ✅ Fetch user from DB
                var user = await _context.Users.Find(u => u.ID == objectId).Project(u => new UserDto
                {
                    IdAsString = u.ID.ToString(),
                    name = u.name,
                    email = u.email,
                    phone = u.phone,
                    gender = u.gender,
                    type = u.type,
                    ProfilePic = u.ProfilePic

                }
                ).FirstOrDefaultAsync();
                if (user == null) return null;  // Return null or handle exception
                
                // ✅ Map user to DTO
                var userDto = _mapper.Map<UserDto>(user);

                

                // ✅ Remove password before returning
                userDto.password = null;

                return userDto;
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public async Task<Users> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Find(user => user.email == username).FirstOrDefaultAsync();
        }

        public async Task<UserDto[]> GetUserByEmailAsync(string email)
        {
            var data = await _context.Users.Find(user => user.email == email).ToListAsync();
            var users = data.Select(user => _mapper.Map<UserDto>(data)).ToArray();
            return users;
        }

        public async Task<UserDto> AddUserAsync(UserDto user)
        {
            try
            {
                user.email = user.email.Trim().ToLower();

                if (string.IsNullOrEmpty(user.name) || string.IsNullOrEmpty(user.email) ||
                    string.IsNullOrEmpty(user.password) || string.IsNullOrEmpty(user.phone))
                {
                    throw new ArgumentException("Name, email, password, and phone are required fields.");
                }

                var existingUserTask = _context.Users
                    .Find(u => u.email == user.email && u.deleted_at == null)
                    .FirstOrDefaultAsync();

                var loggedInUserTask = GetUserFromHttpContextAsync();

                await Task.WhenAll(existingUserTask, loggedInUserTask);

                if (existingUserTask.Result != null)
                {
                    throw new InvalidOperationException("Email has already been taken.");
                }

                AddFutureStartDateRegions(user);

                var loggedInUser = loggedInUserTask.Result;
                if (loggedInUser != null && !new[] { "admin", "sof admin", "regional representatives" }.Contains(loggedInUser.type))
                {
                    user.status = "pending";
                    user.active = false;
                }
                else
                {
                    user.status = "active";
                    user.active = true;
                }
                var generatedPassword = user.password;

                var userEntity = _mapper.Map<Users>(user);
                userEntity.password = BCrypt.Net.BCrypt.HashPassword(user.password);

                // Run region, cluster, and facility queries in parallel
                var regionsTask = _context._regions.Find(r => user.region_id.Contains(r.ID.ToString())).ToListAsync();
                var clustersTask = _context._clusters.Find(c => user.cluster_id.Contains(c.ID.ToString())).ToListAsync();
                var facilitiesTask = _context._Facilities.Find(f => user.facility_id.Contains(f.ID.ToString())).ToListAsync();
                var subRegionTask = _context._subRegions.Find(f => user.sub_region_id.Contains(f.ID.ToString())).ToListAsync();
                var subsectorTask = _context._subSector.Find(f => user.sub_sector_id.Contains(f.ID.ToString())).ToListAsync();

                await Task.WhenAll(regionsTask, clustersTask, facilitiesTask, subRegionTask, subsectorTask);

                userEntity.region_ids = regionsTask.Result.Select(r => r.ID.ToString()).ToList();
                userEntity.sub_region_id = string.Join(",", subRegionTask.Result.Select(r => r.ID.ToString()));
                userEntity.sub_sector_id = string.Join(",", subsectorTask.Result.Select(r => r.ID.ToString()));
                userEntity.clusters_ids = clustersTask.Result.Select(c => c.ID.ToString()).ToList();
                userEntity.obligation_sites_ids = facilitiesTask.Result.Select(f => f.ID.ToString()).ToList();

                // Handle module assignments
                userEntity.module_ids = await GetModulesForUserTypeAsync(userEntity.type);

                await _context.Users.InsertOneAsync(userEntity);
                // Assuming you already have injected EmailSettings into your class
                string templatePath = _emailSettings.Paths[EmailTemplatePath.EmailCreationPath];

                // Use the templatePath in the GenerateUserCreationEmail method
                // Run both methods in the background
                _ = Task.Run(async () =>
                {
                    // Generate the email body asynchronously
                    string emailBody = await _emailService.GenerateUserCreationEmail(userEntity.name, userEntity.email, generatedPassword, templatePath);

                    List<string> emailRecipients = new List<string> { userEntity.email };
                    // Send the email asynchronously
                    await _emailService.SendEmailAsync(emailRecipients, "Account Created", emailBody);
                });
                return _mapper.Map<UserDto>(userEntity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // New method to update the region, cluster, and facility with the user_id
        public async Task UpdateRegionClusterFacilityWithUserId(string userId, List<string> regionIds, List<string> clusterIds, List<string> facilityIds, List<string> moduleIds, string subRegionIds, string subSectorIds)
        {
            try
            {
                var regionOperations = new List<WriteModel<Regions>>();
                var clusterOperations = new List<WriteModel<Clusters>>();
                var facilityOperations = new List<WriteModel<Facilities>>();
                var moduleOperations = new List<WriteModel<Modules>>();

                var regionObjectIds = regionIds.Select(id => new ObjectId(id)).ToList();
                var clusterObjectIds = clusterIds.Select(id => new ObjectId(id)).ToList();
                var facilityObjectIds = facilityIds.Select(id => new ObjectId(id)).ToList();
                var moduleObjectIds = moduleIds.Select(id => new ObjectId(id)).ToList();
                // Regions Bulk Update
                if (regionIds.Any())
                {
                    regionOperations.Add(new UpdateManyModel<Regions>(
                        Builders<Regions>.Filter.Empty,
                        Builders<Regions>.Update.Pull("user_ids", userId)
                    ));

                    regionOperations.Add(new UpdateManyModel<Regions>(
                        Builders<Regions>.Filter.In(r => r.ID, regionObjectIds),
                        Builders<Regions>.Update.AddToSet("user_ids", userId)
                    ));
                }

                // Clusters Bulk Update
                if (clusterIds.Any())
                {
                    clusterOperations.Add(new UpdateManyModel<Clusters>(
                        Builders<Clusters>.Filter.Empty,
                        Builders<Clusters>.Update.Pull("user_ids", userId)
                    ));

                    clusterOperations.Add(new UpdateManyModel<Clusters>(
                        Builders<Clusters>.Filter.In(c => c.ID, clusterObjectIds),
                        Builders<Clusters>.Update.AddToSet("user_ids", userId)
                    ));
                }

                // Facilities Bulk Update
                if (facilityIds.Any())
                {
                    facilityOperations.Add(new UpdateManyModel<Facilities>(
                        Builders<Facilities>.Filter.Empty,
                        Builders<Facilities>.Update.Pull("user_ids", userId)
                    ));

                    facilityOperations.Add(new UpdateManyModel<Facilities>(
                        Builders<Facilities>.Filter.In(f => f.ID, facilityObjectIds),
                        Builders<Facilities>.Update.AddToSet("user_ids", userId)
                    ));
                }

                // Modules Bulk Update
                if (moduleIds.Any())
                {
                    moduleOperations.Add(new UpdateManyModel<Modules>(
                        Builders<Modules>.Filter.Empty,
                        Builders<Modules>.Update.Pull("user_ids", userId)
                    ));

                    moduleOperations.Add(new UpdateManyModel<Modules>(
                        Builders<Modules>.Filter.In(m => m.ID, moduleObjectIds),
                        Builders<Modules>.Update.AddToSet("user_ids", userId)
                    ));
                }

                // Execute Bulk Writes in Parallel for Performance
                var bulkTasks = new List<Task>();

                if (regionOperations.Any())
                    bulkTasks.Add(_context._regions.BulkWriteAsync(regionOperations));

                if (clusterOperations.Any())
                    bulkTasks.Add(_context._clusters.BulkWriteAsync(clusterOperations));

                if (facilityOperations.Any())
                    bulkTasks.Add(_context._Facilities.BulkWriteAsync(facilityOperations));

                if (moduleOperations.Any())
                    bulkTasks.Add(_context._modules.BulkWriteAsync(moduleOperations));

                await Task.WhenAll(bulkTasks); // Run all bulk operations concurrently
            }
            catch(Exception ex)
            {
                throw;
            }
        }



        public async Task<UserDto> UpdateUserAsync(string id, UserDto user)
        {
            try
            {
                // ✅ Ensure valid ObjectId
                if (!ObjectId.TryParse(id, out var objectId))
                    throw new ArgumentException("Invalid ObjectId format.", nameof(id));
                
                // ✅ Fetch existing user
                var existingUser = await _context.Users.Find(u => u.ID == objectId).FirstOrDefaultAsync();
                if (existingUser == null)
                    throw new InvalidOperationException("User not found.");
               
                AddFutureStartDateRegions(user);

                // ✅ Preserve existing status
                user.status = existingUser.status;
                // ✅ Hash password if updated
                if (string.IsNullOrEmpty(user.password))
                {
                    user.password = existingUser.password;
                }
                else
                {
                    user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
                }
                // ✅ Map updates
                _mapper.Map(user, existingUser);

               
                // ✅ Region synchronization
                existingUser.region_ids = user.region_id.Any()
                    ? (await _context._regions.Find(r => user.region_id.Contains(r.ID.ToString())).ToListAsync())
                        .Select(r => r.ID.ToString()).ToList()
                    : new List<string>();

                // ✅ Cluster synchronization
                existingUser.clusters_ids = user.cluster_id?.Any() == true
                    ? (await _context._clusters.Find(c => user.cluster_id.Contains(c.ID.ToString())).ToListAsync())
                        .Select(c => c.ID.ToString()).ToList()
                    : new List<string>();

                // ✅ Facility synchronization
                existingUser.obligation_sites_ids = user.facility_id?.Any() == true
                    ? (await _context._Facilities.Find(f => user.facility_id.Contains(f.ID.ToString())).ToListAsync())
                        .Select(f => f.ID.ToString()).ToList()
                    : new List<string>();

                // ✅ Module synchronization based on user type
                existingUser.module_ids = await GetModulesForUserTypeAsync(existingUser.type);

                // ✅ Set active flag based on status
                existingUser.active = existingUser.status == "active";

                // ✅ Update user in MongoDB
                var updateResult = await _context.Users.ReplaceOneAsync(u => u.ID == objectId, existingUser);
                if (updateResult.ModifiedCount == 0)
                    throw new InvalidOperationException("No changes were made to the user.");

                return _mapper.Map<UserDto>(existingUser);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}");
            }
        }

        public async Task<UserDto> UpdateUserByFieldName(string id, UserDto user)
        {
            try
            {
                if (!ObjectId.TryParse(id, out var objectId))
                    throw new ArgumentException("Invalid ObjectId format.", nameof(id));

                var existingUser = await GetByIdAsync(id);
                if (existingUser == null)
                    throw new InvalidOperationException("User not found.");

                bool isModified = false;

                // ✅ Only update changed fields
                if ((!string.Equals(NormalizePhone(existingUser.name), user.name, StringComparison.Ordinal)) && !string.IsNullOrEmpty(user.name))
                {
                    existingUser.name = user.name;
                    isModified = true;
                }

                if ((!string.Equals(existingUser.email, user.email, StringComparison.OrdinalIgnoreCase))&& !string.IsNullOrEmpty(user.email))
                {
                    existingUser.email = user.email;
                    isModified = true;
                }

                if ((!string.Equals(existingUser.phone, user.phone, StringComparison.Ordinal))&&!string.IsNullOrEmpty(user.phone))
                {
                    existingUser.phone = user.phone;
                    isModified = true;
                }

                if ((!string.Equals(existingUser.gender, user.gender, StringComparison.OrdinalIgnoreCase)) && !string.IsNullOrEmpty(user.gender))
                {
                    existingUser.gender = user.gender;
                    isModified = true;
                }

                if ((!string.Equals(existingUser.type, user.type, StringComparison.Ordinal)) && !string.IsNullOrEmpty(user.type))
                {
                    existingUser.type = user.type;
                    isModified = true;
                }

                if ((!string.Equals(existingUser.ProfilePic, user.ProfilePic, StringComparison.Ordinal)) && !string.IsNullOrEmpty(user.ProfilePic))
                {
                    existingUser.ProfilePic = user.ProfilePic;
                    isModified = true;
                }
               var users= _mapper.Map<Users>(existingUser);
                if (isModified)
                {
                    var updateResult = await _context.Users.ReplaceOneAsync(u => u.ID == objectId, users);
                    if (updateResult.ModifiedCount == 0)
                        throw new InvalidOperationException("Update operation did not modify the user.");
                }

                return _mapper.Map<UserDto>(users);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}");
            }
        }

        public async Task<UserDto> UpdateUserStatusAsync(string id, UserDto user)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentException("Invalid ObjectId format.", nameof(id));
            }
            var userEntity = _mapper.Map<Users>(user);
            userEntity.ID = objectId;
            await _context.Users.ReplaceOneAsync(r => r.ID.ToString() == id, userEntity);
            return user;
        }

        public async Task DeleteUserAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<Users>.Filter.Eq(c => c.ID, objectId);
            var update = Builders<Users>.Update
                .Set(c => c.deleted_at, DateTime.Now)
                .Set(c => c.UpdatedDate, DateTime.UtcNow);

            await _context.Users.UpdateOneAsync(filter, update);
        }


        public async Task ScheduleUserDeletionAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var user = await _context.Users.Find(u => u.ID == objectId).FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("User not found.");

            // Don't update anything in the DB now

            // Just send an email warning
            var emailList = new List<string> { user.email };
            _ = Task.Run(async () =>
            {
                await _emailService.SendEmailAsync(
                emailList,
                "Account Scheduled for Deletion",
                $"Dear {user.name}, your account will be deleted in 24 hours unless action is taken. Please contact support if this was a mistake."
            );
            });

                // Then schedule a delayed job to run after 24 hours
                // e.g., using Hangfire:
                _backgroundJobClient.Schedule(() => DeleteUserAsync(id), TimeSpan.FromHours(24));
        }


        public async Task<List<Users>> GetFilteredUsersAsync(UserFilterDto filterDto)
        {
            var filterBuilder = Builders<Users>.Filter;
            var filters = new List<FilterDefinition<Users>>();

            if (!string.IsNullOrEmpty(filterDto.Name))
                filters.Add(filterBuilder.Eq(u => u.email, filterDto.Name));

            if (!string.IsNullOrEmpty(filterDto.Email))
                filters.Add(filterBuilder.Eq(u => u.email, filterDto.Email));

            if (!string.IsNullOrEmpty(filterDto.Role))
                filters.Add(filterBuilder.Eq(u => u.type, filterDto.Role));

            //if (!string.IsNullOrEmpty(filterDto.Region))
            //    filters.Add(filterBuilder.Eq(u => u.Regions, filterDto.Region));

            if (filterDto.IsActive.HasValue)
                filters.Add(filterBuilder.Eq(u => u.active, filterDto.IsActive.Value));

            var filter = filterBuilder.And(filters);

            return await _context.Users.Find(filter).ToListAsync();
        }
        //Based on search and type
        public async Task<List<Users>> GetFilteredAllUsersAsync(AllUsersDto req)
        {
            var filterBuilder = Builders<Users>.Filter;
            var filter = filterBuilder.Empty; // Initialize with an empty filter to start building conditions

            if (!string.IsNullOrEmpty(req.search))
            {
                var searchFilter = filterBuilder.Or(
                    filterBuilder.Regex(u => u.name, new MongoDB.Bson.BsonRegularExpression(req.search, "i")),
                    filterBuilder.Regex(u => u.email, new MongoDB.Bson.BsonRegularExpression(req.search, "i"))
                );
                filter &= searchFilter;
            }

            if (!string.IsNullOrEmpty(req.formInfo.type))
            {
                switch (req.formInfo.type)
                {
                    case "expert":
                        filter &= filterBuilder.Eq(u => u.type, "expert");
                        break;
                    case "agent":
                        filter &= filterBuilder.Eq(u => u.type, "agent");
                        break;
                    case "guest_user":
                        break;
                    default:
                        filter &= filterBuilder.In(u => u.type, new[] { "agent", "expert" });
                        break;
                }
            }
            else
            {
                filter &= filterBuilder.In(u => u.type, new[] { "agent", "expert" });
            }

            var users = await _context.Users
                .Find(filter)       // Use the filter with Find
                .Limit(10)          // Equivalent to Take(10)
                .ToListAsync();     // Convert to List asynchronously

            return users;
        }


        public async Task AddRoleToUserAsync(string userId, string roleId)
        {
            var userRole = new UserRole { UserID = userId, RoleID = roleId };
            await _context._userRoles.InsertOneAsync(userRole);
        }

        public async Task AddRefreshTokenAt(string email)
        {
            var filter = Builders<Users>.Filter.Eq(u => u.email, email);
            var update = Builders<Users>.Update.Set(u => u.RefreshTokenAt, DateTime.UtcNow);

            await _context.Users.UpdateOneAsync(filter, update);
        }
        
        public async Task RemoveRoleFromUserAsync(string userId, string roleId)
        {
            await _context._userRoles.DeleteOneAsync(ur => ur.UserID == userId && ur.RoleID == roleId);
        }
        public async Task<Users> GetByIdAsync(string id)
        {
            return await _context.Users.Find(site => site.ID.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Users site)
        {
            await _context.Users.ReplaceOneAsync(s => s.ID.ToString() == id, site);
        }


        public async Task<UserDto> GetUserFromHttpContextAsync(string userIdKey = "UserId")
        {
            // Retrieve HttpContext
            var context = _httpContextAccessor.HttpContext;

            // Attempt to get the user ID from the HttpContext.Items
            if (context?.Items.TryGetValue(userIdKey, out var userId) == true)
            {
                // Convert userId to string (if found)
                string uid = userId?.ToString();

                if (!string.IsNullOrEmpty(uid))
                {
                    // Get user data from the repository
                    return await GetUserByIdAsync(uid);
                }
            }

            // Return null or throw exception if no user found or user ID is invalid
            return null;
        }
        
        public async Task<UserDto> GetUserProfileFromHttpContextAsync(string userIdKey = "UserId")
        {
            // Retrieve HttpContext
            var context = _httpContextAccessor.HttpContext;

            // Attempt to get the user ID from the HttpContext.Items
            if (context?.Items.TryGetValue(userIdKey, out var userId) == true)
            {
                // Convert userId to string (if found)
                string uid = userId?.ToString();

                if (!string.IsNullOrEmpty(uid))
                {
                    // Get user data from the repository
                    return await GetUserProfileAsync(uid);
                }
            }

            // Return null or throw exception if no user found or user ID is invalid
            return null;
        }

        public bool IsTopThree(string userType)
        {
            List<string> topThreeUserTypes = new() { "admin", "regional representatives", "supervisor" };

            return topThreeUserTypes.Contains(userType);
        }

        public async Task<List<Users>> GetUsersByEmailsAsync(List<string> emails)
        {
            return await _context.Users.Find(u => emails.Contains(u.email)).ToListAsync();
        }
        public Dictionary<string, (string UserEmail, string UserName)> GetUsersByIdsAsync(List<string> ids)
        {
            var users =  _context.Users
         .Find(u => ids.Contains(u.ID.ToString()) && u.deleted_at == null)
         .Project(u => new { u.ID, u.email, u.name })
         .ToList();

            return users.ToDictionary(
                u => u.ID.ToString(),
                u => (u.email, u.name));
        }


        public async Task<List<Users>> GetUsersByRolesAsync(List<string> roles)
        {
            return await _context.Users.Find(u => roles.Contains(u.type)).ToListAsync();
        }
        private async Task<List<string>> GetModulesForUserTypeAsync(string userType)
        {
            var moduleSlugs = userType switch
            {
                "sof admin" => new[] { "Users", "Sites","Clusters", "Regions" },
                "regional representatives" => new[] { "Users", "Sites", "Self-Assessment", "Clusters" },
                "supervisor" => new[] { "Users", "Sites", "Self-Assessment" },
                "agent" => new[] { "Self-Assessment" },
                _ => Array.Empty<string>()
            };

            return moduleSlugs.Any()
                ? await _context._modules.Find(m => moduleSlugs.Contains(m.slug)).Project(m => m.ID.ToString()).ToListAsync()
                : new List<string>();
        }
        private void AddFutureStartDateRegions(UserDto user)
        {
            user.email=user.email.ToLower().Trim();
            user.default_region_id = user.region_id?.FirstOrDefault();
            user.region_id = user.default_region_id != null ? new List<string> { user.default_region_id } : new List<string>();
            if (user.SME_regions?.Any() == true)
            {
                foreach (var smeRegion in user.SME_regions)
                {
                    if (DateTime.TryParse(smeRegion.start_date, out DateTime startDate) && startDate > DateTime.UtcNow)
                    {
                        if (!user.region_id.Contains(smeRegion.region._id))
                            user.region_id.Add(smeRegion.region._id);
                    }
                }
            }
        }
        private FilterDefinition<Users> GetUserFilter(string? userType, List<string> userRegions)
        {
            var roleFilters = new Dictionary<string, FilterDefinition<Users>>
            {
                ["sof admin"] = Builders<Users>.Filter.In(x => x.type,
                    new[] { "sof facility", "facility manager", "sof cluster", "sof hold co" }),

                ["regional representatives"] = Builders<Users>.Filter.In(x => x.type, new[] { "facility manager", "facility", "cluster manager", "hold_co" }) &
                                               Builders<Users>.Filter.AnyIn(x => x.region_ids, userRegions),

                ["default"] = Builders<Users>.Filter.Eq(x => x.type, "agent") &
                              Builders<Users>.Filter.AnyIn(x => x.region_ids, userRegions)
            };

            return roleFilters.GetValueOrDefault(userType, roleFilters["default"]);
        }
        string NormalizePhone(string str) =>
    str?.Replace(" ", "").Replace("-", "").Trim();
    }

}
