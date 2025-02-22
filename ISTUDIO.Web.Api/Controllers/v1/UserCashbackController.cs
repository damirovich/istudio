using ISTUDIO.Application.Features.CashUsers.Commands.CreateCashUsers;
using ISTUDIO.Application.Features.CashUsers.Commands.DeleteCashUsers;
using ISTUDIO.Application.Features.CashUsers.Commands.EditCashUsers;
using ISTUDIO.Application.Features.CashUsers.Queries;
using ISTUDIO.Contracts.Features.UserCashback;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0")]
[Authorize]
public class UserCashbackController : BaseController
{
    private readonly IMapper _mapper;

    public UserCashbackController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetUserCashbackList([FromQuery] PaginatedListVM page)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetCashbackUsersQuery()
            {
                Parameters = new PaginatedParameters()
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize
                }
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetUserCashbackById([FromQuery] string userId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetUserCashbackByUserIdQuery
            {
                UserId = userId
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateUserCashback([FromBody] CreateCashUserVM userCashback)
    {
        try
        {
            var command = _mapper.Map<CreateCashUserCommand>(userCashback);
            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return new CsmActionResult(result);

            return new CsmActionResult(result.Errors);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditUserCashback([FromBody] EditCashUserVM userCashback)
    {
        try
        {
            var command = _mapper.Map<EditCashUserCommand>(userCashback);
            var result = await Mediator.Send(command);

            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteUserCashback([FromQuery] int id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteCashUserCommand { CashbackId = id });
            return new CsmActionResult(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
