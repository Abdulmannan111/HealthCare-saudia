using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Application.Entities;
using Application.IRepositories;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomNotificationController : ControllerBase
    {
        private readonly ICustomNotificationRepository _notificationService;
        public CustomNotificationController(ICustomNotificationRepository notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index([FromQuery] int page = 0, [FromQuery] int per_page = 10, string search = null)
        {
            try
            {
                // Retrieve clusters
                var result = await _notificationService.PaginateAsync(page, per_page, search);
                var data = new
                {
                    data = result
                };
                return Ok(data);
            }
            catch (SecurityTokenException ex)
            {
                // Token validation failed
                return Unauthorized(new { message = "Invalid or expired token", error = ex.Message });
            }

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _notificationService.CreateAsync(request);

            return StatusCode(result.StatusCode, new { data = result.Data });
        }
    }
}
