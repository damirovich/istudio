using ISTUDIO.Application.Features.TotalOrdersFavorite.Queries;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;


[ApiVersion("1.0")]
public class TotalOrderFavoriteController : BaseController
{
    private readonly IMapper _mapper;

    public TotalOrderFavoriteController(IMapper mapper) =>
        _mapper = mapper;


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetTotalOrderFavorites([FromQuery] string userId)
    {
        try
        {
            var query = new GetTotalOrderFavoritesQuery { UserId = userId };

            var result = await Mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
