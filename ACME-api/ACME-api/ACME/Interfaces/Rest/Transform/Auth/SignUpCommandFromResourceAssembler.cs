using ACME_api.ACME.Domain.Model.Commands.User;
using ACME_api.ACME.Interfaces.Rest.Resources.Auth;

namespace ACME_api.ACME.Interfaces.Rest.Transform.Auth
{
    public class SignUpCommandFromResourceAssembler
    {
        public static SignUpCommand ToCommandFromResource(SignUpRequestResource resource)
        {
            return new SignUpCommand(resource.FirstName, resource.LastName, resource.Email, resource.Password);
        }
    }
}
