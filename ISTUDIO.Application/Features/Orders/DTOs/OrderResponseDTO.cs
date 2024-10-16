using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Orders.DTOs;

public class OrderResponseDTO : IMapWith<OrderEntity>
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalQuentyProduct { get; set; }
    public string Status { get; set; }
    public string CustomerName { get; set; }
    public string ShippingAddress { get; set; }
    public string? UserPhoneNumber { get; set; }
    public string UserId { get; set; }
    public DateTime CreateDate { get; set; }
    public List<OrdersMagazineDTO> OrdersDetails { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderEntity, OrderResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.TotalQuentyProduct, opt => opt.MapFrom(src => src.TotalQuantyProduct))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customers.FirstOrDefault().FullName))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
            .ForMember(dest => dest.OrdersDetails, opt => opt.MapFrom(src => src.Details));
    }

}

