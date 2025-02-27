using FluentValidation;
using MediatR;

namespace ISTUDIO.Web.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Produces("application/json")]
[ApiController]

public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    /// <summary>
    /// Обработка запросов (GET)
    /// </summary>
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status500InternalServerError)]
    protected async Task<CsmActionResult> HandleQuery<TResponse>(IRequest<TResponse> query)
    {
        try
        {
            var result = await Mediator.Send(query);
            return new CsmActionResult(result);
        }
        catch (ValidationException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(400, "Validation Error", ex.Errors.Select(e => e.ErrorMessage)));
        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(404, "Not Found", new { ex.Message }));
        }
        catch (Exception ex)
        {
            // В продакшене лучше скрывать детали ошибок
            var errorMessage = "An unexpected error occurred.";
            #if DEBUG
                    errorMessage = ex.Message;
            #endif
            return new CsmActionResult(new CsmReturnStatus(500, "Internal Server Error", errorMessage));
        }
    }

    /// <summary>
    /// Обработка команд (POST, PUT, DELETE)
    /// </summary>
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status500InternalServerError)]
    protected async Task<CsmActionResult> HandleCommand<T>(IRequest<T> command)
    {
        try
        {
            var result = await Mediator.Send(command);
            return new CsmActionResult(result);
        }
        catch (ValidationException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(400, "Validation Error", ex.Errors.Select(e => e.ErrorMessage)));
        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(404, "Not Found", new { ex.Message }));
        }
        catch (Exception ex)
        {
            var errorMessage = "An unexpected error occurred.";
            #if DEBUG
                    errorMessage = ex.Message;
            #endif
            return new CsmActionResult(new CsmReturnStatus(500, "Internal Server Error", errorMessage));
        }
    }

}
