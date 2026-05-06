using ACME_api.ACME.Domain.Model.Commands.User;
using ACME_api.ACME.Interfaces.Rest.Resources.Auth;

namespace ACME_api.ACME.Domain.Services.Auth
{
    public interface IAuthCommandService
    {
        public Task<SignInResponseResource> SignIn(SignInCommand command);
        public Task<SignUpResponseResource> SignUp(SignUpCommand command);
    }
}
