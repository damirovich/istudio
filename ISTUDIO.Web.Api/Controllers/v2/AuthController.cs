using ISTUDIO.Application.Features.Authentication.Commands.AuthUsers;
using ISTUDIO.Application.Features.Authentication.DTOs;
using ISTUDIO.Contracts.Features.Authentication.Authorizations;

namespace ISTUDIO.Web.Api.Controllers.v2;


/// <summary>
/// Контроллер для аутентификации и авторизации пользователей
/// </summary>
[ApiVersion("2.0")]
public class AuthController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IAppUserService _userService;
    private readonly IIdentityService _identityService;

    /// <summary>
    /// Конструктор контроллера аутентификации
    /// </summary>
    /// <param name="mapper">Сервис маппинга объектов</param>
    /// <param name="userService">Сервис работы с пользователями</param>
    /// <param name="identityService">Сервис управления идентификацией</param>
    public AuthController(IMapper mapper, IAppUserService userService, IIdentityService identityService)
    {
        _mapper = mapper;
        _userService = userService;
        _identityService = identityService;
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
    [ProducesResponseType(typeof(CsmActionResult<AuthResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<CsmActionResult> Login([FromBody] LoginVM login)
    {
        var command = _mapper.Map<AuthUserCommand>(login);
        return await HandleCommand(command);
    }
}
