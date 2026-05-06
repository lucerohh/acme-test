namespace ACME_api.ACME.Interfaces.Rest.Resources.User
{
    public record UserInformationResponseResource(
        string FirstName,
        string LastName,
        string Email
     );
}
