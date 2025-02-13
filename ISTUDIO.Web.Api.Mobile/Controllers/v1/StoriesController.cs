
using ISTUDIO.Application.Features.Stories.Queries;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class StoriesController : BaseController
{
    private readonly IMapper _mapper;

    public StoriesController(IMapper mapper)
        => _mapper = mapper;

    /// <summary>
    /// Получение списка сторис с контентом
    /// </summary>
    [HttpGet("with-content")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStoriesWithContent()
    {
        try
        {
            var result = await Mediator.Send(new GetStoriesWithContentQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
