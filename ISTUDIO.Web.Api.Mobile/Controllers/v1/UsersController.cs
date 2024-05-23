using ISTUDIO.Application.Features.UserManagement.Commands.DeleteUser;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

public class UsersController : BaseController
{
    // <summary>
    /// Удаление учетной записи
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RemoveUsers([FromQuery] string userId)
    {
        try
        {
            var result = await Mediator.Send(new DeleteUserCommand { UserId = userId });
            return Ok(result);
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
