using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.Shared.Domain.Repositories;

namespace ACME_api.ACME.Domain.Repositories
{
    public interface ITaskItemRepository : IBaseRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> FindByUserIdAsync(int userId);
    }
}
