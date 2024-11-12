using WebConfigurationControl.Domain.Entities;
using WebConfigurationControl.Infrastructure.Contexts;
using WebConfigurationControl.Infrastructure.Contracts;

namespace WebConfigurationControl.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) 
            : base(context)
        {
        }
    }
}
