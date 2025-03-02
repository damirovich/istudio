using ISTUDIO.Application.Features.CashbackProduct.Commands;
using ISTUDIO.Application.Features.CashbackProduct.Queries;
using ISTUDIO.Contracts.Features.CashbackProducts;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

/// <summary>
/// Контроллер для управления кэшбэк-продуктами
/// </summary>
[ApiVersion("2.0")]
[Authorize]
public class CashbackProductController : BaseController2
{
    private readonly ILogger<CashbackProductController> _logger;
    private readonly IMapper _mapper;

    public CashbackProductController(ILogger<CashbackProductController> logger, IMapper mapper) : base(logger)
    {
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Получение списка кэшбэк-продуктов с пагинацией
    /// </summary>
    /// <param name="page">Параметры пагинации</param>
    /// <returns>Список кэшбэк-продуктов</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetCashbackProductsList([FromQuery] PaginatedListVM page)
    {
        return await HandleQuery(new GetCashbackProductsQuery
        {
            Parameters = new PaginatedParameters
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            }
        });
    }

    /// <summary>
    /// Создание нового кэшбэк-продукта
    /// </summary>
    /// <param name="cashbackProduct">Данные для создания</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Кэшбэк-продукт успешно создан</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPost]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateCashbackProduct([FromBody] CreateCashbackProductVM cashbackProduct)
    {
        var command = _mapper.Map<CreateCashbackProductCommand>(cashbackProduct);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование кэшбэк-продукта
    /// </summary>
    /// <param name="cashbackProduct">Данные для обновления</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Кэшбэк-продукт успешно обновлен</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditCashbackProduct([FromBody] EditCashbackProductVM cashbackProduct)
    {
        var command = _mapper.Map<EditCashbackProductCommand>(cashbackProduct);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление кэшбэк-продукта
    /// </summary>
    /// <param name="id">Идентификатор кэшбэк-продукта</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Кэшбэк-продукт успешно удален</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpDelete]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteCashbackProduct([FromQuery] int id)
    {
        return await HandleCommand(new DeleteCashbackProductCommand { Id = id });
    }
}
