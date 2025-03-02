using FluentValidation;
using MediatR;

namespace ISTUDIO.Web.Api.Controllers.v2;

[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Produces("application/json")]
[ApiController]

public class BaseController2 : ControllerBase
{
    private readonly ILogger<BaseController2> _logger;
    private IMediator _mediator;
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    public BaseController2(ILogger<BaseController2> logger)
    {
        _logger = logger;
    }

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
            _logger.LogInformation("Executing query: {Query}", query.GetType().Name);
            var result = await Mediator.Send(query);
            return new CsmActionResult(result);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation error: {Errors}", ex.Errors.Select(e => e.ErrorMessage));
            return new CsmActionResult(new CsmReturnStatus(400, "Validation Error", ex.Errors.Select(e => e.ErrorMessage)));
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning("Not Found: {Message}", ex.Message);
            return new CsmActionResult(new CsmReturnStatus(404, "Not Found", new { ex.Message }));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
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
            _logger.LogInformation("Executing command: {Command}", command.GetType().Name);
            var result = await Mediator.Send(command);
            return new CsmActionResult(result);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation error: {Errors}", ex.Errors.Select(e => e.ErrorMessage));
            return new CsmActionResult(new CsmReturnStatus(400, "Validation Error", ex.Errors.Select(e => e.ErrorMessage)));
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning("Not Found: {Message}", ex.Message);
            return new CsmActionResult(new CsmReturnStatus(404, "Not Found", new { ex.Message }));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            var errorMessage = "An unexpected error occurred.";
            #if DEBUG
                    errorMessage = ex.Message;
            #endif
            return new CsmActionResult(new CsmReturnStatus(500, "Internal Server Error", errorMessage));
        }
    }

}
