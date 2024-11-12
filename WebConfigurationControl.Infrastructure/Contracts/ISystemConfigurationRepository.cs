using WebConfigurationControl.Common.Models.Ordering;
using WebConfigurationControl.Common.Models.Pagination;
using WebConfigurationControl.Domain.Entities;

namespace WebConfigurationControl.Infrastructure.Contracts
{
    public interface ISystemConfigurationRepository : IBaseRepository<SystemConfiguration>
    {
        Task<IEnumerable<SystemConfiguration>> GetAsync(Pagination pagination, IEnumerable<GlobalEntityOrdering> ordering);
        Task<long> GetTotalCountAsync();
        Task<SystemConfiguration?> GetByNameAsync(string name, Guid userId);
    }
}
