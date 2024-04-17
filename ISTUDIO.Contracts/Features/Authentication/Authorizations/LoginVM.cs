using ISTUDIO.Application.Features.Authentication.Commands.AuthUsers;

namespace ISTUDIO.Contracts.Features.Authentication.Authorizations;

public class LoginVM : IMapWith<AuthUserCommand>
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginVM, AuthUserCommand>()
             .ForMember(u => u.UserName,
                x => x.MapFrom(c => c.UserName))
            .ForMember(u => u.Password,
                x => x.MapFrom(c => c.Password));
    }
}
