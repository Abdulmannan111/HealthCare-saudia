using System.Text.Json;
using AutoMapper;
using Application.Entities;
using Application.IRepositories;
using MongoDB.Driver;
using Infrastructure.Mongo;
using Application.DTOs.Form;
using Application.Entities.Form;
using Microsoft.AspNet.Identity;
using Application.DTOs.User;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Domain.Repositories
{
    public class FormSubmissionRepository : IFormSubmission
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public FormSubmissionRepository(MongoDBContext context, IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<FormSubmissionsDto> CreateFormSubmissionAsync(FormSubmissionsDto? requestData)
        {
            try
            {
                requestData.report_no = await GenerateReportNo();

                // Simulate user authentication (replace with actual auth logic)
                var user = await _userRepository.GetUserFromHttpContextAsync();
                var threeMonthsAgo = DateTime.UtcNow.AddMonths(-3);
                if (user.type != "facility")
                {
                    requestData.status = "approved";
                }
                else
                {
                    // Fetch all submissions matching the criteria
                    //
                    var submissions = await _context._form_submissions
                        .Find(x => x.form_id == requestData.id && x.user_id == requestData.user_id && x.inspection_date >= threeMonthsAgo)
                        .ToListAsync();

                    // Filter submissions in memory
                    var existingSubmissions = submissions
                        .Where(x =>
                        {
                            if (x.data is JsonElement dataElement &&
                                dataElement.TryGetProperty("site", out var dataSiteElement) &&
                                dataSiteElement.TryGetProperty("value", out var dataSiteValue))
                            {
                                string? siteId = null;
                                return dataSiteValue.GetString() == siteId;
                            }
                            return false;
                        })
                        .ToList();

                    if (existingSubmissions.Count > 0)
                    {
                        return null;
                    }
                }
                var res = _mapper.Map<FormSubmissions>(requestData);
                res.ID = default;
                await _context._form_submissions.InsertOneAsync(res);

                return requestData;
                }
            catch(Exception ex)
            {
                return null; 
            }
        }
        
        public int GetImpactCategory(double impactValue)
        {
            if (impactValue >= 1 && impactValue <= 3.0)
                return 1; // 'Minor'
            if (impactValue >= 3.5 && impactValue <= 4.5)
                return 3; // 'Moderate'
            if (impactValue >= 5.0 && impactValue <= 7.5)
                return 4; // 'Major'
            if (impactValue >= 8 && impactValue <= 10)
                return 7; // 'Catastrophic'

            return -1; // 'Invalid Impact Value'
        }


        public (int Weightage, string Priority)? GetDate(int impactPrefix, int likelihoodPostfix)
        {
            var riskMatrix = new List<(int ImpactPrefix, int LikelihoodPostfix, int Weightage, string Priority)>
    {
        (7, 7, 14, "critical"),
        (7, 4, 14, "critical"),
        (7, 3, 14, "critical"),
        (7, 1, 14, "critical"),
        (4, 7, 20, "high"),
        (4, 4, 20, "high"),
        (4, 3, 20, "high"),
        (4, 1, 20, "high"),
        (3, 7, 30, "medium"),
        (3, 4, 30, "medium"),
        (3, 3, 30, "medium"),
        (3, 1, 30, "medium"),
        (1, 7, 40, "low"),
        (1, 4, 40, "low"),
        (1, 3, 40, "low"),
        (1, 1, 40, "low")
    };

            var result = riskMatrix
                .FirstOrDefault(x => x.ImpactPrefix == impactPrefix && x.LikelihoodPostfix == likelihoodPostfix);

            return result != default ? (result.Weightage, result.Priority) : null;
        }
        public async Task<String?> GetFormByIdAsync(string id)
        {
            var key = _configuration["FormSecretKey:SecretKey"];
            var Url = _configuration["FormSecretKey:BaseUrl"];

            try
            {
                using var client = new HttpClient { BaseAddress = new Uri(Url) };
                var response = await client.GetAsync($"/api/form?id={id}&secret={key}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return jsonResponse;

                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<String?> GetSchemeFormByIdAsync(string id)
        {
            var key = _configuration["FormSecretKey:SecretKey"];
            var Url = _configuration["FormSecretKey:BaseUrl"];

            try
            {
                using var client = new HttpClient { BaseAddress = new Uri(Url) };
                var response = await client.GetAsync($"/api/form/{id}?secret={key}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return jsonResponse;

                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task<int> GenerateReportNo()
        {
            var collection = _context._reportNumbers;

            var result = await collection.Find(FilterDefinition<ReportNumber>.Empty).FirstOrDefaultAsync();
            int reportCountValue = 1;

            if (result != null)
            {
                reportCountValue = result.reportCount + 1;
                var update = Builders<ReportNumber>.Update.Set(r => r.reportCount, reportCountValue);
                await collection.UpdateOneAsync(Builders<ReportNumber>.Filter.Eq("_id", result.ID), update);
            }
            else
            {
                var newRecord = new ReportNumber { reportCount = reportCountValue };
                await collection.InsertOneAsync(newRecord);
            }

            return reportCountValue;
        }
        //public async Task<IEnumerable<FormSubmissionsDto>> GetFormSubmissionsAsync(string formId, string? submissionId, UserDto user)
        //{
        //    try
        //    {
        //        var filter = Builders<FormSubmissions>.Filter.Eq(f => f.form_id, formId);

        //        if (!string.IsNullOrEmpty(submissionId))
        //        {
        //            filter &= Builders<FormSubmissions>.Filter.Eq(f => f.form_id, submissionId);
        //        }

        //        if (user.type != "admin")
        //        {
        //            if (user.region_ids != null && user.IsTopThree())
        //            {
        //                filter &= Builders<FormSubmissions>.Filter.AnyIn("data.region.value", user.region_ids);
        //            }
        //            else if (user.IsClusterManager != null && (user.IsClusterManager() || user.isHoldCo()))
        //            {
        //                filter &= Builders<FormSubmissions>.Filter.AnyIn("data.cluster.value", user.clusters_ids);
        //            }
        //            else
        //            {
        //                filter &= Builders<FormSubmissions>.Filter.Eq(f => f.user_id, user.IdAsString);
        //            }
        //        }

        //        var forms = await _context._form_submissions.Find(filter).SortByDescending(f => f.CreatedDate).ToListAsync();
        //        return _mapper.Map<List<FormSubmissionsDto>>(forms);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        public async Task<IEnumerable<FormSubmissionsDto>> GetFormSubmissionsAsync(string formId, string? submissionId, UserDto user)
        {
            try
            {
                var filter = Builders<FormSubmissions>.Filter.Eq(f => f.form_id, formId); // Filter by form_id

                if (!string.IsNullOrEmpty(submissionId))
                {
                    filter &= Builders<FormSubmissions>.Filter.Eq(f => f.form_id, submissionId); // Further filter by submissionId if provided
                }

                // Handle user-based filtering
                if (user.type != "admin")
                {
                    if (user.region_ids != null && user.IsTopThree())
                    {
                        filter &= Builders<FormSubmissions>.Filter.AnyIn("data.region.value", user.region_ids);
                    }
                    else if (user.IsClusterManager != null && (user.IsClusterManager() || user.isHoldCo()))
                    {
                        filter &= Builders<FormSubmissions>.Filter.AnyIn("data.cluster.value", user.clusters_ids);
                    }
                    else
                    {
                        filter &= Builders<FormSubmissions>.Filter.Eq(f => f.user_id, user.IdAsString); // Filter by user_id if none of the other conditions apply
                    }
                }

                // Fetch the data from MongoDB with the filter and sorting by created_at (descending)
                var forms = await _context._form_submissions
                                          .Find(filter)
                                          .SortByDescending(f => f.CreatedDate) // Sort by created_at in descending order
                                          .ToListAsync();

                // Map the MongoDB results to DTOs (Data Transfer Objects)
                var formSubmissionsDto =_mapper.Map<List<FormSubmissionsDto>>(forms);

                // Assuming that the User and Region data are either embedded in the FormSubmissions document or separately fetched
                //foreach (var submission in formSubmissionsDto)
                //{
                //    // You can manually populate user data and regions if needed
                //    // Example: Assuming user data is embedded or has a reference to user_id and region data
                //    var userData = await _userRepository.GetUserDataByIdAsync(submission.user_id);
                //    var userRegionData = await _regionRepository.GetRegionDataByUserIdAsync(submission.user_id);

                //    submission.us = userData; // Set user data
                //    submission.User.Region = userRegionData; // Set the region data
                //}
                return formSubmissionsDto;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw;
            }
        }


    }
}
