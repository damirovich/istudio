using ISTUDIO.Application.Features.UserManagement.Commands.EditUserProfile;

namespace ISTUDIO.Contracts.Features.UserManagement;

public class EditUserVM : IMapWith<EditUserProfileCommand>
{
    public string FullName { get { return LastName + " " + FirstName + " " + MiddleName; } }
    [Required]
    public string UserId { get; set; }

    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string MiddleName { get; set; }
    public List<string> Roles { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditUserVM, EditUserProfileCommand>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
    }
}