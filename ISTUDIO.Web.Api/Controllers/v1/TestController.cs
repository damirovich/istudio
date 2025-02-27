using ISTUDIO.Domain.Enums;
using ISTUDIO.Web.Api.AppStart;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0", Deprecated = true)]
[Authorize]
public class TestController : BaseController
{
    [AuthorizePermission(PermissionEnum.CanManageUsers)]
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
