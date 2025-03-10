using ISTUDIO.Application.Features.Magazines.Commands.CreateMagazines;
using ISTUDIO.Application.Features.Magazines.Commands.DeleteMagazines;
using ISTUDIO.Application.Features.Magazines.Commands.EditMagazines;
using ISTUDIO.Application.Features.Magazines.Queries;
using ISTUDIO.Contracts.Features.Magazines;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

/// <summary>
/// Контроллер для управления магазинами
/// </summary>
[ApiVersion("2.0")]
[Authorize]
public class MagazinesController : BaseController2
{
    private readonly IMapper _mapper;
    private readonly ILogger<MagazinesController> _logger;
    public MagazinesController(IMapper mapper, ILogger<MagazinesController> logger) : base (logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Получение списка магазинов с пагинацией
    /// </summary>
    /// <param name="page">Параметры пагинации</param>
    /// <returns>Список магазинов</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetMagazinesList([FromQuery] PaginatedListVM page)
    {
        return await HandleQuery(new GetMagazineListQuery
        {
            Parameters = new PaginatedParameters
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            }
        });
    }

    /// <summary>
    /// Получение информации о магазине по ID
    /// </summary>
    /// <param name="id">Идентификатор магазина</param>
    /// <returns>Данные магазина</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetMagazineById([FromQuery] int id)
    {
        return await HandleQuery(new GetMagazineByIdQuery { MagazineId = id });
    }

    /// <summary>
    /// Добавление нового магазина
    /// </summary>
    /// <param name="magazine">Данные магазина</param>
    /// <returns>Результат операции</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateMagazine([FromBody] CreateMagazineVM magazine)
    {
        var command = _mapper.Map<CreateMagazinesCommand>(magazine);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование данных магазина
    /// </summary>
    /// <param name="magazine">Данные магазина</param>
    /// <returns>Результат операции</returns>
    [HttpPut]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditMagazine([FromBody] EditMagazineVM magazine)
    {
        var command = _mapper.Map<EditMagazinesCommand>(magazine);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление магазина по идентификатору
    /// </summary>
    /// <param name="magazineId">Идентификатор магазина</param>
    /// <returns>Результат операции</returns>
    [HttpDelete]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteMagazine([FromQuery] int magazineId)
    {
        return await HandleCommand(new DeleteMagazinesCommand { MagazineId = magazineId });
    }
}
