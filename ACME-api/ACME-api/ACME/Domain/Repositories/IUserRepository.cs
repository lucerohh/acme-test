using ACME_api.ACME.Domain.Model.Aggregates;
using ACME_api.Shared.Domain.Repositories;

namespace ACME_api.ACME.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> FindByEmailAsync(string email);
    }
}
