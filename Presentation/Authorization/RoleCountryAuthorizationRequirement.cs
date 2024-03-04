using Microsoft.AspNetCore.Authorization;

namespace Presentation.Authorization;

internal class RoleCountryAuthorizationRequirement : IAuthorizationRequirement
{
    public string Role { get; private set; }
    public string Country { get; private set; }

    public RoleCountryAuthorizationRequirement(string role, string country)
    {
         Role = role;
        Country = country;
    }
}
