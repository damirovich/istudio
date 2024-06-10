using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Orders.DTOs;

public class ProductOrderResDTO : IMapWith<ProductsEntity>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public ICollection<ProductImagesDTO> Images { get; set; }
    public ProductDiscountDTO ProductDiscount { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductsEntity, ProductOrderResDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.ProductDiscount, opt => opt.MapFrom(src => src.Discount));
    }
}

