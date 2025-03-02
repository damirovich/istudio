using ISTUDIO.Application.Features.Authentication.Commands.AuthUsers;
using ISTUDIO.Application.Features.Authentication.Commands.RefreshJWT;
using ISTUDIO.Application.Features.SmsNikita.Commands.SendSms;
using ISTUDIO.Application.Features.UserManagement.Commands.RegistrUserMobile;
using ISTUDIO.Contracts.Features.Authentication.Authorizations;
using ISTUDIO.Contracts.Features.Authentication.JWTTokens;
using ISTUDIO.Contracts.Features.UserManagement;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

/// <summary>
/// Контроллер для аутентификации и авторизации пользователей
/// </summary>
[ApiVersion("2.0")]
public class AuthController : BaseController2
{
    private readonly IMapper _mapper;
    private readonly IAppUserService _userService;
    private readonly IIdentityService _identityService;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Конструктор контроллера аутентификации
    /// </summary>
    /// <param name="mapper">Сервис маппинга объектов</param>
    /// <param name="userService">Сервис работы с пользователями</param>
    /// <param name="identityService">Сервис управления идентификацией</param>
    public AuthController(IMapper mapper, IAppUserService userService, IIdentityService identityService, ILogger<AuthController> logger) : base(logger)
    {
        _mapper = mapper;
        _userService = userService;
        _identityService = identityService;
        _logger = logger;
    }

    /// <summary>
    /// Метод для аутентификации и авторизации
    /// </summary>
    /// <param name="login">Модель запроса с логином и паролем</param>
    /// <returns>Данные пользователя, токен доступа и refresh-токен</returns>
    /// <response code="200">Успешная авторизация</response>
    /// <response code="400">Ошибка валидации или некорректные данные</response>
    /// <response code="401">Неверные учетные данные</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<CsmActionResult> Login([FromBody] LoginVM login)
    {
        var command = _mapper.Map<AuthUserCommand>(login);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Метод для обновления токена
    /// </summary>
    /// <param name="token">Access token и Refresh token</param>
    /// <returns>Новый Access token и Refresh token</returns>
    /// <response code="200">Токен успешно обновлен</response>
    /// <response code="400">Ошибка валидации или некорректные данные</response>
    /// <response code="401">Неверные учетные данные</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<CsmActionResult> RefreshToken([FromBody] TokenVM token)
    {
        var command = _mapper.Map<RefreshJWTCommand>(token);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Метод для сброса Refresh token
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Результат операции</returns>
    [HttpPost("revoke")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<CsmActionResult> Revoke([FromForm] string userId)
    {
        var user = await _userService.GetUserDetailsByUserIdAsync(userId);

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = new DateTime(1900, 01, 01);
        await _identityService.UpdateTokenUsers(userId, user.RefreshToken, user.RefreshTokenExpiryTime);

        return new CsmActionResult(0, "Success!");
    }

    /// <summary>
    /// Отправка ОТП-кода на номер телефона
    /// </summary>
    /// <param name="phonesNumber">Номер телефона</param>
    /// <returns>Результат отправки</returns>
    [Authorize]
    [HttpPost("send-otp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<CsmActionResult> SendOTP([FromForm] string phonesNumber)
    {
        var command = new SendSmsCommand { PhonesNumber = phonesNumber };
        return await HandleCommand(command);
    }

    /// <summary>
    /// Регистрация клиента по номеру телефона в мобильном приложении
    /// </summary>
    /// <param name="user">Данные пользователя</param>
    /// <returns>Результат регистрации</returns>
    [Authorize]
    [HttpPost("register-mobile-user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<CsmActionResult> RegisterMobileUser([FromBody] CreateUserMobleVM user)
    {
        var command = new RegistrUsersMobileCommand
        {
            PhoneNumber = user.PhoneNumber,
            OTPCode = user.CodeOTP,
            HasAgreedToPrivacyPolicy = user.HasAgreedToPrivacyPolicy,
            ConsentToTheUserAgreement = user.ConsentToTheUserAgreement,
            Roles = new List<string> { "MobileUser" }
        };

        return await HandleCommand(command);
    }

}
