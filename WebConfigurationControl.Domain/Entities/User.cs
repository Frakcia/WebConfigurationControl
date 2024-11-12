namespace WebConfigurationControl.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public List<EventSubscription> Subscriptions { get; set; }
        public List<UserEventNotification> UserEventNotifications { get; set; }
    }
}
