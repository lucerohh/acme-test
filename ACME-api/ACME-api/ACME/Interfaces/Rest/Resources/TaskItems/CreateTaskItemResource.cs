
using ACME_api.ACME.Domain.Model.ValueObjects;

namespace ACME_api.ACME.Interfaces.Rest.Resources.TaskItems
{
    public record CreateTaskItemResource(
        string Title,
        string? Description,
        TaskItemStatus Status = TaskItemStatus.Pending,
        DateTime? DueDate = null,
        int? TaskCategoryId = null
    );
}
