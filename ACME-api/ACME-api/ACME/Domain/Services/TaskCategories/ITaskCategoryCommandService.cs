using ACME_api.ACME.Domain.Model.Commands.TaskCategories;
using ACME_api.ACME.Domain.Model.Entities;

namespace ACME_api.ACME.Domain.Services.TaskCategories
{
    public interface ITaskCategoryCommandService
    {
        public Task<TaskCategory> Handle(CreateTaskCategoryCommand command);
        public Task Handle(DeleteTaskCategoryCommand command);
    }
}
