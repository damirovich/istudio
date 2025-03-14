﻿using ISTUDIO.Application.Features.UserManagement.Commands.DeleteUser;
using ISTUDIO.Application.Features.UserManagement.Commands.UpdateUserPhotoProfile;
using ISTUDIO.Application.Features.UserManagement.Queries;
using ISTUDIO.Application.Helpers;

namespace ISTUDIO.Web.Api.Shop.Controllers.v1;

[ApiVersion("1.0")]
public class UsersController : BaseController
{
    private readonly IFileStoreService _fileStoreService;
    public UsersController(IFileStoreService fileStoreService)
        => (_fileStoreService) = (fileStoreService);
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

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdatePhotoUser([FromQuery] string userId, IFormFile photoUser)
    {
        try
        {
            var fileByte = await HelperServices.ConvertToByteArrayAsync(photoUser);
            var photoUrl = await _fileStoreService.SaveImage(fileByte);


            var result = await Mediator.Send(new UpdateUserPhotoProfileCommand
            {
                UserId = userId,
                PhotoUrl = photoUrl,

            });
            if (result.Succeeded)
                return Ok(result);
            return StatusCode(StatusCodes.Status503ServiceUnavailable, result.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsers([FromQuery] string userId)
    {
        try
        {
            var result = await Mediator.Send(new GetMobileUserByIdQuery { UserId = userId });
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
