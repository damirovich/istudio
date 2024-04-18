using ISTUDIO.Application.Features.Categories.Commands.CreateCategories;
using ISTUDIO.Application.Features.Categories.Commands.DeleteCategories;
using ISTUDIO.Application.Features.Categories.Commands.EditCategories;
using ISTUDIO.Application.Features.Categories.Queries;
using ISTUDIO.Contracts.Features.Categories;

namespace ISTUDIO.Web.Api.Controllers.v1;

//[Authorize]
[ApiVersion("1.0")]
public class CategoriesController : BaseController
{
    private readonly IMapper _mapper;
    public CategoriesController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Получение список категорий
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetCategoriesList()
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetCategoriesListQuery()));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Получение данные категории по Id
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ICsmActionResult> GetCategoriesById([FromQuery] int categoryId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetCategoriesByIdQuery { Id = categoryId }));
        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Добавление категории
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateCategories([FromBody] CreateCategoriesVM category)
    {
        try
        {

            var command = _mapper.Map<CreateCategoriesCommand>(category);

            var result = await Mediator.Send(command);
            if(result.Succeeded)
                return new CsmActionResult(result);

            return new CsmActionResult(result.Errors);

        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Редактирование данных категории
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditCategories([FromBody] EditCategoriesVM category)
    {
        try
        {
          
            var command = _mapper.Map<EditCategoriesCommand>(category);

            var result = await Mediator.Send(command);

            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Удаление данных категории
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteCategory([FromQuery] int Id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteCategoriesCommand { CategoryId = Id });
            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    
}
