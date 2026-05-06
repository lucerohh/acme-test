namespace ACME_api.ACME.Interfaces.Rest.Resources.Auth
{
    public record SignUpResponseResource(
        string AccessToken,
        string FirstName,
        string LastName
     );
}
