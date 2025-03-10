using ISTUDIO.Application.Features.CashUsers.Commands.CreateCashUsers;
using ISTUDIO.Application.Features.CashUsers.Commands.DeleteCashUsers;
using ISTUDIO.Application.Features.CashUsers.Commands.EditCashUsers;
using ISTUDIO.Application.Features.CashUsers.Queries;
using ISTUDIO.Contracts.Features.UserCashback;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]
public class CashUsersController : BaseController2
{
    private readonly IMapper _mapper;

    public CashUsersController(IMapper mapper, ILogger<CashUsersController> logger) : base(logger)
        => _mapper = mapper;

    /// <summary>
    /// Получение списка кэш-пользователей с пагинацией
    /// </summary>
    /// <param name="page">Параметры пагинации</param>
    /// <returns>Список кэш-пользователей</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet]
    public async Task<ICsmActionResult> GetList([FromQuery] PaginatedListVM page)
        => await HandleQuery(new GetCashbackUsersQuery
        {
            Parameters = new PaginatedParameters
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            }
        });

    /// <summary>
    /// Получение кэш-пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор кэш-пользователя</param>
    /// <returns>Данные кэш-пользователя</returns>
    /// <response code="200">Успешное получение кэш-пользователя</response>
    /// <response code="404">Кэш-пользователь не найден</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet]
    public async Task<ICsmActionResult> GetById([FromQuery] string userId)
        => await HandleQuery(new GetUserCashbackByUserIdQuery { UserId = userId });

    /// <summary>
    /// Создание нового кэш-пользователя
    /// </summary>
    /// <param name="model">Данные нового кэш-пользователя</param>
    /// <returns>Результат операции создания</returns>
    /// <response code="200">Кэш-пользователь успешно создан</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPost]
    public async Task<ICsmActionResult> Create([FromBody] CreateCashUserVM model)
    {
        var command = _mapper.Map<CreateCashUserCommand>(model);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование кэш-пользователя
    /// </summary>
    /// <param name="id">Идентификатор кэш-пользователя</param>
    /// <param name="model">Данные для редактирования</param>
    /// <returns>Результат операции редактирования</returns>
    /// <response code="200">Кэш-пользователь успешно обновлён</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut]
    public async Task<ICsmActionResult> Edit([FromBody] EditCashUserVM model)
    {
        var command = _mapper.Map<EditCashUserCommand>(model);
    
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление кэш-пользователя
    /// </summary>
    /// <param name="id">Идентификатор кэш-пользователя</param>
    /// <returns>Результат операции удаления</returns>
    /// <response code="200">Кэш-пользователь успешно удалён</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpDelete]
    public async Task<ICsmActionResult> Delete([FromQuery] int id)
        => await HandleCommand(new DeleteCashUserCommand { CashbackId = id });
}