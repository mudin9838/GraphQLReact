using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GraphQLReact.UI.Server.Extensions;

public class CustomRequirementHandler : AuthorizationHandler<CustomRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
    {
        // Check for the standard ClaimsTypes.Role
        if (context.User.HasClaim(ClaimTypes.Role, requirement.RequiredRole) ||
            context.User.HasClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", requirement.RequiredRole))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}