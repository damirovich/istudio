
using ISTUDIO.Application.Features.Banners.Commands.CreateBanners;
using ISTUDIO.Application.Features.Banners.Commands.DeleteBannes;
using ISTUDIO.Application.Features.Banners.Commands.EditBanners;
using ISTUDIO.Application.Features.Banners.Queries;
using ISTUDIO.Contracts.Features.Banners;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

/// <summary>
/// Контроллер для управления баннерами.
/// Позволяет создавать, обновлять, удалять и получать список баннеров.
/// </summary>
[ApiVersion("2.0")]
[Authorize]
public class BannersController : BaseController2
{
    private readonly ILogger<BannersController> _logger;
    private readonly IMapper _mapper;
    public BannersController(ILogger<BannersController> logger, IMapper mapper) : base(logger)
    {
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Получение списка всех баннеров
    /// </summary>
    /// <returns>Список баннеров</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="500">Внутренная ошибка </response>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ICsmActionResult> GetBannersList()
    {
        return await HandleQuery(new GetBannersListQuery());
    }

    /// <summary>
    /// Получение данных конкретного баннера по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор баннера</param>
    /// <returns>Данные баннера</returns>
    /// <response code="200">Успешное получение данных</response>
    /// <response code="400">Некорректные данные запроса</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="500">Внутренная ошибка </response>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ICsmActionResult> GetBannersById([FromQuery] int id)
    {
        return await HandleQuery(new GetBannersByIdQuery { BannerId = id });
    }

    /// <summary>
    /// Добавление нового баннера
    /// </summary>
    /// <param name="banner">Данные для создания баннера</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Баннер успешно добавлен</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="500">Внутренная ошибка </response>
    [HttpPost]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ICsmActionResult> CreateBanner([FromBody] CreateBannerVM banner)
    {
        var command = _mapper.Map<CreateBannersCommand>(banner);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование существующего баннера
    /// </summary>
    /// <param name="banner">Данные для обновления баннера</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Баннер успешно обновлен</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="500">Внутренная ошибка </response>
    [HttpPut]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ICsmActionResult> EditBanner([FromBody] EditBannerVM banner)
    {
        var command = _mapper.Map<EditBannerCommand>(banner);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление баннера по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор баннера</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Баннер успешно удален</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="500">Внутренная ошибка сервера</response>
    [HttpDelete]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ICsmActionResult> DeleteBanner([FromQuery] int id)
    {
        return await HandleCommand(new DeleteBannerCommand { BannerId = id });
    }
}
