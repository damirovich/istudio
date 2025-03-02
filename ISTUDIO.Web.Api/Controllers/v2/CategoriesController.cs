using ISTUDIO.Application.Features.Categories.Commands.CreateCategories;
using ISTUDIO.Application.Features.Categories.Commands.DeleteCategories;
using ISTUDIO.Application.Features.Categories.Commands.EditCategories;
using ISTUDIO.Application.Features.Categories.Queries;
using ISTUDIO.Contracts.Features.Categories;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]
public class CategoriesController : BaseController2
{
    private ILogger<CategoriesController> _logger;
    private IMapper _mapper;

    public CategoriesController(ILogger<CategoriesController> logger, IMapper mapper) : base(logger)
    {
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Получение списка всех категорий
    /// </summary>
    /// <returns>Список категорий</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetCategoriesList()
    {
        return await HandleQuery(new GetCategoriesListQuery());
    }

    /// <summary>
    /// Получение данных категории по идентификатору
    /// </summary>
    /// <param name="categoryId">Идентификатор категории</param>
    /// <returns>Данные категории</returns>
    /// <response code="200">Успешное получение данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="404">Категория не найдена</response>
    [HttpGet]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ICsmActionResult> GetCategoriesById([FromQuery] int categoryId)
    {
        return await HandleQuery(new GetCategoriesByIdQuery { Id = categoryId });
    }

    /// <summary>
    /// Добавление новой категории
    /// </summary>
    /// <param name="category">Данные для создания</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Категория успешно создана</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPost]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateCategories([FromBody] CreateCategoriesVM category)
    {
        var command = _mapper.Map<CreateCategoriesCommand>(category);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование категории
    /// </summary>
    /// <param name="category">Данные для обновления</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Категория успешно обновлена</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditCategories([FromBody] EditCategoriesVM category)
    {
        var command = _mapper.Map<EditCategoriesCommand>(category);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление категории по идентификатору
    /// </summary>
    /// <param name="Id">Идентификатор категории</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Категория успешно удалена</response>
    /// <response code="400">Ошибка валидации</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpDelete]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteCategory([FromQuery] int Id)
    {
        return await HandleCommand(new DeleteCategoriesCommand { CategoryId = Id });
    }
}
