using Microsoft.EntityFrameworkCore;
using WebConfigurationControl.Domain.Entities;
using WebConfigurationControl.Infrastructure.Contexts;
using WebConfigurationControl.Infrastructure.Contracts;

namespace WebConfigurationControl.Infrastructure.Repositories
{
    public class EventSubscriptionRepository : BaseRepository<EventSubscription>, IEventSubscriptionRepository
    {
        public EventSubscriptionRepository(AppDbContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<EventSubscription>> GetByEventId(Guid eventId)
        {
            return await _context.EventSubscriptions
                .Where(e => e.EventId == eventId)
                .ToListAsync();
        }

        public async Task<IEnumerable<EventSubscription>> GetByUserIdAndEventIds(Guid userId, IEnumerable<Guid> eventIds)
        {
            return await _context.EventSubscriptions
                .Where(e => e.UserId == userId && eventIds.Contains(e.EventId))
                .ToListAsync();
        }
    }
}
