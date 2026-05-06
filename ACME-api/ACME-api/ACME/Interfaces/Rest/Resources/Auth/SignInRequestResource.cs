using System.ComponentModel.DataAnnotations;

namespace ACME_api.ACME.Interfaces.Rest.Resources.Auth
{
    public record SignInRequestResource
    (
        string Email,
        string Password
    );
}
