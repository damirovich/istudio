using ISTUDIO.Application.Features.OrderAddress.Commands.CreateOrderUserAddress;

namespace ISTUDIO.Contracts.Features.OrderAddress;

public class CreateOrderUserAddressVM : IMapWith<CreateOrderUserAddressCommand>
{
    [Required(ErrorMessage = "Region is required.")]
    public string Region { get; set; }

    [Required(ErrorMessage = "City is required.")]
    public string City { get; set; }
    public string? Address { get; set; }
    public string? Comments { get; set; }
    public string? UserId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrderUserAddressVM, CreateOrderUserAddressCommand>()
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}
