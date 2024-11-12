namespace WebConfigurationControl.Domain.Entities
{
    public class EventSubscription : Entity
    {
        private EventSubscription() { }
        public EventSubscription(Guid eventId, Guid userId)
        {
            EventId = eventId;
            UserId = userId;
        }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
