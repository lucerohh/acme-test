using ACME_api.Shared.Domain.Repositories;
using ACME_api.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace ACME_api.Shared.Infrastructure.Persistence.EFC.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
