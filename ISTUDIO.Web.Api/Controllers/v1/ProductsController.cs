using ISTUDIO.Application.Features.Products.Commands.CreateProducts;
using ISTUDIO.Application.Features.Products.Commands.DeleteProducts;
using ISTUDIO.Application.Features.Products.Commands.EditProducts;
using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Application.Features.Products.Queries;
using ISTUDIO.Contracts.Features.Products;

namespace ISTUDIO.Web.Api.Controllers.v1;
[ApiVersion("1.0")]
public class ProductsController : BaseController
{
    private readonly IFileStoreService _fileStoreService;
    private readonly IMapper _mapper;
    public ProductsController(IFileStoreService fileStoreService, IMapper mapper)
        => (_fileStoreService, _mapper) = (fileStoreService, mapper);

   /// <summary>
   /// Получение списка всех продуктов
   /// </summary>
   /// <param name="page"></param>
   /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetProducts([FromQuery] PaginatedListVM page)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetProductsListQuery
            {
                Parameters = new PaginatedParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize
                }
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Получение списка продуктов по категории
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetProductsByCategory([FromQuery] PaginatedListVM page, int categoryId )
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetProductByCategoryIdQuery 
            { 
                Parameters = new PaginatedParameters
                {
                    PageNumber= page.PageNumber,
                    PageSize = page.PageSize
                },
                CategoryId = categoryId 
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
    /// <summary>
    /// Добавление продуктов
    /// </summary>
    /// <param name="product"></param>
    /// <param name="productPhotos"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> CreateProducts([FromForm] CreateProductsVM product, IList<IFormFile>? productPhotos)
    {
        try
        {
            var productImages = new List<ProductImagesDTO>();

            foreach (var photo in productPhotos)
            {
                if (photo != null)
                {
                    var photoUrl = await _fileStoreService.SaveImage(photo);

                    // Другие свойства из объекта IFormFile
                    var typeImg = photo.ContentType; // Пример типа изображения
                    var name = photo.FileName; // Пример имени файла

                    productImages.Add(new ProductImagesDTO
                    {
                        Url = photoUrl,
                        Name = name,
                        ContentType = typeImg,
                    });
                }
            }

            var command = _mapper.Map<CreateProductsCommand>(product);
            command.Images = productImages;

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
    /// Редактирование продуктов
    /// </summary>
    /// <param name="product"></param>
    /// <param name="photoProducts"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> EditProducts([FromForm] EditProductsVM product, List<IFormFile> photoProducts)
    {
        try
        {
            var productImages = new List<ProductImagesDTO>();

            foreach (var photo in photoProducts)
            {
                if (photo != null)
                {
                    var photoUrl = await _fileStoreService.SaveImage(photo);

                    // Другие свойства из объекта IFormFile
                    var typeImg = photo.ContentType; // Пример типа изображения
                    var name = photo.FileName; // Пример имени файла

                    productImages.Add(new ProductImagesDTO
                    {
                        Url = photoUrl,
                        Name = name,
                        ContentType = typeImg,
                    });
                }
            }

            var command = _mapper.Map<EditProductsCommand>(product);
            command.Images = productImages;

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


    // <summary>
    /// Удаление данных продукта
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> DeleteProducts([FromQuery] int Id)
    {
        try
        {
            var result = await Mediator.Send(new DeleteProductsCommand { ProductId = Id });
            return new CsmActionResult<Result>(result);
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }
}
