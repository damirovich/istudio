using FluentValidation;
using MediatR;
using System.Net;

namespace ISTUDIO.Web.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Produces("application/json")]
[ApiController]

public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();


    protected async Task<IActionResult> HandleQuery<TResponse>(IRequest<TResponse> query)
    {
        try
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var errorResponse = new
            {
                error = "Validation Error",
                message = "One or more validation errors occurred.",
                errors = errors
            };

            return BadRequest(errorResponse);
        }
        catch (NotFoundException ex)
        {
            var errorResponse = new
            {
                error = "Not Found",
                message = ex.Message
            };

            return NotFound(errorResponse);
        }
        catch (Exception ex)
        {
            var errorResponse = new
            {
                error = "Internal Server Error",
                message = "An unexpected error occurred.",
                details = ex.Message // Только для разработки
            };

            return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
        }
    }

    protected async Task<IActionResult> HandleCommand<T>(IRequest<T> command)
    {
        try
        {
            await Mediator.Send(command);
            return Ok();
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var errorResponse = new
            {
                error = "Validation Error",
                message = "One or more validation errors occurred.",
                errors = errors
            };

            return BadRequest(errorResponse);
        }
        catch (NotFoundException ex)
        {
            var errorResponse = new
            {
                error = "Not Found",
                message = ex.Message
            };

            return NotFound(errorResponse);
        }
        catch (Exception ex)
        {
            var errorResponse = new
            {
                error = "Internal Server Error",
                message = "An unexpected error occurred.",
                details = ex.Message // Только для разработки
            };

            return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
        }
    }
}
