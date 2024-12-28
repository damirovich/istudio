using ISTUDIO.Application.Features.UpAppInfo.Queries;

namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

/// <summary>
/// Контроллер для работы с настройками приложения.
/// </summary>
[ApiVersion("1.0")]
public class SettingsController : BaseController
{
    private readonly ILogger _logger;

    public SettingsController(ILogger<SettingsController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Получение информации о последней версии приложения.
    /// </summary>
    /// <returns>
    /// Возвращает информацию о версии приложения и URL для обновления.
    /// </returns>
    /// <response code="200">Информация успешно получена.</response>
    /// <response code="500">Ошибка сервера.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetInfoUpdateInfo([FromQuery] string platform)
    {
        try
        {

            if (string.IsNullOrEmpty(platform))
            {
                return BadRequest("Платформа должна быть указана (iOS или Android).");
            }

            // Отправляем запрос через Mediator
            var result = await Mediator.Send(new GetInfoAppQuery
            {
                Platform = platform
            });

            // Возвращаем успешный результат
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Логируем ошибку (если логирование настроено)
            _logger.LogError(ex, "Ошибка при получении информации о версии приложения.");

            // Возвращаем статус 500 с сообщением об ошибке
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Message = "Произошла ошибка на сервере.",
                Details = ex.Message
            });
        }
    }
}
