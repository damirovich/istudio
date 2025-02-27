using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v1;

[ApiVersion("1.0", Deprecated = true)]
[Authorize]
public class FilesStoreController : BaseController
{
    private readonly IFileStoreService _fileStoreService;

    public FilesStoreController(IFileStoreService fileStoreService)
        => _fileStoreService = fileStoreService;
    [HttpGet]
    public async Task<IActionResult> GetFile(string photoFilePath)
    {
        try
        {
            // Получаем содержимое файла в виде массива байтов асинхронно
            var fileContent = await _fileStoreService.GetImage(photoFilePath);

            if (fileContent == null || fileContent.Length == 0)
            {
                return NotFound(); // Файл не найден или пустой
            }

            // Конвертируем содержимое файла в строку base64
            var base64String = Convert.ToBase64String(fileContent);

            // Возвращаем строку base64 в ответе
            return Ok(new { File = base64String });
        }
        catch (ArgumentException ex)
        {
            // Ловим исключение, если переданный путь к файлу пуст или null
            return BadRequest(ex.Message);
        }
        catch (FileNotFoundException ex)
        {
            // Ловим исключение, если файл не найден
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
        catch (Exception)
        {
            // Ловим любые другие исключения и возвращаем 500 ошибку сервера
            return StatusCode(500);
        }
    }
}
