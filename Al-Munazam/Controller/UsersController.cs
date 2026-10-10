using Microsoft.AspNetCore.Mvc;
using Application.Entities;
using Application.IRepositories;
using System.Text.Json;
using Infrastructure.Configurations;
using Domain.Services;
using Application.DTOs.User;
using Domain.Repositories;
using Application.Resolvers;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessTokens _tokenService;
        private readonly IEmailService _emailService;
        private readonly EmailSettings _emailSettings;
        public UsersController(IUserRepository userRepository, IAccessTokens tokenService, IEmailService emailService, EmailSettings emailSettings)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _emailService = emailService;
            _emailSettings = emailSettings;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _userRepository.AddUserAsync(userDto);

                // Run update operation in parallel for better performance
                await _userRepository.UpdateRegionClusterFacilityWithUserId(
                    result._id.ToString(), result.region_ids, result.clusters_ids, result.obligation_sites_ids, result.module_ids, result.sub_region_id, result.sub_sector_id
                );

                //await updateTask; // Ensure completion before returning

                return Ok(new { data = "User Successfully Created" });
            }
            catch (System.Text.Json.JsonException ex)
            {
                return BadRequest($"Invalid JSON format in SME_regions: {ex.Message}");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Email has already been taken.")
                    return UnprocessableEntity(new
                    {
                        message = "The email has already been taken.",
                        errors = new { email = new[] { "The email has already been taken." } }
                    });

                throw;
            }
        }

        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ApproveSite(string id)
        {
            var userData = await _userRepository.GetByIdAsync(id);
            if (userData == null)
            {
                return NotFound(new { data = "User not found!" });
            }

            var user = await _userRepository.GetUserFromHttpContextAsync();
            if (!(user.IsAdmin() || user.IsRegional() || user.IsSupervisor()))
            {
                return Unauthorized(new { data = "You are not authorized to change the status!" });
            }

            userData.status = "active";
            await _userRepository.UpdateAsync(id, userData);

            return Ok(new { data = "User Approved Successfully" });
        }


        [HttpPost("user-status-change/{id}")]
        public async Task<IActionResult> UserStatusChange(string id)
        {
            try
            {
                var userDetail = await _userRepository.GetUserByIdAsync(id);
                if (userDetail == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                // Toggle the user status between 'active' and 'in_active'
                if (userDetail.status == "active")
                {
                    userDetail.status = "in_active";  // Toggle to 'in_active'
                    await _tokenService.DeleteAccessTokenAsync(id);  // Delete access token when going to 'in_active'
                }
                else if (userDetail.status == "in_active")
                {
                    userDetail.status = "active";  // Toggle to 'active'
                }
                else
                {
                    userDetail.status = "active";
                }

                // Update user status in the database
                var updateResult = await _userRepository.UpdateUserStatusAsync(id, userDetail);
                if (updateResult == null)
                {
                    return StatusCode(500, new { message = "Failed to update user status" });
                }

                return Ok(new { data = "User status successfully changed" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpGet("GetUserDetail/{id}")]
        public async Task<IActionResult> GetUserDetail(string id)
        {
            try
            {
                var userDetail = await _userRepository.GetUserByIdAsync(id);
                if (userDetail == null) return NotFound();
                userDetail.password = null;
                // Remove null and empty lists before returning
                var cleanedObject = ObjectCleaner.RemoveNullAndEmptyProperties(userDetail);

                return new JsonResult(new { data = cleanedObject }, new JsonSerializerOptions { PropertyNamingPolicy = null });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        

        [HttpGet("UserProfile")]
        public async Task<IActionResult> UserProfile()
        {
            try
            {
                var userDetail = await _userRepository.GetUserProfileFromHttpContextAsync();
                if (userDetail == null) return NotFound();

                // Remove null and empty lists before returning
                var cleanedObject = ObjectCleaner.RemoveNullAndEmptyProperties(userDetail);

                return new JsonResult(new { data = cleanedObject }, new JsonSerializerOptions { PropertyNamingPolicy = null });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto user)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByIdAsync(id);
                if (existingUser == null) return NotFound();

                var updatedUser = await _userRepository.UpdateUserAsync(id, user);

                // Update regions, clusters, facilities, and modules in parallel
                var updateTask = _userRepository.UpdateRegionClusterFacilityWithUserId(
                    id, updatedUser.region_ids, updatedUser.clusters_ids,
                    updatedUser.obligation_sites_ids, updatedUser.module_ids,  updatedUser.sub_region_id,updatedUser.sub_sector_id
                );

                await updateTask;
                return Ok(new { message = "User Updated Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPut("UpdateUserByFieldName")]
        public async Task<IActionResult> UpdateUserByFieldName([FromBody] UserDto user)
        {
            try
            {
                var userDto= await _userRepository.GetUserFromHttpContextAsync();
                var existingUser = await _userRepository.GetUserProfileAsync(userDto.IdAsString);
                if (existingUser == null) return NotFound();

                var updatedUser = await _userRepository.UpdateUserByFieldName(userDto.IdAsString, user);
               
                return Ok(new { message = "User fields Updated Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



        [HttpGet("user-index")]
        public async Task<ActionResult<IEnumerable<Users>>> GetAll([FromQuery] int page, [FromQuery] int per_page = 10, [FromQuery] string? search = null, [FromQuery] string? status = "active")
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync(page, per_page, status, search);
                var data = new
                {
                    data = users,
                    resultCount = users.resultCount_
                };
                return Ok(data);
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        [HttpGet("export")]
        public async Task<ActionResult<IEnumerable<Users>>> export([FromQuery] int page, [FromQuery] int per_page = 10, [FromQuery] string? search = null, [FromQuery] string? status = "active")
        {
            var users = await _userRepository.GetAllUsersAsync(page, per_page, status, search);
            var data = new
            {
                data = users
            };
            return Ok(data);

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                await _userRepository.DeleteUserAsync(id);
                return Ok(new { data = "user Successfully Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpDelete("ScheduleUserDeletionAsync/{id}")]
        public async Task<IActionResult> ScheduleUserDeletionAsync(string id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                await _userRepository.ScheduleUserDeletionAsync(id);
                return Ok(new { data = "This account will be deleted after 24 hours." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string roleId)
        {
            await _userRepository.AddRoleToUserAsync(userId, roleId);
            return Ok();
        }

        [HttpDelete("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleId)
        {
            await _userRepository.RemoveRoleFromUserAsync(userId, roleId);
            return Ok();
        }

        [HttpGet("TestEmail")]
        public async Task<IActionResult> TestEmail()
        {
            try
            {
                // Get the current directory of the application
                string basePath = Directory.GetCurrentDirectory();

                // Remove the 'Al-Munazam' part from the base path
                string trimmedBasePath = basePath.Replace("\\Al-Munazam", string.Empty);
                string templateSubPath = _emailSettings.Paths[EmailTemplatePath.EmailCreationPath];

                // Now, build the correct path to the EmailTemplates folder
                string templatePath = Path.Combine(trimmedBasePath, templateSubPath);

                // Generate the email body using the template
                string emailBody = await _emailService.GenerateUserCreationEmail("Usman", "muhammad.usman@ascend.com.sa", "ABCD", templatePath);

                // Send the email asynchronously
                List<string> recipients = new List<string> { "muhammad.usman@ascend.com.sa" };
                await _emailService.SendEmailAsync(recipients, "Welcome to Efficax!", emailBody);

                // Return success response with email body
                return new JsonResult(new { data = emailBody }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                // Return a structured error response with a custom error code or message
                return BadRequest(new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }
    }
}
