using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Orders.DTOs;

public class OrderDetailsResponseDTO : IMapWith<OrderDetailEntity>
{
    public int OrderDetailsId { get; set; }
    public string ProductName { get; set; }
    public string ProductModel { get; set; }
    public string ProductImageUrl { get; set; }
    public int QuentyProduct {  get; set; }
    public decimal ProductPrice { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TotalPrice { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderDetailEntity, OrderDetailsResponseDTO>()
            .ForMember(dest => dest.OrderDetailsId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductModel, opt => opt.MapFrom(src => src.Product.Model))
            .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => src.Product.Images.FirstOrDefault(s => s.ProductId == src.ProductId).Url))
            .ForMember(dest => dest.QuentyProduct, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));
    }
}
