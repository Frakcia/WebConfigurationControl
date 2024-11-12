using Microsoft.EntityFrameworkCore;
using WebConfigurationControl.Domain.Entities;
using WebConfigurationControl.Infrastructure.Contexts;
using WebConfigurationControl.Infrastructure.Contracts;

namespace WebConfigurationControl.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : Entity
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context
                .Set<T>()
                .ToListAsync();
        }

        public virtual async Task<T?> GetById(Guid id)
        {
            return await _context
                .Set<T>()
                .FirstOrDefaultAsync(e=> e.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetByIds(IEnumerable<Guid> ids)
        {
            return await _context
                .Set<T>()
                .Where(e=> ids.Contains(e.Id))
                .ToListAsync();
        }

        public virtual async Task Add(T entity)
        {
            await _context
                .Set<T>()
                .AddAsync(entity);
        }

        public virtual async Task AddRange(IEnumerable<T> entities)
        {
            await _context
                .Set<T>()
                .AddRangeAsync(entities);
        }

        public virtual void Update(T entity)
        {
            _context
                .Set<T>()
                .Update(entity);
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
