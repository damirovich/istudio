using ISTUDIO.Application.Features.Authentication.Commands.RefreshJWT;

namespace ISTUDIO.Contracts.Features.Authentication.JWTTokens;

public class TokenVM : IMapWith<RefreshJWTCommand>
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TokenVM, RefreshJWTCommand>()
            .ForMember(t => t.AccessToken,
                x => x.MapFrom(rt => rt.AccessToken))
            .ForMember(t => t.RefreshToken,
                x => x.MapFrom(rt => rt.RefreshToken));
    }
}
