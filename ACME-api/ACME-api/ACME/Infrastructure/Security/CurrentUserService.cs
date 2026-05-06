using ACME_api.ACME.Domain.Services.Auth;
using System.Security.Claims;

namespace ACME_api.ACME.Infrastructure.Security
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity!.IsAuthenticated)
                throw new Exception("No authenticated user");

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new Exception("User ID not found in token");

            return int.Parse(userId);
        }
    }
}
