using ISTUDIO.Application.Features.Authentication.DTOs;

namespace ISTUDIO.Application.Features.Authentication.Commands.RefreshJWT;

public class RefreshJWTCommand : IRequest<RefreshJWTResponseDTO>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
