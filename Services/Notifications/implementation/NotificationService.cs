

using SupplyChain360.DTOs.Notifications;
using SupplyChain360.Models.Notifications;
using SupplyChain360.Repositories.Notifications.Interfaces;
using SupplyChain360.Services.Notifications.Interfaces;

namespace SupplyChain360.Services.Notifications.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task SendNotificationAsync(NotificationDto dto)
        {
            var notification = new Notification
            {
                UserId = dto.UserId,
                Message = dto.Message
            };

            await _repository.AddNotificationAsync(notification);
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _repository.GetAllNotificationsAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserAsync(string userId)
        {
            return await _repository.GetNotificationsByUserAsync(userId);
        }
    }
}
