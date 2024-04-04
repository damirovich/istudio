using ISTUDIO.Application.Features.Categories.Queries;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

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
    /// Получение список всех пользователей системы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoriesList()
    {
        try
        {
            var result = await Mediator.Send(new GetCategoriesListQuery());

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
