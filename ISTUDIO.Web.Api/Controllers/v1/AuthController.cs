using ISTUDIO.Application.Features.Authentication.Commands.AuthUsers;
using ISTUDIO.Application.Features.Authentication.Commands.RefreshJWT;
using ISTUDIO.Application.Features.SmsNikita.Commands.SendSms;
using ISTUDIO.Application.Features.UserManagement.Commands.RegistrUserMobile;
using ISTUDIO.Contracts.Features.Authentication.Authorizations;
using ISTUDIO.Contracts.Features.Authentication.JWTTokens;
using ISTUDIO.Contracts.Features.UserManagement;
using System.Net;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0", Deprecated = true)]
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
    /// Метод для аудентификации и авторизации
    /// </summary>
    /// <param name="login">UserName and Password</param>
    /// <returns>User data, Access token and Refresh token</returns>
    [HttpPost("Login")]
    public async Task<ICsmActionResult> Login([FromBody] LoginVM login)
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
    [HttpPost("revoke")]
    public async Task<ICsmActionResult> Revoke([FromForm] string userId)
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
    /// <summary>
    /// Отправка ОТП кода в номер телефона
    /// </summary>
    /// <param name="phonesNumber"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ICsmActionResult> SendOTP([FromForm]string phonesNumber)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new SendSmsCommand { PhonesNumber = phonesNumber }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));

        }
    }
    /// <summary>
    /// Регистрация клиента по номеру в мобильном приложении
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ICsmActionResult> RegistrMobileUser([FromBody] CreateUserMobleVM user)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new RegistrUsersMobileCommand
            {
                PhoneNumber = user.PhoneNumber,
                OTPCode = user.CodeOTP,
                HasAgreedToPrivacyPolicy = user.HasAgreedToPrivacyPolicy,
                ConsentToTheUserAgreement = user.ConsentToTheUserAgreement,
                Roles = new List<string> { "MobileUser" }
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
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
