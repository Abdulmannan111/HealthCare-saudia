using AutoMapper;
using MimeKit;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using Application.Pagination;
using MongoDB.Bson;
using MongoDB.Driver;
using Infrastructure.Mongo;
//using MailKit.Net.Smtp;
using MailKit.Security;
using Domain.Services;
using Application.DTOs.User;
using Application.Entities.ApiSettings;
using Microsoft.Extensions.Options;

namespace Domain.Repositories
{
    public class CustomNotificationRepository : ICustomNotificationRepository
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userService;
        private readonly string _baseUrl;

        public CustomNotificationRepository(IOptions<ApiSettings> apiSettings, MongoDBContext context, IMapper mapper, IUserRepository userService, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _emailService = emailService;
            _baseUrl = apiSettings.Value.BaseUrl + "CustomNotification";
        }


        public async Task<ApiResponse> CreateAsync(CreateNotificationRequest request)
        {
            try
            {
                var user = await _userService.GetUserFromHttpContextAsync();
                var notification = new CustomNotification
                {
                    subject = request.subject,
                    content = request.content,
                    is_individual = request.is_individual,
                    user_id = user._id,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    user_emails = request.is_individual ? request.user_emails : null,
                    user_roles = request.is_individual ? null : request.user_roles
                };

                await _context.CustomNotifications.InsertOneAsync(notification);

                bool isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
                List<Users> users = isProduction
                    ? request.is_individual
                        ? await _userService.GetUsersByEmailsAsync(request.user_emails)
                        : await _userService.GetUsersByRolesAsync(request.user_roles)
                    : await _userService.GetUsersByEmailsAsync(new List<string>
                        {                  
                    "muhammad.ilyas@ascend.com.sa",
                    "romeesa.ashfaq@ascend.com.sa",
                    "bilal.ahmed@ascend.com.sa",
                    "abdul.mannan@ascend.com.sa",
                        });

                if (users.Any())
                {
                    var emailRecipients = users.Select(u => u.email).Distinct().ToList();
                    _ = Task.Run(() => _emailService.SendEmailAsync(emailRecipients, notification.subject, notification.content));
                }

                return new ApiResponse { Data = "Notification successfully created", StatusCode = 200 };
            }
            catch (Exception ex)
            {
                // Log exception
                return new ApiResponse { Data = $"Error: {ex.Message}", StatusCode = 500 };
            }
        }


        public async Task<ApiResponse<CustomNotificationDto>> PaginateAsync(int page = 0, int perPage = 20, string search = null)
        {
            var filter = Builders<CustomNotification>.Filter.Empty; // Start with an empty filter (i.e., match everything)
            if (perPage <= 0)
            {
                perPage = 20;  // Set to default value if invalid
            }

            if (page <= 0)
            {
                page = 1;  // Set to default value if invalid
            }
            // Apply search filter if there's a search term
            if (!string.IsNullOrEmpty(search))
            {
                filter = Builders<CustomNotification>.Filter.Where(n => n.subject.Contains(search));
            }

            // Sorting by CreatedAt in descending order
            var sort = Builders<CustomNotification>.Sort.Descending(n => n.CreatedDate);

            // Fetch the total count of notifications for pagination
            var totalCount = await _context.CustomNotifications.CountDocumentsAsync(filter);

            // Paginate the query using Skip and Limit
            var notifications = await _context.CustomNotifications
                .Find(filter)
                .Sort(sort)
                .Skip((page - 1) * perPage)
                .Limit(perPage)
                .ToListAsync();
            var userIds = notifications.Select(n => n.user_id).Distinct()
            .Select(id => ObjectId.Parse(id)) // Convert string to ObjectId
            .ToList();

            // Fetch users with the matching ObjectIds
            var users = await _context.Users
                         .Find(u => userIds.Contains(u.ID)) // Query using ObjectId
                         .ToListAsync();

            // Step 2: Map the list of User entities to UserDto using AutoMapper
            var userDtos = _mapper.Map<List<UserDto>>(users);
            var notificationDtos = _mapper.Map<List<CustomNotificationDto>>(notifications);

            // Step 2: Loop through each notificationDto and map the user separately
            foreach (var notificationDto in notificationDtos)
            {
                notificationDto.user = userDtos.FirstOrDefault(c => c.IdAsString == notificationDto.user_id);

            }
            // Map to DTOs
            // Count total records
            var totalRecords = await _context.CustomNotifications.CountDocumentsAsync(filter);
            var totalPages = (int)Math.Ceiling(totalRecords / (double)perPage);

            return new ApiResponse<CustomNotificationDto>
            {
                current_page = page,
                data = notificationDtos.ToList(),
                first_page_url = $"{_baseUrl}?page=1",
                from = (page - 1) * perPage + 1,
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
                per_page = perPage,
                prev_page_url = page > 1 ? $"{_baseUrl}?page={page - 1}" : null,
                to = Math.Min(page * perPage, (int)totalRecords),
                total = (int)totalRecords,
            };
        }

        private async Task SendEmailAsync(List<string> recipients, string subject, string body)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Mohammad Usman", "muhammad.usman@ascend.com.sa"));

                foreach (var recipient in recipients)
                {
                    emailMessage.To.Add(new MailboxAddress("", recipient));
                }

                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("plain") { Text = body };

                using var client = new MailKit.Net.Smtp.SmtpClient();
                await client.ConnectAsync("sandbox.smtp.mailtrap.io", 2525, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("0095cd66401f25", "4db3a5f17991ab");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
            }
        }

    }
}