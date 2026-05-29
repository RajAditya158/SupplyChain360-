using SupplyChain360.Enums.Notifications;

namespace SupplyChain360.Models.Notifications
{
    public class Notification
    {
        public int NotificationId { get; set; }   // Primary Key
        public NotificationUserId UserId { get; set; }
        public string Message { get; set; }       // TEXT
    }
}
