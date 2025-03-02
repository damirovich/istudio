using ISTUDIO.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ISTUDIO.Web.Api.AppStart;

public class AuthorizePermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly string _permission;

    public AuthorizePermissionAttribute(PermissionEnum permission)
    {
        _permission = permission.ToString(); // Enum → String
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult(); // 401
            return;
        }

        var userPermissions = user.Claims.Where(c => c.Type == "permission").Select(c => c.Value).ToList();

        if (!userPermissions.Contains(_permission))
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden; 
            context.Result = new JsonResult(new { error = "🚫 Доступ запрещен!" });
            return;
        }
    }
}
