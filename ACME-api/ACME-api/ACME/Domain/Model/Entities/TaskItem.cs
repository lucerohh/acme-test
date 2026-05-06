using ACME_api.ACME.Domain.Model.Aggregates;
using ACME_api.ACME.Domain.Model.ValueObjects;
using ACME_api.Shared.Domain.Aggregates;

namespace ACME_api.ACME.Domain.Model.Entities
{
    public class TaskItem : BaseEtity
    {
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public TaskItemStatus Status { get; private set; }
        public DateTime? DueDate { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
        public int? TaskCategoryId { get; set; }
        public TaskCategory? TaskCategory { get; set; }
        public byte[]? RowVersion { get; set; }

        private TaskItem() { }

        public TaskItem(
            string title,
            string? description,
            TaskItemStatus status,
            DateTime? dueDate,
            int userId,
            int? taskCategoryId = null
            )
        {
            Title = title;
            Description = description;
            Status = status;
            DueDate = dueDate;
            UserId = userId;
            TaskCategoryId = taskCategoryId;
        }

        public void Update(
            string title,
            string? description,
            TaskItemStatus status,
            DateTime? dueDate,
            int? taskCategoryId)
        {
            Title = title;
            Description = description;
            Status = status;
            DueDate = dueDate;
            TaskCategoryId = taskCategoryId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
