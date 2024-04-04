using ISTUDIO.Application.Features.SubCategories.Commands.CreateSubCategories;
using ISTUDIO.Application.Features.SubCategories.Commands.DeleteSubCategories;
using ISTUDIO.Application.Features.SubCategories.Commands.EditSubCategories;
using ISTUDIO.Contracts.Features.SubCategories;

namespace ISTUDIO.Web.Api.Controllers.v1;

//[Authorize]
[ApiVersion("1.0")]
public class SubCategoriesController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IFileStoreService _fileStoreService;
    public SubCategoriesController(IMapper mapper, IFileStoreService fileStoreService)
    {
        _mapper = mapper;
        _fileStoreService = fileStoreService;
    }

    /// <summary>
    /// Добавление под категории
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateSubCategories([FromForm] CreateSubCategoriesVM subCategory, IFormFile? photoSubCategory)
    {
        try
        {
            string photoFilePath = string.Empty;

            if (photoSubCategory != null)
            {
                photoFilePath = await _fileStoreService.SaveImage(photoSubCategory);
            }

            var command = _mapper.Map<CreateSubCategoriesCommand>(subCategory);

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
    /// Редактирование данных под категории
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditSubCategories([FromForm] EditSubCategoriesVM subCategory, IFormFile? photoSubCategory)
    {
        try
        {
            string photoFilePath = string.Empty;

            if (photoSubCategory != null)
            {
                photoFilePath = await _fileStoreService.SaveImage(photoSubCategory);
            }

            var command = _mapper.Map<EditSubCategoriesCommand>(subCategory);

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
    /// Удаление данных Под категории
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteSubCategory([FromQuery] int Id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteSubCategoriesCommand { SubCategoryId = Id });
            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
