using WebConfigurationControl.Domain.Entities;

namespace WebConfigurationControl.Infrastructure.Contracts
{
    public  interface IBaseRepository<T>
        where T : Entity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetByIds(IEnumerable<Guid> ids);

        Task Add(T entity);

        Task AddRange(IEnumerable<T> entities);

        void Update(T entity);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
