using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.Shared.Domain.Repositories;

namespace ACME_api.ACME.Domain.Repositories
{
    public interface ITaskCategoryRepository : IBaseRepository<TaskCategory>
    {
        Task<bool> ExistsByNameAndUserIdAsync(string name, int userId);
        Task<IEnumerable<TaskCategory>> FindAllByUserIdAsync(int userId);
    }
}
