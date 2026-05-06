using ACME_api.ACME.Domain.Model.Aggregates;

namespace ACME_api.ACME.Domain.Services.JWT
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
