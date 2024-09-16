using System.Security.Claims;
using AuthPlus.Identity.Extensions;
using GraphQLReact.BLL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GraphQLReact.BLL.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId()
    {
        return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public bool IsAdmin()
    {
        return _httpContextAccessor.HttpContext.User.IsInRole(RoleConstants.AdminRole);
    }
}
