using ACME_api.ACME.Interfaces.Rest.Resources.Task;
using ACME_api.ACME.Domain.Model.Entities;

namespace ACME_api.ACME.Interfaces.Rest.Transform.Task
{
    public class TaskItemResourceFromEntityAssembler
    {
        public static TaskItemResource ToResourceFromEntity(TaskItem entity)
        {
            return new TaskItemResource(entity.Id, entity.Title, entity.Description, entity.Status.ToString(),
                entity.DueDate, entity.TaskCategoryId, entity.TaskCategory?.Name);
        }
    }
}
