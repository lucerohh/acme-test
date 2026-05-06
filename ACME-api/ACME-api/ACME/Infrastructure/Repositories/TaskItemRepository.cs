using ACME_api.ACME.Domain.Model.Aggregates;
using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.ACME.Domain.Repositories;
using ACME_api.Shared.Infrastructure.Persistence.EFC.Configuration;
using ACME_api.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ACME_api.ACME.Infrastructure.Repositories
{
    public class TaskItemRepository(AppDbContext context) : BaseRepository<TaskItem>(context), ITaskItemRepository
    {
        public async Task<IEnumerable<TaskItem>> FindByUserIdAsync(int userId)
        {
            return await Context.Set<TaskItem>()
                .Include(t => t.TaskCategory)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
        public new async Task<TaskItem?> FindByIdAsync(int id)
        {
            return await Context.Set<TaskItem>()
                .Include(t => t.TaskCategory) 
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
