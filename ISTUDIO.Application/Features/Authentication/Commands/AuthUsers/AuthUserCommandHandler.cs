using ISTUDIO.Application.Features.Authentication.DTOs;
using ISTUDIO.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace ISTUDIO.Application.Features.Authentication.Commands.AuthUsers;
using ResModel = AuthResponseDTO;
public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, ResModel>
{
    private readonly IIdentityService _identityService;
    private readonly IAppUserService _userService;
    private readonly IJwtUtils _jwtUtils;
    private readonly IConfiguration _configuration;

    public AuthUserCommandHandler(IIdentityService identityService, IAppUserService userService, IJwtUtils jwtUtils, IConfiguration configuration)
    {
        _identityService = identityService;
        _userService = userService;
        _jwtUtils = jwtUtils;
        _configuration = configuration;
    }

    public async Task<ResModel> Handle(AuthUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _identityService.AuthenticateAsync(request.UserName!, request.Password!);

            if (!result)
                throw new BadRequestException("Неверное имя пользователя и пароль");

            var userDetails = await _userService.GetUserDetailsByUserNameAsync(request.UserName!);

            string token = await _jwtUtils.GenerateToken(userDetails.UserId, userDetails.FullName, userDetails.UserName, userDetails.Roles);

            var refreshToken = await _jwtUtils.GenerateRefreshToken();
            var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetSection("JwtOptions:RefreshTokenValidityInDays").Get<int>());


            var upUserRes = _identityService.UpdateTokenUsers(userDetails.UserId, refreshToken, refreshTokenExpiryTime);
            if (!upUserRes.Result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, upUserRes.Result.Errors);

                throw new BadRequestException($"Не удается обновить в БД Refresh Token {request.UserName}.{Environment.NewLine}{errors}");
            }

            return new ResModel()
            {
                UserSession = new UserSessions
                {
                    UserId = userDetails.UserId,
                    Name = userDetails.FullName,
                    Email = userDetails.Email,
                    Roles = userDetails.Roles,
                    AccessToken = token,
                    RefreshToken = refreshToken
                }
            };
        }
        catch (BadRequestException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BadRequestException($"Ошибка {ex.Message}", ex.InnerException);
        }
    }
}
