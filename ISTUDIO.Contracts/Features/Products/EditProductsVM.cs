using ISTUDIO.Application.Features.Products.Commands.EditProducts;
using ISTUDIO.Application.Features.Products.DTOs;

namespace ISTUDIO.Contracts.Features.Products;

/// <summary>
/// Модель для редактирования информации о продукте.
/// </summary>
public class EditProductsVM : IMapWith<EditProductsCommand>
{
    /// <summary>
    /// Уникальный идентификатор продукта.
    /// </summary>
    public int Id { get; set; }

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
    /// Идентификатор категории продукта (если применимо).
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// Идентификатор скидки (если применимо).
    /// </summary>
    public int? DiscountId { get; set; }

    /// <summary>
    /// Идентификатор магазина, в котором продается продукт (если применимо).
    /// </summary>
    public int? MagazineId { get; set; }

    /// <summary>
    /// Конфигурация маппинга между EditProductsVM и EditProductsCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditProductsVM, EditProductsCommand>();

        // Маппинг данных из DTO в VM для редактирования продукта.
        profile.CreateMap<ProductsResponseDTO, EditProductsVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.ProductCategory))
            .ForMember(dest => dest.MagazineId, opt => opt.MapFrom(src => src.ProductMagazine.MagazineId));
    }
}