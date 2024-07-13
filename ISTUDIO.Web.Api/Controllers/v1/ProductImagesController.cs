
using ISTUDIO.Application.Features.Products.Commands.EditPhotosProducts;
using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Application.Features.Products.Queries;
using ISTUDIO.Contracts.Features.Products;

namespace ISTUDIO.Web.Api.Controllers.v1;
[ApiVersion("1.0")]
public class ProductImagesController : BaseController
{
    private readonly IFileStoreService _fileStoreService;
    private readonly IMapper _mapper;

    public ProductImagesController(IFileStoreService fileStoreService, IMapper mapper)
        => (_fileStoreService, _mapper) = (fileStoreService, mapper);

    /// <summary>
    /// Получение фото продуктов
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ICsmActionResult> GetProductPhotosById(int productId)
    {
        try
        {
            var query = new GetProductPhotosByIdQuery { ProductId = productId };
            var result = await Mediator.Send(query);

            return new CsmActionResult(result);

        }
        catch (NotFoundException ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Изменение все фотографии продукта 
    /// </summary>
    /// <param name="editPhotos"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICsmActionResult> EditAllProductPhotos([FromBody] EditAllPhotosProductVM editPhotos)
    {
        try
        {
            var productImages = new List<ProductImagesDTO>();

            foreach (var photo in editPhotos.ProductPhotos)
            {
                if (photo != null)
                {
                    var fileByte = Convert.FromBase64String(photo);

                    var photoUrl = await _fileStoreService.SaveImage(fileByte);

                    productImages.Add(new ProductImagesDTO
                    {
                        Url = photoUrl,
                        Name = $"image_{DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss")}.png",
                        ContentType = "image/png",
                    });
                }
            }

            var command = new EditAllPhotosProductCommand
            {
                ProductId = editPhotos.ProductId,
                Images = productImages
            };

            var result = await Mediator.Send(command);
            if (result.Succeeded)
                return new CsmActionResult(result);

            return new CsmActionResult(result.Errors);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


    /// <summary>
    /// Изменение фото продукта по Id 
    /// </summary>
    /// <param name="editPhoto"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICsmActionResult> EditProductPhotos([FromBody] EditPhotoProductVM editPhoto)
    {
        try
        {
            var fileByte = Convert.FromBase64String(editPhoto.ProductPhotos);

            var photoUrl = await _fileStoreService.SaveImage(fileByte);

            var command = new EditPhotosProductCommand
            {
                Id = editPhoto.Id,
                Photo = new ProductImagesDTO()
                {
                    Url = photoUrl,
                    Name = $"image_{DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss")}.png",
                    ContentType = "image/png",
                }
            };

            var result = await Mediator.Send(command);
            if (result.Succeeded)
                return new CsmActionResult(result);

            return new CsmActionResult(result.Errors);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }


}
