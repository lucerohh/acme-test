using System.ComponentModel.DataAnnotations;

namespace ACME_api.ACME.Domain.Model.Commands.User
{
    public record SignInCommand
    (
        [Required]
        [EmailAddress]
        string Email,
        [Required]
        [MinLength(8)]
        string Password
    );
}
