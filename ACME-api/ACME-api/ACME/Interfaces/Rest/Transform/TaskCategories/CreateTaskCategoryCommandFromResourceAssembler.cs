using ACME_api.ACME.Domain.Model.Commands.TaskCategories;
using ACME_api.ACME.Interfaces.Rest.Resources.TaskCategories;

namespace ACME_api.ACME.Interfaces.Rest.Transform.TaskCategories
{
    public static class CreateTaskCategoryCommandFromResourceAssembler
    {
        public static CreateTaskCategoryCommand ToCommandFromResource(CreateTaskCategoryResource resource)
        {
            return new CreateTaskCategoryCommand(resource.Name);
        }
    }
}
