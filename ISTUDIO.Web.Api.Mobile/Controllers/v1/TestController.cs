using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
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
