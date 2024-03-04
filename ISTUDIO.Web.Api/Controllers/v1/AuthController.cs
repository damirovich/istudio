using Asp.Versioning;
using AutoMapper;
using ISTUDIO.Application.Common.Interfaces;
using ISTUDIO.Application.Features.Authentication.Commands.AuthUsers;
using ISTUDIO.Application.Features.Authentication.Commands.CreateUsers;
using ISTUDIO.Application.Features.Authentication.Commands.RefreshJWT;
using ISTUDIO.Contracts.Features.Authentication.Authorizations;
using ISTUDIO.Contracts.Features.Authentication.JWTTokens;
using ISTUDIO.Contracts.Features.UserManagement;
using ISTUDIO.Web.Api.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0")]
public class AuthController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IAppUserService _userService;
    private readonly IIdentityService _identityService;

    public AuthController(IMapper mapper, IAppUserService userService, IIdentityService identityService)
    {
        _mapper = mapper;
        _userService = userService;
        _identityService = identityService;
    }

    /// <summary>
    /// Метод для создание нового пользователя
    /// </summary>
    /// <param name="user">User Data</param>
    /// <returns>UserId</returns>
    [HttpPost("Register")]
    public async Task<ICsmActionResult> CreateUser([FromBody] CreateUserVM user)
    {
        try
        {
            var command = _mapper.Map<CreateUserCommand>(user);

            return new CsmActionResult(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Метод для аудентификации
    /// </summary>
    /// <param name="login">UserName and Password</param>
    /// <returns>User data, Access token and Refresh token</returns>
    [HttpPost("Login")]
    public async Task<ICsmActionResult> Login([FromBody] AuthUserVM login)
    {
        try
        {
            var command = _mapper.Map<AuthUserCommand>(login);

            return new CsmActionResult(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }

    }
    /// <summary>
    /// Метод для обновление Token
    /// </summary>
    /// <param name="token">Access token and Refresh token</param>
    /// <returns>New Access token and Refresh token</returns>
    [HttpPost("refresh-token")]
    public async Task<ICsmActionResult> RefreshToken(TokenVM? token)
    {
        if (token is null)
            return BadRequest("Invalid client request");
        try
        {
            var command = _mapper.Map<RefreshJWTCommand>(token);
            return new CsmActionResult(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message, ex.InnerException ?? ex));
        }
    }
    /// <summary>
    /// Метод для сборса Refresh token
    /// </summary>
    /// <param name="userId">userId пользователя</param>
    /// <returns>Ok</returns>
    [HttpPost("revoke/{userId}")]
    public async Task<ICsmActionResult> Revoke(string userId)
    {
        try
        {
            var user = await _userService.GetUserDetailsByUserIdAsync(userId);
            if (user is null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = new DateTime(1900, 01, 01);
            await _identityService.UpdateTokenUsers(userId, user.RefreshToken, user.RefreshTokenExpiryTime);

            return new CsmActionResult(0, "Success!");
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message, ex.InnerException ?? ex));
        }
    }
    private ICsmActionResult BadRequest(string message)
    {
        return new CsmActionResult(new CsmReturnStatus
        {
            Status = (int)HttpStatusCode.BadRequest,
            Message = message
        });
    }
}
