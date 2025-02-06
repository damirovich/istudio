using ISTUDIO.Application.Features.CashUsers.Commands.CreateCashUsers;
using ISTUDIO.Application.Features.CashUsers.Commands.DeleteCashUsers;
using ISTUDIO.Application.Features.CashUsers.Commands.EditCashUsers;
using ISTUDIO.Application.Features.CashUsers.Queries;
using ISTUDIO.Contracts.Features.UserCashback;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
public class UserCashbacksController : BaseController
{
    private readonly IMapper _mapper;

    public UserCashbacksController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Получение списка кэшбеков пользователей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserCashbackList([FromQuery] PaginatedListVM page)
    {
        try
        {
            var result = await Mediator.Send(new GetCashbackUsersQuery
            {
                Parameters = new PaginatedParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize
                }
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получение кэшбека пользователя по ID
    /// </summary>
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserCashbackById(string userId)
    {
        try
        {
            var result = await Mediator.Send(new GetUserCashbackByUserIdQuery
            {
                UserId = userId
            });

            if (result == null)
                return NotFound($"Кэшбек для пользователя с ID {userId} не найден.");

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    ///// <summary>
    ///// Создание нового кэшбека для пользователя
    ///// </summary>
    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> CreateUserCashback([FromBody] CreateCashUserVM userCashback)
    //{
    //    try
    //    {
    //        var command = _mapper.Map<CreateCashUserCommand>(userCashback);
    //        var result = await Mediator.Send(command);

    //        if (result.Succeeded)
    //            return CreatedAtAction(nameof(GetUserCashbackById), new { userId = result.Succeeded }, result);

    //        return BadRequest(result.Errors);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //    }
    //}

    ///// <summary>
    ///// Редактирование кэшбека пользователя
    ///// </summary>
    //[HttpPut]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> EditUserCashback([FromBody] EditCashUserVM userCashback)
    //{
    //    try
    //    {
    //        var command = _mapper.Map<EditCashUserCommand>(userCashback);
    //        var result = await Mediator.Send(command);

    //        if (result.Succeeded)
    //            return Ok(result);

    //        return BadRequest(result.Errors);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //    }
    //}

    ///// <summary>
    ///// Удаление кэшбека пользователя
    ///// </summary>
    //[HttpDelete("{id}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> DeleteUserCashback(int id)
    //{
    //    try
    //    {
    //        var result = await Mediator.Send(new DeleteCashUserCommand { CashbackId = id });

    //        if (result.Succeeded)
    //            return Ok(result);

    //        return BadRequest(result.Errors);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //    }
    //}
}
