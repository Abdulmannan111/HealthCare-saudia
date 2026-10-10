using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using Application.Pagination;
using MongoDB.Bson;
using MongoDB.Driver;
using OfficeOpenXml;
using Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.RiskCompliance;
using Application.DTOs.Category;
using Application.Entities.Category;
using Application.Entities.Domain;
using Application.Entities.RiskCompliance;
using Application.Entities.ApiSettings;
using Microsoft.Extensions.Options;

namespace Domain.Repositories
{
    public class RiskComplianceItemRepository : IRiskComplianceItem
    {
        private readonly MongoDBContext _context;
        private readonly string _baseUrl;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        public RiskComplianceItemRepository(IOptions<ApiSettings> apiSettings, MongoDBContext context, IMapper mapper, IUserRepository userRepository)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
            _baseUrl = apiSettings.Value.BaseUrl + "RIskCompliance/RiskCompliance";
        }

        public async Task<ResultCount> GetCounts()
        {
            var notDeleted = await _context._riskCompliance.CountDocumentsAsync( x=> x.deleted_at == null);
            var deleted = await _context._riskCompliance.CountDocumentsAsync(x=> x.deleted_at != null);
           

            var resultCount = new ResultCount
            {
               deleted = deleted,
               notDeleted = notDeleted
            };


            return resultCount;
        }
        public async Task CreateAsync(RiskComplianceItemDto request)
        {

            var riskComplianceNumber = await GetRiskComplianceNumberAsync();
            string identifier;
            int riskCount;

            if (riskComplianceNumber != null)
            {
                riskCount = riskComplianceNumber.RiskCount + 1;
                identifier = $"{request.identifier}-{riskCount}";
                riskComplianceNumber.RiskCount = riskCount;
                await UpdateRiskComplianceNumberAsync(riskComplianceNumber);
            }
            else
            {
                var lastRiskCompliance = await GetLastRiskComplianceAsync();
                riskCount = lastRiskCompliance != null ? int.Parse(lastRiskCompliance.identifier.Split('-').Last()) + 1 : 1;
                identifier = $"{request.identifier}-{riskCount}";
                var newRiskComplianceNumber = new RiskComplianceNumber { RiskCount = riskCount };
                await _context.risk_compliance_numbers.InsertOneAsync(newRiskComplianceNumber);
            }

            // Calculate likelihoodPostfix, impactPrefix, score, and risk
            // var (likelihoodPostfix, impactPrefix, risk) = CalculateRiskData(request.likelihood, request.impact);

            // Map DTO to domain model
            var riskCompliance = _mapper.Map<RiskComplianceItem>(request);
            riskCompliance.identifier = identifier;

            await _context._riskCompliance.InsertOneAsync(riskCompliance);
            var riskComplianceId = riskCompliance.ID.ToString();
            // Log the activity for creation
            var user = _userRepository.GetUserFromHttpContextAsync();
            List<string> changedFields = new List<string> { "DomainId", "CategoryId", "SubCategoryId" };
            await LogActivityAsync(riskCompliance.ID.ToString(), "created", user.Result.name , changedFields, user.Result._id);

            // Map the domain model back to DTO for response
            _mapper.Map<RiskComplianceItem>(riskCompliance);
        }

        public async Task DeleteAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<RiskComplianceItem>.Filter.Eq(c => c.ID, objectId);
            var update = Builders<RiskComplianceItem>.Update
                .Set(c => c.deleted_at, DateTime.UtcNow)
                .Set(c => c.UpdatedDate, DateTime.UtcNow);
            await _context._riskCompliance.UpdateOneAsync(filter, update);
        }
        public async Task<ApiResponse<RiskComplianceItemDto>> GetAllAsync(int page, int pageSize, string? search, string? deleted_at)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var skip = (page - 1) * pageSize;

            // Build the base filter dynamically
            var filter = Builders<RiskComplianceItem>.Filter.Empty;
            if (deleted_at == "0") filter = Builders<RiskComplianceItem>.Filter.Eq(x => x.deleted_at, null);
            else if (deleted_at == "1") filter = Builders<RiskComplianceItem>.Filter.Ne(x => x.deleted_at, null);

            // Apply search if provided
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                var domainFilter = Builders<Domains>.Filter.Regex(d => d.name, new BsonRegularExpression(search, "i"));
                var categoryFilter = Builders<Category>.Filter.Regex(c => c.name, new BsonRegularExpression(search, "i"));
                var subCategoryFilter = Builders<SubCategories>.Filter.Regex(sc => sc.name, new BsonRegularExpression(search, "i"));

                var domains = await _context._domains.Find(domainFilter).ToListAsync();
                var categories = await _context._category.Find(categoryFilter).ToListAsync();
                var subCategories = await _context._subCategories.Find(subCategoryFilter).ToListAsync();

                var domainIds = domains.Select(d => d.ID.ToString()).ToList();
                var categoryIds = categories.Select(c => c.ID.ToString()).ToList();
                var subCategoryIds = subCategories.Select(sc => sc.ID.ToString()).ToList();

                var searchFilter = Builders<RiskComplianceItem>.Filter.Or(
                    Builders<RiskComplianceItem>.Filter.In(rc => rc.domain_id, domainIds),
                    Builders<RiskComplianceItem>.Filter.In(rc => rc.category_id, categoryIds),
                    Builders<RiskComplianceItem>.Filter.In(rc => rc.subCategory_id, subCategoryIds),
                    Builders<RiskComplianceItem>.Filter.Regex(rc => rc.standard, new BsonRegularExpression(search, "i"))
                );

                filter = Builders<RiskComplianceItem>.Filter.And(filter, searchFilter);
            }

            // Fetch risk compliance items with pagination
            var riskCompliances = await _context._riskCompliance
                .Find(filter)
                .SortByDescending(x => x.CreatedDate)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();

            // Fetch related data only if riskCompliances is not empty
            if (riskCompliances.Any())
            {
                var categoryIds = riskCompliances.Select(rc => rc.category_id).Distinct().ToList();
                var domainIds = riskCompliances.Select(rc => rc.domain_id).Distinct().ToList();
                var subCategoryIds = riskCompliances.Select(rc => rc.subCategory_id).Distinct().ToList();

                var categoriesTask = _context._category.Find(c => categoryIds.Contains(c.ID.ToString())).ToListAsync();
                var domainsTask = _context._domains.Find(d => domainIds.Contains(d.ID.ToString())).ToListAsync();
                var subCategoriesTask = _context._subCategories.Find(sc => subCategoryIds.Contains(sc.ID.ToString())).ToListAsync();

                await Task.WhenAll(categoriesTask, domainsTask, subCategoriesTask);

                var categories = await categoriesTask;
                var domains = await domainsTask;
                var subCategories = await subCategoriesTask;

                // Associate related data
                foreach (var riskCompliance in riskCompliances)
                {
                    riskCompliance.category = categories.FirstOrDefault(c => c.IdAsString == riskCompliance.category_id);
                    riskCompliance.domain = domains.FirstOrDefault(d => d.IdAsString == riskCompliance.domain_id);
                    riskCompliance.sub_category = subCategories.FirstOrDefault(sc => sc.IdAsString == riskCompliance.subCategory_id);
                }
            }

            // Count total records
            int totalRecords = (int)await _context._riskCompliance.CountDocumentsAsync(filter);
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            // Map RiskCompliance items to DTOs
            var mapRiskCompliances = _mapper.Map<List<RiskComplianceItemDto>>(riskCompliances);

            return new ApiResponse<RiskComplianceItemDto>
            {
                current_page = page,
                data = mapRiskCompliances,
                first_page_url = $"{_baseUrl}?page=1",
                from = (page - 1) * pageSize + 1,
                last_page = totalPages,
                last_page_url = $"{_baseUrl}?page={totalPages}",
                links = new List<Link>
        {
            new Link { url = page > 1 ? $"{_baseUrl}?page={page - 1}" : null, label = "&laquo; Previous", active = false },
            new Link { url = page < totalPages ? $"{_baseUrl}?page={page + 1}" : null, label = "Next &raquo;", active = false }
        },
                next_page_url = page < totalPages ? $"{_baseUrl}?page={page + 1}" : null,
                path = _baseUrl,
                per_page = pageSize,
                prev_page_url = page > 1 ? $"{_baseUrl}?page={page - 1}" : null,
                to = Math.Min(page * pageSize, totalRecords),
                total = totalRecords
            };
        }


        public async Task<RiskComplianceItemDto> GetByIdAsync(string id)
        {
            // Parse the string id to an ObjectId
            if (!ObjectId.TryParse(id, out var parsedId))
            {
                Console.WriteLine("Invalid ID format.");
                return null; // or return an appropriate error, e.g., throw a custom exception
            }

            // Create a filter to query by ID (as ObjectId)
            var filter = Builders<RiskComplianceItem>.Filter.Eq(r => r.ID, parsedId);

            // Fetch the RiskCompliance document using the filter
            var riskCompliance = await _context._riskCompliance
                .Find(filter)
                .FirstOrDefaultAsync();

            if (riskCompliance == null)
            {
                Console.WriteLine("RiskCompliance not found!");
                return null; // Handle the case appropriately, e.g., throw NotFoundException or return NotFound
            }

            // Manually load related entities (category, domain, sub-category)
            riskCompliance.category = await _context._category
                .Find(c => c.ID.ToString() == riskCompliance.category_id)
            .FirstOrDefaultAsync();

            riskCompliance.domain = await _context._domains
                .Find(d => d.ID.ToString() == riskCompliance.domain_id)
            .FirstOrDefaultAsync();

            riskCompliance.sub_category = await _context._subCategories
                .Find(sc => sc.ID.ToString() == riskCompliance.subCategory_id)
                .FirstOrDefaultAsync();
            // Load the related activities where the subject_id matches the current RiskCompliance's ID
            riskCompliance.activity = await _context._activity_log
                .Find(a => a.subject_id == riskCompliance.IdAsString)
                .ToListAsync();
            foreach (var act in riskCompliance.activity)
            {
                var causer = await _context.Users
                    .Find(u => u.ID.ToString() == act.causer_id.ToString())
                    .FirstOrDefaultAsync();

                act.causer = causer; // Assuming causer is a property of the activity log
            }

            // Map the populated riskCompliance object to a DTO (RiskComplianceItemDto)
            var complianceDto = _mapper.Map<RiskComplianceItemDto>(riskCompliance);
            return complianceDto;
        }

        public async Task UpdateAsync(string id, RiskComplianceRequestDto risk)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentException("Invalid ObjectId format.", nameof(id));
            }

            // Fetch the existing document
            var existingRiskCompliance = await _context._riskCompliance
                .Find(r => r.ID == objectId)
                .FirstOrDefaultAsync();

            if (existingRiskCompliance == null)
            {
                throw new ArgumentException("RiskCompliance not found.", nameof(id));
            }

            // Validation of the required fields, similar to Laravel's validation logic
            if (string.IsNullOrEmpty(risk.standard) || string.IsNullOrEmpty(risk.healthcareDomain.ToString()) ||
                string.IsNullOrEmpty(risk.category.ToString()) || string.IsNullOrEmpty(risk.subCategory.ToString()) ||
                !risk.likelihood.HasValue || !risk.impact.HasValue || string.IsNullOrEmpty(risk.legal_descriptions))
            {
                throw new ArgumentException("Missing required fields.", nameof(id));
            }

            // Handle risk calculation logic
            decimal? score = null;
            string likelihoodPostfix = null;
            string impactPrefix = null;
            decimal? riskValue = null;
            decimal? weightage = null;

            if (risk.likelihood.HasValue)
            {
                likelihoodPostfix = GetImpactCategory(risk.likelihood.Value);
            }
            if (risk.impact.HasValue)
            {
                impactPrefix = GetImpactCategory(risk.impact.Value);
            }
            if (risk.likelihood.HasValue && risk.impact.HasValue)
            {
                score = (decimal?)(risk.likelihood.Value * risk.impact.Value);
            }

            if (likelihoodPostfix != null && impactPrefix != null)
            {
                weightage = (decimal?)GetWeightage(impactPrefix, likelihoodPostfix);
                if (score.HasValue)
                {
                    riskValue = weightage * score;
                    riskValue = Math.Round(riskValue.Value, 2); // rounding to two decimal places
                }
            }

            // Update the Identifier field
            string identifier = existingRiskCompliance.identifier;
            if (!string.IsNullOrEmpty(risk.identifier))
            {
                var parts = identifier.Split('-');
                var identifierNumber = parts.Last();
                identifier = $"{risk.identifier}-{identifierNumber}";
            }
            var updateDefinition = Builders<RiskComplianceItem>.Update
                                    .Set(r => r.standard, risk.standard)
                                    .Set(r => r.legal_descriptions, risk.legal_descriptions)
                                    .Set(r => r.domain_id, risk.healthcareDomain.ToString())
                                    .Set(r => r.category_id, risk.category.ToString())
                                    .Set(r => r.subCategory_id, risk.subCategory.ToString())
                                    .Set(r => r.likelihood, risk.likelihood)
                                    .Set(r => r.impact, risk.impact)
                                    //.Set(r => r.likelihood_postfix, likelihoodPostfix)
                                    //.Set(r => r.impact_prefix, impactPrefix)
                                    .Set(r => r.score, score)
                                    .Set(r => r.weightage, weightage)
                                    .Set(r => r.risk, riskValue)
                                    .Set(r => r.identifier, identifier)
                                    .Set(r => r.active, true);

            // Update the document in the database
            var updateResult = await _context._riskCompliance.UpdateOneAsync(
                                  r => r.ID == objectId, updateDefinition);

            // Log the activity for creation
            var user = _userRepository.GetUserFromHttpContextAsync();
            List<string> changedFields = new List<string> { "DomainId", "CategoryId", "SubCategoryId" };
            await LogActivityAsync(id, "updated", user.Result.name, changedFields, user.Result._id);
        }
        public async Task<RiskComplianceNumber> GetRiskComplianceNumberAsync()
        {
            return await _context.risk_compliance_numbers.Find(Builders<RiskComplianceNumber>.Filter.Empty).FirstOrDefaultAsync();
        }

        public async Task<RiskComplianceItem> GetLastRiskComplianceAsync()
        {
            return await _context._riskCompliance
                .Find(Builders<RiskComplianceItem>.Filter.Empty)
                .SortByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();
        }
        public (string LikelihoodPostfix, string ImpactPrefix, decimal? Risk) CalculateRiskData(decimal? likelihood, decimal? impact)
        {
            string likelihoodPostfix = likelihood.HasValue ? GetImpactCategory(likelihood.Value) : null;
            string impactPrefix = impact.HasValue ? GetImpactCategory(impact.Value) : null;
            decimal? score = likelihood.HasValue && impact.HasValue ? likelihood.Value * impact.Value : null;
            decimal? risk = null;
            decimal? weightage = null;

            if (likelihoodPostfix != null && impactPrefix != null)
            {
                weightage = (decimal?)GetWeightage(impactPrefix, likelihoodPostfix);
                if (score.HasValue)
                {
                    risk = weightage * score.Value;
                    risk = Math.Round(risk.Value, 2);
                }
            }

            return (likelihoodPostfix, impactPrefix, risk);
        }


        public async Task<MemoryStream> ExportToExcelAsync(int page = 1, int pageSize = 20000, string? search = null, string? regionId = null)
        {
            try
            {
                // Fetch the data with pagination
                var skip = (page - 1) * pageSize;
                var filter = Builders<RiskComplianceItem>.Filter.Empty;

                if (!string.IsNullOrEmpty(search))
                {
                    filter &= Builders<RiskComplianceItem>.Filter.Regex(x => x.identifier, new BsonRegularExpression(search, "i"));
                }

                if (!string.IsNullOrEmpty(regionId))
                {
                    filter &= Builders<RiskComplianceItem>.Filter.Eq(x => x.domain.name, regionId);
                }

                // Fetch risk compliance items based on the filter
                var riskCompliances = await _context._riskCompliance
                    .Find(filter)
                    .Skip(skip)
                    .Limit(pageSize)
                    .ToListAsync();

                // Collect related data for Category, Domain, and SubCategory
                var categoryIds = riskCompliances.Select(rc => rc.category_id).Distinct().ToList();
                var domainIds = riskCompliances.Select(rc => rc.domain_id).Distinct().ToList();
                var subCategoryIds = riskCompliances.Select(rc => rc.subCategory_id).Distinct().ToList();

                // Query the related collections based on their respective IDs
                var categories = await _context._category
                    .Find(c => categoryIds.Contains(c.ID.ToString()))
                    .ToListAsync();

                var domains = await _context._domains
                    .Find(d => domainIds.Contains(d.ID.ToString()))
                    .ToListAsync();

                var subCategories = await _context._subCategories
                    .Find(sc => subCategoryIds.Contains(sc.ID.ToString()))
                    .ToListAsync();

                // Prepare the data to export
                var data = new List<List<string>>();

                // Add the header row (column names)
                var tempHead = new List<string>
        {
            "Identifier", "Standard", "Domain", "Legal Description", "Category", "Sub Category",
            "Likelihood", "Impact", "Likelihood Postfix", "Impact Prefix", "Score", "Weightage", "Risk"
        };
                data.Add(tempHead);

                // Add each row of data
                foreach (var compliance in riskCompliances)
                {
                    var category = categories.FirstOrDefault(c => c.ID.ToString() == compliance.category_id.ToString());
                    var domain = domains.FirstOrDefault(d => d.ID.ToString() == compliance.domain_id.ToString());
                    var subCategory = subCategories.FirstOrDefault(sc => sc.ID.ToString() == compliance.subCategory_id.ToString());

                    var row = new List<string>
            {
                compliance.identifier ?? "-",
                compliance.standard ?? "-",
                domain?.name ?? "-",  // Assign domain name if available, else fallback to "-"
                compliance.legal_descriptions ?? "-",
                category?.name ?? "-",  // Assign category name if available, else fallback to "-"
                subCategory?.name ?? "-",  // Assign subCategory name if available, else fallback to "-"
                compliance.likelihood.ToString() ?? "-",
                compliance.impact.ToString() ?? "-",
                compliance.likelihood_postfix.ToString() ?? "-",
                compliance.impact_prefix.ToString() ?? "-",
                compliance.score?.ToString() ?? "-",
                compliance.weightage?.ToString() ?? "-",
                compliance.risk?.ToString() ?? "-"
            };

                    data.Add(row);
                }

                // Create the Excel file
                var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("RiskCompliances");

                // Write the data into the worksheet
                for (int rowIndex = 0; rowIndex < data.Count; rowIndex++)
                {
                    var row = data[rowIndex];
                    for (int colIndex = 0; colIndex < row.Count; colIndex++)
                    {
                        worksheet.Cells[rowIndex + 1, colIndex + 1].Value = row[colIndex];
                    }
                }

                // Save the Excel file to a MemoryStream
                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);
                memoryStream.Position = 0;  // Reset the position to the start of the stream

                return memoryStream;
            }
            catch (Exception ex)
            {
                // Handle any errors
                // You can log the error or handle it accordingly
                throw new Exception("An error occurred while exporting data to Excel.", ex);
            }
        }

        private string GetImpactCategory(decimal value)
        {
            return value > 3 ? "High" : "Low"; // Example logic
        }

        private double GetWeightage(string impactPrefix, string likelihoodPostfix)
        {
            return (impactPrefix == "High" && likelihoodPostfix == "High" ? 1.5 : 1.0); // Example logic
        }

        private async Task UpdateRiskComplianceNumberAsync(RiskComplianceNumber riskComplianceNumber)
        {
            // Assuming 'Id' is the ObjectId field in MongoDB
            var filter = Builders<RiskComplianceNumber>.Filter.Eq(r => r.ID, riskComplianceNumber.ID);
            var update = Builders<RiskComplianceNumber>.Update.Set(r => r.RiskCount, riskComplianceNumber.RiskCount);

            await _context.risk_compliance_numbers.UpdateOneAsync(filter, update);
        }

        public async Task<List<CategoriesDomainsDto>> categories()
        {
            // Fetch related collections based on their respective IDs
            var categories = await _context._category
                .Find(_ => true)
                .ToListAsync();

            var domains = await _context._domains
                .Find(_ => true)
                .ToListAsync();

            var subcategories = await _context._subCategories
                .Find(_ => true)
                .ToListAsync();

            // Create a list to store the results containing category and domain information
            var result = new List<CategoriesDomains>();

            // Iterate through each category to filter and assign subcategories and domains
            foreach (var category in categories)
            {
                // Filter subcategories based on category ID and assign it to the category directly
                category.sub_category = subcategories.Where(sc => sc.category_id.ToString() == category.ID.ToString()).ToList();

                // Filter domains based on category ID or other domain-category association logic
                var relatedDomains = domains.Where(d => d.ID.ToString() == category.ID.ToString()).ToList();

            }
            // Create a CategoriesDomains object that holds this category, related domains, and its subcategories
            var categoriesDomains = new CategoriesDomains
            {
                category = categories,               // Assign the category
                domain = domains,          // Assign the list of related domains
            };

            // Add to the result list
            result.Add(categoriesDomains);

            var resultMap = _mapper.Map<List<CategoriesDomainsDto>>(result);

            // Return the list containing category, domain, and subcategory information
            return resultMap;
        }

        public async Task<List<activity_logDto>> activity()
        {
            var activity = await _context._activity_log.Find(_ => true).ToListAsync();

            // Use AutoMapper to map the entities to DTOs
            var mappedActivity = _mapper.Map<List<activity_logDto>>(activity);

            return mappedActivity;
        }


        // Method to log activity in the database
        public async Task LogActivityAsync(string subjectId, string eventName, string causerName, List<string> changedFields , string userid)
        {
            // Get the activity description based on the event and changed fields
            string description = GetActivityDescription(eventName, changedFields);

            var activityLog = new activity_log
            {
                subject_id = subjectId,
                description = description,
                event_ = eventName,
                causer_type = causerName,
                CreatedDate = DateTime.UtcNow.Date,
                UpdatedDate=DateTime.UtcNow.Date,
                causer_id = userid,
                CreatedBy=DateTime.UtcNow.Date.ToString(),
                UpdatedBy=DateTime.UtcNow.Date.ToString()
            };

            // Insert the log into the ActivityLogs collection
            await _context._activity_log.InsertOneAsync(activityLog);
        }

        // Method to build activity description based on event type
        private string GetActivityDescription(string eventName, List<string> changedFields)
        {
            var fieldMapping = new Dictionary<string, string>
        {
            { "DomainId", "Domain" },
            { "CategoryId", "Category" },
            { "SubCategoryId", "SubCategory" }
        };

            var updatedFields = changedFields
                .Where(f => f != "UpdatedAt")  // Remove "UpdatedAt" from the changes
                .Select(f => fieldMapping.ContainsKey(f) ? fieldMapping[f] : f)
                .ToList();

            // Description based on event type
            if (eventName == "created")
            {
                return "Standard is created by";
            }
            else if (eventName == "updated" && updatedFields.Any())
            {
                return $"{string.Join(", ", updatedFields)} updated by";
            }
            else if (eventName == "deleted")
            {
                return "Deleted by";
            }
            else
            {
                return $"RiskCompliance model with ID  was {eventName} by";
            }
        }

    }
}
