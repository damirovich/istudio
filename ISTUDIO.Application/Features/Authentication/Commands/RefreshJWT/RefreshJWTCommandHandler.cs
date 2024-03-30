using ISTUDIO.Application.Features.Authentication.DTOs;
using ISTUDIO.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace ISTUDIO.Application.Features.Authentication.Commands.RefreshJWT;
using ResModel = RefreshJWTResponseDTO;
public class RefreshJWTCommandHandler : IRequestHandler<RefreshJWTCommand, ResModel>
{
    private readonly IJwtUtils _jwtUtils;
    private readonly IAppUserService _userService;
    private readonly IIdentityService _identityService;
    private readonly IConfiguration _configuration;
    public RefreshJWTCommandHandler(IJwtUtils jwtUtils, IAppUserService userService, IIdentityService identityService, IConfiguration configuration)
    {
        _jwtUtils = jwtUtils;
        _userService = userService;
        _identityService = identityService;
        _configuration = configuration;
    }

    public async Task<ResModel> Handle(RefreshJWTCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //Валидация токена
            var principal = _jwtUtils.ValidateToken(request.AccessToken);
            if (principal is null)
                throw new BadRequestException("Недопустимый токен доступа или токен обновления");

            // Извлечение UserName из Claims
            var userName = principal?.Identity.Name;

            //Получение данных пользователя по имени пользователя
            var user = await _userService.GetUserDetailsByUserNameAsync(userName!);

            // Проверка валидности пользователя и срока действия токена
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new BadRequestException("Не правильный RefreshToken или время Refresh Token истек");
            }
            // Генерация новых токенов 
            var newAccessToken = await _jwtUtils.GenerateToken(user.UserId,  user.UserName, user.Roles);
            var newRefreshToken = await _jwtUtils.GenerateRefreshToken();

            //Обновление токена в базе 
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetSection("JwtOptions:RefreshTokenValidityInDays").Get<int>());

            var upUserRes = _identityService.UpdateTokenUsers(user.UserId, newRefreshToken, user.RefreshTokenExpiryTime);
            if (!upUserRes.Result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, upUserRes.Result.Errors);

                throw new Exception($"Не удалось обновить в БД Refresh Token {userName}.{Environment.NewLine}{errors}");
            }

            return new ResModel()
            {
                UserSession = new UserSessions
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Roles = user.Roles,
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                }
            };
        }
        catch (Exception ex)
        {
            throw new BadRequestException($"Ошибка при обновление токена {ex.Message}", ex.InnerException);
        }
    }
}
