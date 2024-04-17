using ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;

namespace ISTUDIO.Contracts.Features.UserManagement;

public class CreateUserVM : IMapWith<CreateUserCommand>
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public List<string> Roles { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserVM, CreateUserCommand>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
    }
}
