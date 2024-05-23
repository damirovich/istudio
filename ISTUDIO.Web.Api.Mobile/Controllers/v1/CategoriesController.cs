using ISTUDIO.Application.Features.Categories.Queries;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class CategoriesController : BaseController
{
    /// <summary>
    /// Получение список категорий
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
