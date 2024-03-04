using ISTUDIO.Application.Features.Authentication.Commands.CreateUsers;

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

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserVM, CreateUserCommand>()
            .ForMember(u => u.FullName, x => x.MapFrom(c => c.FullName))
            .ForMember(u => u.UserName, x => x.MapFrom(c => c.UserName))
            .ForMember(u => u.Email, x => x.MapFrom(c => c.Email))
            .ForMember(u => u.Password, x => x.MapFrom(c => c.Password))
            .ForMember(u => u.FirstName, x => x.MapFrom(c => c.FirstName))
            .ForMember(u => u.LastName, x => x.MapFrom(c => c.LastName))
            .ForMember(u => u.MiddleName, x => x.MapFrom(c => c.MiddleName))
            .ForMember(u => u.Roles, x => x.MapFrom(c => c.Roles));
    }
}
