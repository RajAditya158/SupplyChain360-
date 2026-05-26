namespace SupplyChain360.Models.Notifications
{
    public class Notification
    {
        public int NotificationId { get; set; }   // Primary Key
        public string UserId { get; set; }        // VARCHAR(20)
        public string Message { get; set; }       // TEXT
    }
}
