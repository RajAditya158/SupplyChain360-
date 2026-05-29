

using Microsoft.AspNetCore.Mvc;
using SupplyChain360.DTOs.Notifications;
using SupplyChain360.Services.Notifications.Interfaces;
using SupplyChain360.Enums.Notifications;

namespace SupplyChain360.Controllers.Notifications
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] NotificationDto dto)
        {
            await _service.SendNotificationAsync(dto);
            return Ok(new { message = "Notification sent successfully" });
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _service.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        [HttpGet("user/{UserId}")]
        public async Task<IActionResult> GetNotificationsByUser(NotificationUserId UserId)
        {
            var notifications = await _service.GetNotificationsByUserAsync(UserId);
            return Ok(notifications);
        }
    }
}
