using Microsoft.AspNetCore.Authorization;

namespace GraphQLReact.UI.Server.Extensions;

public class CustomRequirement : IAuthorizationRequirement
{
    public string RequiredRole { get; }

    public CustomRequirement(string requiredRole)
    {
        RequiredRole = requiredRole;
    }
}