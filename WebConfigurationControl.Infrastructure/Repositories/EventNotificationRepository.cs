using WebConfigurationControl.Domain.Entities;
using WebConfigurationControl.Infrastructure.Contexts;
using WebConfigurationControl.Infrastructure.Contracts;

namespace WebConfigurationControl.Infrastructure.Repositories
{
    public class EventNotificationRepository : BaseRepository<EventNotification>, IEventNotificationRepository
    {
        public EventNotificationRepository(AppDbContext context) 
            : base(context)
        {
        }
    }
}
