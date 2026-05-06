using System.ComponentModel.DataAnnotations;

namespace ACME_api.ACME.Interfaces.Rest.Resources.Auth
{
    public record SignUpRequestResource (
    string FirstName,
    string LastName,
    string Email,
    string Password
    );
}
