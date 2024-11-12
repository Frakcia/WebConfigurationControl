namespace WebConfigurationControl.Domain.Entities
{
    public class EventNotification : Entity
    {
        private EventNotification() { }

        public EventNotification(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
        public List<UserEventNotification> UserEventNotifications { get; set; }

        public void UpdateEventNotifications(List<UserEventNotification> userEventNotifications)
        {
            UserEventNotifications = userEventNotifications;
        }
    }
}
