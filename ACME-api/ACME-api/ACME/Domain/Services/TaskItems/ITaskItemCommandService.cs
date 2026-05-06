using ACME_api.ACME.Domain.Model.Commands.TaskItems;
using ACME_api.ACME.Domain.Model.Entities;

namespace ACME_api.ACME.Domain.Services.TaskItems
{
    public interface ITaskItemCommandService
    {
        public Task<TaskItem> Handle(CreateTaskItemCommand command);
        public Task<TaskItem> Handle(UpdateTaskItemCommand command);
        public Task Handle(DeleteTaskItemCommand command);
    }
}
