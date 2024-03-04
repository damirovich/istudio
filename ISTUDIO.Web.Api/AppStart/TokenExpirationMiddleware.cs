using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace ISTUDIO.Web.Api.AppStart;

public class TokenExpirationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenExpirationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Проверяем, является ли текущий запрос защищенным
        var isProtectedEndpoint = context.GetEndpoint()?.Metadata.GetMetadata<AuthorizeAttribute>() != null;

        if (isProtectedEndpoint)
        {
            var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(accessToken) && IsTokenExpired(accessToken))
            {
                // Если токен истек, возвращаем 401 Unauthorized
                context.Response.StatusCode = 401;
                return;
            }
        }

        // Проходим далее по конвейеру запроса
        await _next(context);
    }

    public bool IsTokenExpired(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        if (handler.CanReadToken(accessToken))
        {
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

            return jsonToken?.ValidTo != null && jsonToken.ValidTo < DateTime.UtcNow;
        }

        // Если токен не валиден, возможно, он отсутствует или имеет неверный формат
        return true;
    }
}
