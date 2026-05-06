using ACME_api.ACME.Domain.Model.Commands.TaskItems;
using ACME_api.ACME.Interfaces.Rest.Resources.TaskItems;

namespace ACME_api.ACME.Interfaces.Rest.Transform.TaskItems
{
    public class CreateTaskItemCommandFromResourceAssembler
    {
        public static CreateTaskItemCommand ToCommandFromResource(CreateTaskItemResource resource)
        {
            return new CreateTaskItemCommand(
                resource.Title,
                resource.Description,
                resource.Status,
                resource.DueDate,
                resource.TaskCategoryId
            );
        }
    }
}
