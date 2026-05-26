
using SupplyChain360.DTOs.Notifications;
using SupplyChain360.Models.Notifications;

namespace SupplyChain360.Services.Notifications.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(NotificationDto dto);
        Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        Task<IEnumerable<Notification>> GetNotificationsByUserAsync(string userId);
    }
}
