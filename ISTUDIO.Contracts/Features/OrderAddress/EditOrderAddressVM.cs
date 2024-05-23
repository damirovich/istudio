using ISTUDIO.Application.Features.OrderAddress.Commands.EditOrderAddress;

namespace ISTUDIO.Contracts.Features.OrderAddress;

public class EditOrderAddressVM : IMapWith<EditOrderAddressCommand>
{
    [Required(ErrorMessage = "Id is required.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Region is required.")]
    public string Region { get; set; }

    [Required(ErrorMessage = "City is required.")]
    public string City { get; set; }
    public string? Address { get; set; }
    public string? Comments { get; set; }
    public string? UserId { get; set; }
    public int? OrderId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditOrderAddressVM, EditOrderAddressCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId));
    }
}
