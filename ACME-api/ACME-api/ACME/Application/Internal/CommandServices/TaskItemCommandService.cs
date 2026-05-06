using ACME_api.ACME.Domain.Model.Commands.TaskItems;
using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.ACME.Domain.Repositories;
using ACME_api.ACME.Domain.Services.Auth;
using ACME_api.ACME.Domain.Services.TaskItems;
using ACME_api.Shared.Domain.Repositories;

namespace ACME_api.ACME.Application.Internal.CommandServices
{
    public class TaskItemCommandService : ITaskItemCommandService
    {
        private readonly ITaskItemRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public TaskItemCommandService(
            ITaskItemRepository taskRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUser)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }


        public async Task<TaskItem> Handle(CreateTaskItemCommand command)
        {
            var userId = _currentUser.GetUserId();

            if (string.IsNullOrWhiteSpace(command.Title))
                throw new ApplicationException("Title is required");

            var task = new TaskItem(
                command.Title,
                command.Description,
                command.Status,
                command.DueDate,
                userId,
                command.TaskCategoryId
            );

            await _taskRepository.AddAsync(task);
            await _unitOfWork.CompleteAsync();

            return task;
        }

        public async Task<TaskItem> Handle(UpdateTaskItemCommand command)
        {
            var userId = _currentUser.GetUserId();

            var task = await _taskRepository.FindByIdAsync(command.Id);

            if (task == null)
                throw new KeyNotFoundException("Task not found");

            if (task.UserId != userId)
                throw new UnauthorizedAccessException();

            task.Update(
                command.Title,
                command.Description,
                command.Status,
                command.DueDate,
                command.TaskCategoryId
            );

            await _unitOfWork.CompleteAsync();

            return task;
        }

        public async Task Handle(DeleteTaskItemCommand command)
        {
            var userId = _currentUser.GetUserId();

            var task = await _taskRepository.FindByIdAsync(command.Id);

            if (task == null)
                throw new KeyNotFoundException("Task not found");

            if (task.UserId != userId)
                throw new UnauthorizedAccessException();

            _taskRepository.Remove(task);

            await _unitOfWork.CompleteAsync();
        }
    }
}
