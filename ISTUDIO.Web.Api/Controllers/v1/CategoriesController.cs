using ISTUDIO.Application.Features.Categories.Commands.CreateCategories;
using ISTUDIO.Application.Features.Categories.Commands.DeleteCategories;
using ISTUDIO.Application.Features.Categories.Commands.EditCategories;
using ISTUDIO.Application.Features.Categories.Queries;
using ISTUDIO.Contracts.Features.Categories;
using System.Net;

namespace ISTUDIO.Web.Api.Controllers.v1;

//[Authorize]
[ApiVersion("1.0")]
public class CategoriesController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IFileStoreService _fileStoreService;
    public CategoriesController(IMapper mapper, IFileStoreService fileStoreService)
    {
        _mapper = mapper;
        _fileStoreService = fileStoreService;
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
    /// Добавление категории
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateCategories([FromForm] CreateCategoriesVM category, IFormFile? photoCategory)
    {
        try
        {
            string photoFilePath = string.Empty;

            if (photoCategory != null)
            {
                photoFilePath = await _fileStoreService.SaveImage(photoCategory);
            }

            var command = _mapper.Map<CreateCategoriesCommand>(category);

            // Передаем путь к фотографии в команду
            command.PhotoFilePath = photoFilePath;

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
    public async Task<ICsmActionResult> EditCategories([FromForm] EditCategoriesVM category, IFormFile? photoCategory)
    {
        try
        {
            string photoFilePath = string.Empty;

            if (photoCategory != null)
            {
                photoFilePath = await _fileStoreService.SaveImage(photoCategory);
            }

            var command = _mapper.Map<EditCategoriesCommand>(category);

            // Передаем путь к фотографии в команду
            command.PhotoFilePath = photoFilePath;


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

    private ICsmActionResult BadRequest(string message)
    {
        return new CsmActionResult(new CsmReturnStatus
        {
            Status = (int)HttpStatusCode.BadRequest,
            Message = message
        });
    }
}
