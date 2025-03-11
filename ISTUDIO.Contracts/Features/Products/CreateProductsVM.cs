using ISTUDIO.Application.Features.Products.Commands.CreateProducts;

namespace ISTUDIO.Contracts.Features.Products;

/// <summary>
/// Модель для создания нового продукта.
/// </summary>
public class CreateProductsVM : IMapWith<CreateProductsCommand>
{
    /// <summary>
    /// Название продукта.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Модель продукта.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Цвет продукта.
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// Цена продукта.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Количество продукта в наличии.
    /// </summary>
    public int QuantityInStock { get; set; }

    /// <summary>
    /// Описание продукта.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Идентификатор категории продукта.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Идентификатор скидки (если применимо).
    /// </summary>
    public int? DiscountId { get; set; }

    /// <summary>
    /// Идентификатор магазина, в котором продается продукт (если применимо).
    /// </summary>
    public int? MagazineId { get; set; }

    /// <summary>
    /// Список фото продукта в формате Base64.
    /// </summary>
    public List<string> ProductPhotos { get; set; } = new List<string>();

    /// <summary>
    /// Конфигурация маппинга между CreateProductsVM и CreateProductsCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProductsVM, CreateProductsCommand>();
    }
}