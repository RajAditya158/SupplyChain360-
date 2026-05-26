using Microsoft.EntityFrameworkCore;
using Supplychain.Data;
using SupplyChain360.Models.Notifications;
using SupplyChain360.Repositories.Notifications.Interfaces;

namespace SupplyChain360.Repositories.Notifications.Implementation
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly SupplyChainContext _context;

        public NotificationRepository(SupplyChainContext context)
        {
            _context = context;
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserAsync(string userId)
        {
            return await _context.Notifications
                                 .Where(n => n.UserId == userId)
                                 .ToListAsync();
        }
    }
}
