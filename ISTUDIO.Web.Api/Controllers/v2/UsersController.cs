using ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;
using ISTUDIO.Application.Features.UserManagement.Commands.EditUserProfile;
using ISTUDIO.Application.Features.UserManagement.Commands.UpdatePassword;
using ISTUDIO.Application.Features.UserManagement.Queries;
using ISTUDIO.Contracts.Features.UserManagement;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]
public class UsersController : BaseController2
{
    private readonly IMapper _mapper;

    public UsersController(IMapper mapper, ILogger<UsersController> logger) : base(logger)
        => _mapper = mapper;

    /// <summary>
    /// Получение списка всех пользователей системы
    /// </summary>
    /// <returns>Список пользователей</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet]
    public async Task<ICsmActionResult> GetList()
        => await HandleQuery(new GetUserListQuery());

    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Данные пользователя</returns>
    /// <response code="200">Успешное получение данных пользователя</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet("{userId}")]
    public async Task<ICsmActionResult> GetById([FromRoute] string userId)
        => await HandleQuery(new GetUserByIdQuery { UserId = userId });

    /// <summary>
    /// Получение пользователя по номеру телефона
    /// </summary>
    /// <param name="phoneNumber">Номер телефона пользователя</param>
    /// <returns>Данные пользователя</returns>
    /// <response code="200">Успешное получение данных пользователя</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet("by-phone")]
    public async Task<ICsmActionResult> GetByPhoneNumber([FromQuery] string phoneNumber)
        => await HandleQuery(new GetUserByPhoneNumberQuery { PhoneNumber = phoneNumber });

    /// <summary>
    /// Создание нового пользователя
    /// </summary>
    /// <param name="user">Данные нового пользователя</param>
    /// <returns>Результат создания пользователя</returns>
    /// <response code="200">Пользователь успешно создан</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPost("register")]
    public async Task<ICsmActionResult> Create([FromBody] CreateUserVM user)
    {
        var command = _mapper.Map<CreateUserCommand>(user);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование профиля пользователя
    /// </summary>
    /// <param name="user">Данные для обновления профиля</param>
    /// <returns>Результат обновления профиля</returns>
    /// <response code="200">Профиль пользователя успешно обновлён</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut("edit-profile")]
    public async Task<ICsmActionResult> EditProfile([FromBody] EditUserVM user)
    {
        var command = _mapper.Map<EditUserProfileCommand>(user);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Изменение пароля пользователя
    /// </summary>
    /// <param name="user">Данные для изменения пароля</param>
    /// <returns>Результат изменения пароля</returns>
    /// <response code="200">Пароль успешно изменён</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut("update-password")]
    public async Task<ICsmActionResult> UpdatePassword([FromBody] UpdatePasswordVM user)
    {
        var command = _mapper.Map<UpdatePasswordCommand>(user);
        return await HandleCommand(command);
    }
}
