
using ISTUDIO.Application.Features.PayMethodts.Queries;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class PayMethodController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPayMethod()
    {
        try 
        {
            var result = await Mediator.Send(new GetPayMethodQuery());

            return Ok(result);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
