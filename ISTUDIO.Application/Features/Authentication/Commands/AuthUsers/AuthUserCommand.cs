
using ISTUDIO.Application.Features.Authentication.DTOs;

namespace ISTUDIO.Application.Features.Authentication.Commands.AuthUsers;

public class AuthUserCommand : IRequest<AuthResponseDTO>
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
