﻿using ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;
using ISTUDIO.Application.Features.UserManagement.Commands.EditUserProfile;
using ISTUDIO.Application.Features.UserManagement.Commands.UpdatePassword;
using ISTUDIO.Application.Features.UserManagement.Queries;
using ISTUDIO.Contracts.Features.UserManagement;
using Microsoft.AspNetCore.Authorization;


namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0", Deprecated = true)]
[Authorize]
public class UsersController : BaseController
{
    private readonly IMapper _mapper;

    public UsersController(IMapper mapper)
    {
        _mapper = mapper;
    }
    /// <summary>
    /// Получение список всех пользователей системы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetUsersList()
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetUserListQuery()));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Получение данных пользовател по Id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetUsersById([FromQuery] string userId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetUserByIdQuery { UserId = userId}));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Получение данных пользователя по номеру телефона
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetUsersByPhoneNumber([FromQuery] string phoneNumber)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetUserByPhoneNumberQuery { PhoneNumber = phoneNumber }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Метод для создание нового пользователя
    /// </summary>
    /// <param name="user">User Data</param>
    /// <returns>UserId</returns>
    [HttpPost("Register")]
    public async Task<ICsmActionResult> CreateUser([FromBody] CreateUserVM user)
    {
        try
        {
            var command = _mapper.Map<CreateUserCommand>(user);

            return new CsmActionResult(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Метод для редактирование данных пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ICsmActionResult> EditUser([FromBody] EditUserVM user)
    {
        try
        {
            var command = _mapper.Map<EditUserProfileCommand>(user);

            return new CsmActionResult<Result>(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Изменение пароля
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ICsmActionResult> UpdatePassword([FromBody] UpdatePasswordVM user)
    {
        try
        {
            var command = _mapper.Map<UpdatePasswordCommand>(user);

            return new CsmActionResult<Result>(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    
}
