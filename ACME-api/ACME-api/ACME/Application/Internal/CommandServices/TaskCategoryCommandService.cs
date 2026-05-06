using ACME_api.ACME.Domain.Model.Commands.TaskCategories;
using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.ACME.Domain.Repositories;
using ACME_api.ACME.Domain.Services.Auth;
using ACME_api.ACME.Domain.Services.TaskCategories;
using ACME_api.Shared.Domain.Repositories;

namespace ACME_api.ACME.Application.Internal.CommandServices
{
    public class TaskCategoryCommandService : ITaskCategoryCommandService
    {
        private readonly ITaskCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public TaskCategoryCommandService(
            ITaskCategoryRepository repository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<TaskCategory> Handle(CreateTaskCategoryCommand command)
        {
            var userId = _currentUser.GetUserId();

            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ApplicationException("Category name is required");

            var exists = await _repository.ExistsByNameAndUserIdAsync(command.Name, userId);

            if (exists)
                throw new ApplicationException("Category already exists");

            var category = new TaskCategory(command.Name, userId);

            await _repository.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            return category;
        }

        public async Task Handle(DeleteTaskCategoryCommand command)
        {
            var userId = _currentUser.GetUserId();

            var category = await _repository.FindByIdAsync(command.Id);

            if (category == null)
                throw new KeyNotFoundException("Task not found");

            if (category.UserId != userId)
                throw new UnauthorizedAccessException();

            _repository.Remove(category);

            await _unitOfWork.CompleteAsync();
        }
    }
}
