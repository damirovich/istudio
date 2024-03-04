using ISTUDIO.Domain.Models;

namespace ISTUDIO.Application.Features.Authentication.DTOs;

public class AuthResponseDTO
{
    public UserSessions UserSession { get; set; }
}
