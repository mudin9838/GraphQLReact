using AuthPlus.Identity.Extensions;

namespace GraphQLReact.UI.Server.Extensions;

public static class AuthorizationPolicies
{
    public static void AddAuthorizationPolicy(this IServiceCollection services)
    {
        services.AddAuthorizationPolicies();
        services.AddAuthorization(options =>
        {

            // Add new global policy, possibly using your custom requirement
            options.AddPolicy("GlobalPolicy", policy =>
                policy.Requirements.Add(new CustomRequirement("Admin")));
        });
    }
}