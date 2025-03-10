using ISTUDIO.Application.Features.Products.Commands.AddPhotosProducts;
using ISTUDIO.Application.Features.Products.Commands.DeletePhotosProducts;
using ISTUDIO.Application.Features.Products.Commands.EditPhotosProducts;
using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Application.Features.Products.Queries;
using ISTUDIO.Contracts.Features.Products;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]
[Route("api/v{version:apiVersion}/product-images")]
public class ProductImagesController : BaseController2
{
    private readonly IFileStoreService _fileStoreService;
    private readonly IMapper _mapper;

    public ProductImagesController(IFileStoreService fileStoreService, IMapper mapper, ILogger<ProductImagesController> logger)
        : base(logger)
    {
        _fileStoreService = fileStoreService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получение фотографий продукта по идентификатору продукта
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <returns>Фотографии продукта</returns>
    /// <response code="200">Фотографии успешно получены</response>
    /// <response code="404">Фотографии не найдены</response>
    [HttpGet("{productId:int}")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ICsmActionResult> GetProductPhotosById([FromRoute] int productId)
    {
        return await HandleQuery(new GetProductPhotosByIdQuery { ProductId = productId });
    }

    /// <summary>
    /// Изменение всех фотографий продукта
    /// </summary>
    /// <param name="editPhotos">Данные для изменения фотографий</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Фотографии успешно обновлены</response>
    /// <response code="400">Ошибка валидации данных</response>
    [HttpPut("edit-all")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICsmActionResult> EditAllProductPhotos([FromBody] EditAllPhotosProductVM editPhotos)
    {
        var productImages = new List<ProductImagesDTO>();

        foreach (var photo in editPhotos.ProductPhotos)
        {
            if (!string.IsNullOrEmpty(photo))
            {
                var fileByte = Convert.FromBase64String(photo);
                var photoUrl = await _fileStoreService.SaveImage(fileByte);
                var fileName = Path.GetFileName(photoUrl);

                productImages.Add(new ProductImagesDTO
                {
                    Url = photoUrl,
                    Name = fileName,
                    ContentType = "image/png",
                });
            }
        }

        var command = new EditAllPhotosProductCommand
        {
            ProductId = editPhotos.ProductId,
            Images = productImages
        };

        return await HandleCommand(command);
    }

    /// <summary>
    /// Добавление фотографии к продукту
    /// </summary>
    /// <param name="addPhoto">Данные новой фотографии продукта</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Фотография успешно добавлена</response>
    /// <response code="400">Ошибка валидации данных</response>
    [HttpPost("add-photo")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICsmActionResult> AddProductPhotos([FromBody] AddPhotosProductsVM addPhoto)
    {
        var fileByte = Convert.FromBase64String(addPhoto.ProductPhotos);
        var photoUrl = await _fileStoreService.SaveImage(fileByte);
        var fileName = Path.GetFileName(photoUrl);

        var command = new AddPhotosProductsCommand
        {
            ProductId = addPhoto.ProductId,
            Photo = new ProductImagesDTO
            {
                Url = photoUrl,
                Name = fileName,
                ContentType = "image/png"
            }
        };

        return await HandleCommand(command);
    }

    /// <summary>
    /// Изменение конкретной фотографии продукта по идентификатору
    /// </summary>
    /// <param name="editPhoto">Данные для изменения фотографии продукта</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Фотография успешно изменена</response>
    /// <response code="400">Ошибка валидации данных</response>
    [HttpPut("edit-photo")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICsmActionResult> EditProductPhoto([FromBody] EditPhotoProductVM editPhoto)
    {
        var fileByte = Convert.FromBase64String(editPhoto.ProductPhotos);
        var photoUrl = await _fileStoreService.SaveImage(fileByte);
        var fileName = Path.GetFileName(photoUrl);

        var command = new EditPhotosProductCommand
        {
            Id = editPhoto.Id,
            Photo = new ProductImagesDTO
            {
                Url = photoUrl,
                Name = fileName,
                ContentType = "image/png"
            }
        };

        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление фотографии продукта по идентификатору фотографии
    /// </summary>
    /// <param name="photoId">Идентификатор фотографии продукта</param>
    /// <returns>Результат операции</returns>
    /// <response code="200">Фотография успешно удалена</response>
    /// <response code="400">Ошибка валидации данных</response>
    [HttpDelete("{photoId:int}")]
    [ProducesResponseType(typeof(CsmActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICsmActionResult> DeleteProductPhotos([FromRoute] int photoId)
    {
        return await HandleCommand(new DeletePhotosProductCommand
        {
            ProductImagesId = photoId
        });
    }
}
