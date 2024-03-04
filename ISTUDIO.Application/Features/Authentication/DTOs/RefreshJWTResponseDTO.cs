using ISTUDIO.Domain.Models;
namespace ISTUDIO.Application.Features.Authentication.DTOs;

public class RefreshJWTResponseDTO
{
    public UserSessions UserSession { get; set; }
}
