using WebConfigurationControl.Domain.Entities;

namespace WebConfigurationControl.Infrastructure.Contracts
{
    public interface IEventSubscriptionRepository : IBaseRepository<EventSubscription>
    {
        Task<IEnumerable<EventSubscription>> GetByEventId(Guid eventId);
        Task<IEnumerable<EventSubscription>> GetByUserIdAndEventIds(Guid userId, IEnumerable<Guid> eventIds);
    }
}
