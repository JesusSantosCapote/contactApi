using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Presentation.Authorization;

internal class RoleCountryAuthorizationHandler : AuthorizationHandler<RoleCountryAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleCountryAuthorizationRequirement requirement)
    {
        if (context.User.HasClaim(Constants.RoleClaimType, requirement.Role)
            && context.User.HasClaim(ClaimTypes.Country, requirement.Country))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}