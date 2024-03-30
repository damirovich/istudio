using ISTUDIO.Application.Features.UserManagement.Commands.EditUserProfile;

namespace ISTUDIO.Contracts.Features.UserManagement;

public class EditUserVM : IMapWith<EditUserProfileCommand>
{
    
    [Required]
    public string UserId { get; set; }

    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Email { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditUserVM, EditUserProfileCommand>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}