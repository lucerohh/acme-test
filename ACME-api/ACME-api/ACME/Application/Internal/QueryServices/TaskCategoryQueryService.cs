using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.ACME.Domain.Model.Queries.TaskCategories;
using ACME_api.ACME.Domain.Repositories;
using ACME_api.ACME.Domain.Services.Auth;
using ACME_api.ACME.Domain.Services.TaskCategories;

namespace ACME_api.ACME.Application.Internal.QueryServices
{
    public class TaskCategoryQueryService : ITaskCategoryQueryService
    {
        private readonly ITaskCategoryRepository _taskCategoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public TaskCategoryQueryService(
            ITaskCategoryRepository taskCategoryRepository,
            ICurrentUserService currentUserService)
        {
            _taskCategoryRepository = taskCategoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<TaskCategory>> Handle(GetAllTaskCategoriesByUserQuery query)
        {
            var userId = _currentUserService.GetUserId();

            var categories = await _taskCategoryRepository.FindAllByUserIdAsync(userId);

            return categories;
        }
    }
}
