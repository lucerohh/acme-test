using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.Shared.Domain.Aggregates;

namespace ACME_api.ACME.Domain.Model.Aggregates
{
    public class User : BaseEtity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; } 
        public string Email { get; private set; }
        public string Password { get; private set; }
        public ICollection<Entities.TaskItem> Tasks { get; set; } = new List<Entities.TaskItem>();

        public User(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }
    }
}

