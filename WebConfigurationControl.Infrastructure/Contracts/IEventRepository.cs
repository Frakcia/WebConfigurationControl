using WebConfigurationControl.Domain.Entities;

namespace WebConfigurationControl.Infrastructure.Contracts
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<Event?> GetByType(Domain.Enums.EventType type);
    }
}
