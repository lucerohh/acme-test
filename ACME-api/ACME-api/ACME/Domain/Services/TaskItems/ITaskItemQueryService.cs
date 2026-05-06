using ACME_api.ACME.Domain.Model.Queries.TaskItems;
using ACME_api.ACME.Domain.Model.Entities;

namespace ACME_api.ACME.Domain.Services.TaskItems
{
    public interface ITaskItemQueryService
    {
        public Task<IEnumerable<TaskItem>> Handle(GetTaskItemsByUserQuery query);

        public Task<TaskItem> Handle(GetTaskItemByIdQuery query);
    }
}
