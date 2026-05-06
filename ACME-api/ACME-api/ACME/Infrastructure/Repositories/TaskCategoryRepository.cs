using ACME_api.ACME.Domain.Model.Aggregates;
using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.ACME.Domain.Repositories;
using ACME_api.Shared.Infrastructure.Persistence.EFC.Configuration;
using ACME_api.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ACME_api.ACME.Infrastructure.Repositories
{
    public class TaskCategoryRepository(AppDbContext context) : BaseRepository<TaskCategory>(context), ITaskCategoryRepository
    {
        public async Task<bool> ExistsByNameAndUserIdAsync(string name, int userId)
        {
            return await Context.Set<TaskCategory>()
                .AnyAsync(c => c.Name.ToLower() == name.ToLower() && c.UserId == userId);
        }
        public async Task<IEnumerable<TaskCategory>> FindAllByUserIdAsync(int userId)
        {
            return await Context.Set<TaskCategory>()
                .Where(tc => tc.UserId == userId)
                .ToListAsync();
        }
    }
}
