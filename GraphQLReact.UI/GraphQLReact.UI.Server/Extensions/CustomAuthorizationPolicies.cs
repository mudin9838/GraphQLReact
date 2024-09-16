using AuthPlus.Identity.Extensions;

namespace GraphQLReact.UI.Server.Extensions;

public static class CustomAuthorizationPolicies
{
    public static void AddCustomAuthorizationPolicies(this IServiceCollection services)
    {
        // Ensure the base policies are added first
        services.AddAuthorizationPolicies();

        //services.AddAuthorization(options =>
        //{
        //    // Add global policy that allows both Admin and User roles
        //    options.AddPolicy("GlobalPolicy", policy =>
        //        policy.RequireRole(RoleConstants.AdminRole, RoleConstants.UserRole, "ManagerRole"));
        //});

    }
}