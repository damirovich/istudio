
using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.ShoppingCarts.DTOs;

// Определение DTO для продуктов в корзине
public class ProductsShoppinDTO : IMapWith<ProductsEntity>
{
    public int CartId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public int QuantyProductStock { get; set; }
    public int QuantyProductCart { get; set; } // Количество продукта в корзине
    public decimal SumProductCart { get; set; } // Сумма продукта в корзине
    public ICollection<ProductImagesDTO> Images { get; set; }
    public ProductDiscountDTO ProductDiscount { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductsEntity, ProductsShoppinDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.ShoppingCarts.FirstOrDefault().Id))
            .ForMember(dest => dest.QuantyProductStock, opt => opt.MapFrom(src => src.QuantityInStock))
            .ForMember(dest => dest.QuantyProductCart, opt => opt.MapFrom(src => src.ShoppingCarts.FirstOrDefault() != null ? src.ShoppingCarts.FirstOrDefault().QuantyProduct : 0))
            .ForMember(dest => dest.SumProductCart, opt => opt.MapFrom(src => src.Price * (src.ShoppingCarts.FirstOrDefault() != null ? src.ShoppingCarts.FirstOrDefault().QuantyProduct : 0)))
            .ForMember(dest => dest.ProductDiscount, opt => opt.MapFrom(src => src.Discount));
    }
}
