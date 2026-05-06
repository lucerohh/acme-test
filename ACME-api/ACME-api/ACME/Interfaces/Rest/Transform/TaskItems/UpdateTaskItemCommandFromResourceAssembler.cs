using ACME_api.ACME.Domain.Model.Commands.TaskItems;
using ACME_api.ACME.Interfaces.Rest.Resources.TaskItems;

namespace ACME_api.ACME.Interfaces.Rest.Transform.TaskItems
{
    public class UpdateTaskItemCommandFromResourceAssembler
    {
        public static UpdateTaskItemCommand ToCommandFromResource(int id, UpdateTaskItemResource resource)
        {
            return new UpdateTaskItemCommand(
                id,
                resource.Title,
                resource.Description,
                resource.Status,
                resource.DueDate,
                resource.TaskCategoryId
            );
        }
    }
}
