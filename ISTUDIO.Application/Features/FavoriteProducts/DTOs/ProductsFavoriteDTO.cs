
using ISTUDIO.Application.Features.ModelsDTO;
using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.FavoriteProducts.DTOs;

public class ProductsFavoriteDTO : IMapWith<ProductsEntity>
{
    public int FavoriteProductId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string Description { get; set; }
    public ICollection<ProductImagesDTO> Images { get; set; }
    public ProductDiscountDTO ProductDiscount { get; set; }
    public MagazineDTO? ProductMagazine { get; set; }
    public int ProductCategory { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductsEntity, ProductsFavoriteDTO>()
             .ForMember(dest => dest.FavoriteProductId, opt => opt.Ignore())
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
             .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
             .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
             .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
             .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
             .ForMember(dest => dest.ProductDiscount, opt => opt.MapFrom(src => src.Discount))
             .ForMember(dest => dest.ProductMagazine, opt => opt.MapFrom(src => src.Magazine))
             .ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.CategoryId));
            
    }
}
