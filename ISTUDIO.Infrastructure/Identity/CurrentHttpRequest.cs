
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace ISTUDIO.Infrastructure.Identity;

public class CurrentHttpRequest : ICurrentHttpRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentHttpRequest(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    public string? GetUserId()
    {
        return _httpContextAccessor.HttpContext!.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
    }
}
