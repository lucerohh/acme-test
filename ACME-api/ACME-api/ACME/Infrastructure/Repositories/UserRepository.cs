using ACME_api.ACME.Domain.Model.Aggregates;
using ACME_api.ACME.Domain.Repositories;
using ACME_api.Shared.Infrastructure.Persistence.EFC.Configuration;
using ACME_api.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ACME_api.ACME.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        public async Task<User?> FindByEmailAsync(string email)
        {
            return await Context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
