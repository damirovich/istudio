using ISTUDIO.Application.Features.SubCategories.Commands.CreateSubCategories;
using ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;
using ISTUDIO.Contracts.Features.SubCategories;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]
public class SubCategoriesController : BaseController2
{
    private readonly IMapper _mapper;

    public SubCategoriesController(IMapper mapper, ILogger<SubCategoriesController> logger) : base(logger)
        => _mapper = mapper;

    /// <summary>
    /// Создание новой подкатегории
    /// </summary>
    /// <param name="subCategory">Данные новой подкатегории</param>
    /// <returns>Результат операции создания подкатегории</returns>
    /// <response code="200">Подкатегория успешно создана</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPost]
    public async Task<ICsmActionResult> Create([FromBody] CreateSubCategoriesVM subCategory)
    {
        var command = _mapper.Map<CreateSubCategoriesCommand>(subCategory);
        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование существующей подкатегории
    /// </summary>
    /// <param name="id">Идентификатор подкатегории</param>
    /// <param name="subCategory">Данные для редактирования</param>
    /// <returns>Результат операции редактирования подкатегории</returns>
    /// <response code="200">Подкатегория успешно обновлена</response>
    /// <response code="400">Ошибка валидации данных</response>
    /// <response code="401">Пользователь не авторизован</response>
    [HttpPut]
    public async Task<ICsmActionResult> Edit( [FromBody] EditSubCategoriesVM subCategory)
    {
        var command = _mapper.Map<EditSubCategoriesCommand>(subCategory);

        return await HandleCommand(command);
    }
}
