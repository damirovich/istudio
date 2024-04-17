using ISTUDIO.Application.Features.Authentication.Commands.AuthUsers;
using ISTUDIO.Contracts.Features.Authentication.Authorizations;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
public class AuthController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IAppUserService _userService;
    private readonly IIdentityService _identityService;

    public AuthController(IMapper mapper, IAppUserService userService, IIdentityService identityService)
    {
        _mapper = mapper;
        _userService = userService;
        _identityService = identityService;
    }
     
    /// <summary>
    /// Метод для аудентификации и авторизации
    /// </summary>
    /// <param name="login">UserName and Password</param>
    /// <returns>User data, Access token and Refresh token</returns>
    [HttpPost("Login")]
    public async Task<ICsmActionResult> Login([FromBody] LoginVM login)
    {
        try
        {
            var command = _mapper.Map<AuthUserCommand>(login);

            return new CsmActionResult(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }

    }
}
