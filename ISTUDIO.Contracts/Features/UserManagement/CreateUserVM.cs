using ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;
using ISTUDIO.Application.Features.UserManagement.DTOs;

namespace ISTUDIO.Contracts.Features.UserManagement;

public class CreateUserVM : IMapWith<CreateUserCommand>
{
    public string FullName { get { return LastName + " " + FirstName + " " + MiddleName; } }

    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string MiddleName { get; set; }
    public List<string> Roles { get; set; }

    // Изображения пользователя
    public List<UserImageDTO> Images { get; set; } = new List<UserImageDTO>();

    // Данные о родственниках пользователя
    public List<FamilyMemberDTO> FamilyMembers { get; set; } = new List<FamilyMemberDTO>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserVM, CreateUserCommand>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.FamilyMembers, opt => opt.MapFrom(src => src.FamilyMembers));
    }
}
