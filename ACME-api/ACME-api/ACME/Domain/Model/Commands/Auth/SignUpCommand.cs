using System.ComponentModel.DataAnnotations;

namespace ACME_api.ACME.Domain.Model.Commands.User
{
    public record SignUpCommand(
    [Required]
    [MinLength(2)]
    string FirstName,

    [Required]
    [MinLength(2)]
    string LastName,

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [MinLength(8)]
    string Password
    );
}
