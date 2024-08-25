namespace ISTUDIO.Web.Api.Mobile.Controllers.v1;

[ApiVersion("1.0")]
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

            // Возвращаем файл в ответе
            return File(fileContent, "application/octet-stream", Path.GetFileName(photoFilePath));
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
        catch (Exception ex)
        {
            // Ловим любые другие исключения и возвращаем 500 ошибку сервера
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
