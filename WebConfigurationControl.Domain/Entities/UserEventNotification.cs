namespace WebConfigurationControl.Domain.Entities
{
    public class UserEventNotification
    {
        public Guid EventNotificationId { get; set; }
        public EventNotification EventNotification { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public bool IsViewed { get; set; }
    }
}
