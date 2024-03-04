using System.Security.Claims;

namespace Presentation.Authentication;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserName
    {
        get
        {
            if (_httpContextAccessor.HttpContext is null)
                throw new ApplicationException("User context is unavailable");

            return _httpContextAccessor
                .HttpContext
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}