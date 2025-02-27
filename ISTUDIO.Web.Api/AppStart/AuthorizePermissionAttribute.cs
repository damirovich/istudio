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
        Console.WriteLine($"🔍 Авторизация проверяется для {_permission}");

        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            Console.WriteLine("❌ Пользователь не аутентифицирован!");
            context.Result = new UnauthorizedResult(); // 401
            return;
        }

        var userPermissions = user.Claims.Where(c => c.Type == "permission").Select(c => c.Value).ToList();
        Console.WriteLine($"🔍 У пользователя есть permissions: {string.Join(", ", userPermissions)}");

        if (!userPermissions.Contains(_permission))
        {
            Console.WriteLine($"❌ У пользователя НЕТ permission: {_permission}");
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden; // 👈 Явно устанавливаем код 403
            context.Result = new JsonResult(new { error = "🚫 Доступ запрещен!" }); // 👈 Отдаем JSON-ошибку
            return;
        }

        Console.WriteLine($"✅ У пользователя ЕСТЬ permission: {_permission}");
    }
}
