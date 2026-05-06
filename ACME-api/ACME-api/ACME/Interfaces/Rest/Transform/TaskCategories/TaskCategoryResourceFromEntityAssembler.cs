using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.ACME.Interfaces.Rest.Resources.TaskCategories;

namespace ACME_api.ACME.Interfaces.Rest.Transform.TaskCategories
{
    public class TaskCategoryResourceFromEntityAssembler
    {
        public static TaskCategoryResource ToResourceFromEntity(TaskCategory entity)
        {
            return new TaskCategoryResource(
                entity.Id,
                entity.Name
            );
        }
    }
}
