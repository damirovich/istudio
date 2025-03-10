using ISTUDIO.Application.Features.Products.Commands.CreateProducts;
using ISTUDIO.Application.Features.Products.Commands.DeleteProducts;
using ISTUDIO.Application.Features.Products.Commands.EditProducts;
using ISTUDIO.Application.Features.Products.Commands.UpdateProductIsActive;
using ISTUDIO.Application.Features.Products.Commands.UpdateProductQuantity;
using ISTUDIO.Application.Features.Products.Commands.UpdateProductSum;
using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Application.Features.Products.Queries;
using ISTUDIO.Contracts.Features.Products;
using Microsoft.AspNetCore.Authorization;

namespace ISTUDIO.Web.Api.Controllers.v2;

[ApiVersion("2.0")]
[Authorize]
public class ProductsController : BaseController2
{
    private readonly IFileStoreService _fileStoreService;
    private readonly IMapper _mapper;

    public ProductsController(IFileStoreService fileStoreService, IMapper mapper, ILogger<ProductsController> logger) : base(logger)
        => (_fileStoreService, _mapper) = (fileStoreService, mapper);

    /// <summary>
    /// Получение списка всех продуктов с пагинацией
    /// </summary>
    [HttpGet]
    public async Task<ICsmActionResult> GetList([FromQuery] PaginatedListVM page)
        => await HandleQuery(new GetProductsListQuery
        {
            Parameters = new PaginatedParameters
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            }
        });

    /// <summary>
    /// Поиск продуктов по заданному запросу
    /// </summary>
    [HttpGet("search")]
    public async Task<ICsmActionResult> Search([FromQuery] PaginatedListVM page, [FromQuery] string searchTerm)
        => await HandleQuery(new GetSearchProductsQuery
        {
            Parameters = new PaginationWithSearchParameters
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize,
                SearchTerm = searchTerm
            }
        });

    /// <summary>
    /// Получение списка новых продуктов за последнюю неделю
    /// </summary>
    [HttpGet("weekly-new")]
    public async Task<ICsmActionResult> GetWeeklyNewProducts()
        => await HandleQuery(new GetWeeklyNewProductsQuery());

    /// <summary>
    /// Получение списка продуктов по идентификатору категории
    /// </summary>
    [HttpGet("category/{categoryId:int}")]
    public async Task<ICsmActionResult> GetByCategory([FromRoute] int categoryId, [FromQuery] PaginatedListVM page)
        => await HandleQuery(new GetProductByCategoryIdQuery
        {
            CategoryId = categoryId,
            Parameters = new PaginatedParameters
            {
                PageNumber = page.PageNumber,
                PageSize = page.PageSize
            }
        });

    /// <summary>
    /// Получение данных продукта по идентификатору
    /// </summary>
    [HttpGet("{productId:int}")]
    public async Task<ICsmActionResult> GetById([FromRoute] int productId)
        => await HandleQuery(new GetProductsByIdQuery { ProductId = productId });

    /// <summary>
    /// Создание нового продукта
    /// </summary>
    [HttpPost]
    public async Task<ICsmActionResult> Create([FromBody] CreateProductsVM product)
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

        return await HandleCommand(command);
    }

    /// <summary>
    /// Редактирование существующего продукта
    /// </summary>
    [HttpPut]
    public async Task<ICsmActionResult> Edit([FromBody] EditProductsVM product)
    {
        var command = _mapper.Map<EditProductsCommand>(product);
        
        return await HandleCommand(command);
    }

    /// <summary>
    /// Обновление количества продукта
    /// </summary>
    [HttpPut]
    public async Task<ICsmActionResult> UpdateQuantity([FromBody] UpdateProductQuantityVM model)
    {
        var command = _mapper.Map<UpdateProductQuantityCommand>(model);
        
        return await HandleCommand(command);
    }

    /// <summary>
    /// Обновление суммы продукта
    /// </summary>
    [HttpPut]
    public async Task<ICsmActionResult> UpdateSum([FromBody] UpdateProductSummVM model)
    {
        var command = _mapper.Map<UpdateProductSummaCommand>(model);
        
        return await HandleCommand(command);
    }

    /// <summary>
    /// Обновление статуса активности продукта
    /// </summary>
    [HttpPut]
    public async Task<ICsmActionResult> UpdateStatus([FromBody] UpdateProductActiveVM model)
    {
        var command = _mapper.Map<UpdateProductActiveCommand>(model);
        
        return await HandleCommand(command);
    }

    /// <summary>
    /// Удаление продукта по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ICsmActionResult> Delete([FromRoute] int id)
        => await HandleCommand(new DeleteProductsCommand { ProductId = id });
}