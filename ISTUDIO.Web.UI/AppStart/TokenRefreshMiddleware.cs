//using ISTUDIO.Application.Common.Interfaces;
//using ISTUDIO.Contracts.Features.Authentication.JWTTokens;
//using ISTUDIO.Domain.Models;
//using MediatR;
//using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

//namespace ISTUDIO.Web.UI.AppStart;

//public class TokenRefreshMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly IJwtUtils _jwtUtils;
//    private readonly ISender _mediatr;
//    public TokenRefreshMiddleware(RequestDelegate next, ISender mediatr)
//    {
//        _next = next;
//        _mediatr = mediatr;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        var _sessionStorage = context.RequestServices.GetRequiredService<ProtectedSessionStorage>();

//        var userSessionStorageResult = await _sessionStorage.GetAsync<UserSessions>("UserSession");
//        var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

//        if (await _jwtUtils.IsTokenExpired(userSession.AccessToken))
//        {

//            var token = new TokenVM { AccessToken = userSession.AccessToken, RefreshToken = userSession.AccessToken };
//            // Отправьте запрос на обновление токена с использованием MediatR
//            var result = await _mediatr.Send(new UIRefreshTokenCommand { Token = token });

//            if (!result.Status)
//            {
//                // Если обновление токена не удалось, перенаправьте пользователя на страницу входа
//                context.Response.Redirect("/login");
//                return;
//            }
//        }
//        // Продолжите выполнение запроса
//        await _next(context);
//    }
//}
//}
