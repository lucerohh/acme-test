using ACME_api.ACME.Domain.Model.ValueObjects;

namespace ACME_api.ACME.Interfaces.Rest.Resources.TaskItems
{
    public record UpdateTaskItemResource(
    string Title,
    string? Description,
    TaskItemStatus Status,
    DateTime? DueDate,
    int? TaskCategoryId
);
}
