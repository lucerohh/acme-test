namespace ACME_api.ACME.Interfaces.Rest.Resources.Task
{
    public record TaskItemResource(
        int Id,
        string Title,
        string? Description,
        string Status,
        DateTime? DueDate,
        int? TaskCategoryId,
        string? TaskCategoryName
        );
}
