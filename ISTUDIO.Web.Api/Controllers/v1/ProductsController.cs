using ISTUDIO.Application.Features.Products.Commands.CreateProducts;
using ISTUDIO.Application.Features.Products.Commands.DeleteProducts;
using ISTUDIO.Application.Features.Products.Commands.EditProducts;
using ISTUDIO.Application.Features.Products.Commands.UpdateProductQuantity;
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
    /// Метод поиска по продуктам
    /// </summary>
    /// <param name="page"></param>
    /// <param name="searchTerm"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetSearchProducts([FromQuery] PaginatedListVM page, string searchTerm)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetSearchProductsQuery
            {
                Parameters = new PaginationWithSearchParameters
                {
                    PageNumber = page.PageNumber,
                    PageSize = page.PageSize,
                    SearchTerm = searchTerm
                }
            }));
        }
        catch (Exception ex)
        {
            return new CsmActionResult(new CsmReturnStatus(-1, ex.Message));
        }
    }

    /// <summary>
    /// Новинки продуктов за последнюю неделю
    /// </summary>
    /// <param name="page"></param>
    /// <param name="searchTerm"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetWeeklyNewProducts()
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetWeeklyNewProductsQuery()));
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
    /// Получение данные продукта по ProductId
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> GetProductsById([FromQuery] int productId)
    {
        try
        {
            return new CsmActionResult(await Mediator.Send(new GetProductsByIdQuery
            {
                ProductId = productId
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
    public async Task<ICsmActionResult> CreateProducts([FromBody] CreateProductsVM product)
    {
        try
        {
            var productImages = new List<ProductImagesDTO>();

            foreach (var photo in product.ProductPhotos)
            {
                if (photo != null)
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
    public async Task<ICsmActionResult> EditProducts([FromBody] EditProductsVM product)
    {
        try
        {
            var command = _mapper.Map<EditProductsCommand>(product);

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
    /// Обновление количества продукта
    /// </summary>
    /// <param name="updateProductQuantityVM"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ICsmActionResult> UpdateProductQuantity([FromBody] UpdateProductQuantityVM updateProductQuantityVM)
    {
        try
        {
            var command = _mapper.Map<UpdateProductQuantityCommand>(updateProductQuantityVM);

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
