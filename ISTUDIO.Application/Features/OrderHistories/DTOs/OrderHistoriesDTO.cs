using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.OrderHistories.DTOs;

public class OrderHistoriesDTO : IMapWith<OrderStatusHistoryEntity>
{
    public int Id { get; set; }
    public string? Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStatusHistoryEntity, OrderHistoriesDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            
    }
}
