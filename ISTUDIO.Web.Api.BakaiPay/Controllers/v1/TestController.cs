
namespace ISTUDIO.Web.Api.BakaiPay.Controllers.v1;

public class TestController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> TestWorking()
    {
        try
        {
            return Ok("Этот щедерв искусства прекрасно работает");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }
}
