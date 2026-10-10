using Microsoft.AspNetCore.Mvc;
using Application.IRepositories;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IFirebaseService _notificationService;

        public NotificationsController(IFirebaseService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification(string token, string title, string body)
        {
            try
            {
                await _notificationService.SendNotificationAsync(token, title, body);
                return Ok("Notification sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateNotification(string Fcmtoken, string userId)
        {
            try
            {
                await _notificationService.UpdateTokenAsync(Fcmtoken, userId);
                return Ok("Notification update successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

