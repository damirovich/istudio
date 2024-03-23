using System.Security.Claims;

namespace ISTUDIO.Application.Common.Interfaces;

public interface IJwtUtils
{
    Task<string> GenerateToken(string userId, string userName, IList<string> roles);
    ClaimsPrincipal ValidateToken(string token);
    Task<string> GenerateRefreshToken();
    Task<bool> IsTokenExpired(string token);
}
