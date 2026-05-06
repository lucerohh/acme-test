using ACME_api.ACME.Domain.Model.ValueObjects;

namespace ACME_api.ACME.Domain.Model.Commands.TaskItems
{
    public record CreateTaskItemCommand(
        string Title,
        string? Description,
        TaskItemStatus Status,
        DateTime? DueDate,
        int? TaskCategoryId
    );
}
