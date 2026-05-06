using ACME_api.ACME.Domain.Model.Commands.User;
using ACME_api.ACME.Interfaces.Rest.Resources.Auth;

namespace ACME_api.ACME.Interfaces.Rest.Transform.Auth
{
    public class SignInCommandFromResourceAssembler
    {
        public static SignInCommand ToCommandFromResource(SignInRequestResource resource)
        {
            return new SignInCommand(resource.Email, resource.Password);
        }
    }
}
