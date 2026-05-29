

using SupplyChain360.Models.Notifications;
using SupplyChain360.Enums.Notifications;

namespace SupplyChain360.Repositories.Notifications.Interfaces
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
        Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        Task<IEnumerable<Notification>> GetNotificationsByUserAsync(NotificationUserId userId);
    }
}
