
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]
public class TestController : BaseController2
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger) : base(logger)
        => _logger = logger;


    /// <summary>
    /// Метод для проверки API 
    /// </summary>
    /// <returns>Сообщение об успехе или ошибка</returns>
    /// <response code="200">Успешно API Работает</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> TestWorkingAPI()
    {
        try
        {
            return await Task.FromResult(Ok("Этот шедевр искусства прекрасно работает"));

        }
        catch (Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }
} 
