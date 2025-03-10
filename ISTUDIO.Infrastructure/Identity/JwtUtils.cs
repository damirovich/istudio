using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Ardalis.GuardClauses;
using ISTUDIO.Application.Common.Interfaces;

namespace ISTUDIO.Infrastructure.Identity;

public class JwtUtils : IJwtUtils
{
    private readonly IConfiguration _configProvider;
    private readonly IIdentityService _identityService;
    public JwtUtils(IConfiguration configProvider, IIdentityService identityService)
    {
        _configProvider = configProvider;
        _identityService = identityService;
    }
    public async Task<string> GenerateToken(string userId, string userName, IList<string> roles)
    {
        var jwtSettings = _configProvider.GetSection("JwtOptions");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
        var expirationTime = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["expiryInMinutes"]));

        // Получаем Permissions пользователя
        var permissions = await _identityService.GetUserPermissionsAsync(userId);

        var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub, userName),
                    new(JwtRegisteredClaimNames.Jti, userId),
                    new(ClaimTypes.NameIdentifier, userId),
                    new(ClaimTypes.Name, userName)
                };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        claims.AddRange(permissions.Select(permission => new Claim("permission", permission.ToString())));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            Subject = new ClaimsIdentity(claims),
            Expires = expirationTime,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        // Получение настроек JWT из конфигурации
        var jwtSettings = _configProvider.GetSection("JwtOptions");
        Guard.Against.Null(jwtSettings, message: "JwtOptions not found.");

        // Извлечение ключа из настроек JWT
        var key = Guard.Against.NullOrEmpty(jwtSettings["Secret"], message: "'Secret' not found or empty.");

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false,

            ClockSkew = TimeSpan.FromMinutes(5)// Небольшой запас времени для обновление токена
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    public async Task<string> GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    //Проверка время жизни Access Token 
    public async Task<bool> IsTokenExpired(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        if (handler.CanReadToken(accessToken))
        {
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

            return jsonToken?.ValidTo != null && jsonToken.ValidTo < DateTime.UtcNow;
        }

        // Если токен не валиден, возможно, он отсутствует или имеет неверный формат
        return true;
    }
}
