using ISTUDIO.Application.Features.Authentication.Commands.RefreshJWT;

namespace ISTUDIO.Contracts.Features.Authentication.JWTTokens;

/// <summary>
/// Модель запроса для обновления токена
/// </summary>
public class TokenVM : IMapWith<RefreshJWTCommand>
{
    /// <summary>
    /// Действующий Access Token
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// Действующий Refresh Token
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Настройка маппинга между TokenVM и RefreshJWTCommand
    /// </summary>
    /// <param name="profile">Профиль AutoMapper</param>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TokenVM, RefreshJWTCommand>()
            .ForMember(t => t.AccessToken,
                x => x.MapFrom(rt => rt.AccessToken))
            .ForMember(t => t.RefreshToken,
                x => x.MapFrom(rt => rt.RefreshToken));
    }
}