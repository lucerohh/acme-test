using ACME_api.ACME.Domain.Model.ValueObjects;

namespace ACME_api.ACME.Domain.Model.Commands.TaskItems
{
    public record UpdateTaskItemCommand(
    int Id,
    string Title,
    string? Description,
    TaskItemStatus Status,
    DateTime? DueDate,
    int? TaskCategoryId
);
}
