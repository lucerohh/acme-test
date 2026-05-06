using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.ACME.Domain.Model.Queries.TaskCategories;

namespace ACME_api.ACME.Domain.Services.TaskCategories
{
    public interface ITaskCategoryQueryService
    {
        public Task<IEnumerable<TaskCategory>> Handle(GetAllTaskCategoriesByUserQuery query);
    }
}
