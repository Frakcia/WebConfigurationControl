using Microsoft.EntityFrameworkCore;
using WebConfigurationControl.Domain.Entities;
using WebConfigurationControl.Infrastructure.Contexts;
using WebConfigurationControl.Infrastructure.Contracts;

namespace WebConfigurationControl.Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<Event?> GetByType(Domain.Enums.EventType type)
        {
            return await _context.Events
                .FirstOrDefaultAsync(e => e.Type == type && !e.IsDisabled);
        }
    }
}
