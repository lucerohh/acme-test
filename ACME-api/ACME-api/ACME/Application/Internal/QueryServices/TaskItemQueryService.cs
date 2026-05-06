using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.ACME.Domain.Model.Queries.TaskItems;
using ACME_api.ACME.Domain.Repositories;
using ACME_api.ACME.Domain.Services.Auth;
using ACME_api.ACME.Domain.Services.TaskItems;
using ACME_api.Shared.Domain.Repositories;
using System.Data;

namespace ACME_api.ACME.Application.Internal.QueryServices
{
    public class TaskItemQueryService : ITaskItemQueryService
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly ICurrentUserService _currentUserService;

        public TaskItemQueryService(ITaskItemRepository taskItemRepository, ICurrentUserService currentUserService)
        {
            _taskItemRepository = taskItemRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<TaskItem>> Handle(GetTaskItemsByUserQuery query)
        {
            var userId = _currentUserService.GetUserId();

            var taskItems = await _taskItemRepository.FindByUserIdAsync(userId);

            return taskItems;
        }

        public async Task<TaskItem> Handle(GetTaskItemByIdQuery query)
        {
            var task = await _taskItemRepository.FindByIdAsync(query.Id);

            if (task == null)
                throw new KeyNotFoundException("Task not found");

            return task;
        }
    }
}
