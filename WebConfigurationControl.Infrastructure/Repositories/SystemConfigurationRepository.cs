using Microsoft.EntityFrameworkCore;
using WebConfigurationControl.Common.Models.Ordering;
using WebConfigurationControl.Common.Models.Pagination;
using WebConfigurationControl.Domain.Entities;
using WebConfigurationControl.Infrastructure.Contexts;
using WebConfigurationControl.Infrastructure.Contracts;
using WebConfigurationControl.Common.Extensions;

namespace WebConfigurationControl.Infrastructure.Repositories
{
    public class SystemConfigurationRepository : BaseRepository<SystemConfiguration>, ISystemConfigurationRepository
    {
        public SystemConfigurationRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<SystemConfiguration>> GetAsync(Pagination pagination, IEnumerable<GlobalEntityOrdering> ordering)
        {
            return await _context.SystemConfigurations
                .Where(e => !e.IsDisabled)
                .Order(ordering)
                .Skip(pagination.Skip)
                .Take(pagination.PageCount)
                .ToListAsync();
        }

        public async Task<long> GetTotalCountAsync()
        {
            return await _context.SystemConfigurations
                .Where(e => !e.IsDisabled)
                .CountAsync();
        }

        public async Task<SystemConfiguration?> GetByNameAsync(string name, Guid userId)
        {
            return await _context.SystemConfigurations
                .FirstOrDefaultAsync(e => e.UserId == userId && e.Name == name);
        }
    }
}
