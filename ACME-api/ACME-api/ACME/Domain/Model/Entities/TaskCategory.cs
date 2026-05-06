using ACME_api.ACME.Domain.Model.Aggregates;
using ACME_api.Shared.Domain.Aggregates;

namespace ACME_api.ACME.Domain.Model.Entities
{
    public class TaskCategory : BaseEtity
    {
        public string Name { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public TaskCategory() { }

        public TaskCategory(string name, int userId) {  
           Name = name; 
           UserId = userId; 
        }

    }
}
