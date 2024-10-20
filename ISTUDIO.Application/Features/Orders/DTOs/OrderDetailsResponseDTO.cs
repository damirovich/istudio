using ISTUDIO.Application.Features.ModelsDTO;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Orders.DTOs;

public class OrderDetailsResponseDTO : IMapWith<OrderDetailEntity>
{
    public int OrderDetailsId { get; set; }
    public int ProductId { get; set; }
    public int QuentyProduct {  get; set; }
    public decimal ProductPrice { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TotalPrice { get; set; }
    public MagazineDTO OrdersMagazines { get; set; }
    public ProductOrderResDTO ProductOrders { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderDetailEntity, OrderDetailsResponseDTO>()
            .ForMember(dest => dest.OrderDetailsId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.QuentyProduct, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.ProductOrders, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.OrdersMagazines, opt => opt.MapFrom(src => src.Magazines));
    }
}
