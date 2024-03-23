using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Ardalis.GuardClauses;

namespace ISTUDIO.Infrastructure.Identity;

public class JwtUtils : IJwtUtils
{
    private readonly IConfiguration _configProvider;
    public JwtUtils(IConfiguration configProvider)
    {
        _configProvider = configProvider;
    }
    public async Task<string> GenerateToken(string userId, string userName, IList<string> roles)
    {
        // Получение настроек JWT из конфигурации
        var jwtSettings = _configProvider.GetSection("JwtOptions");
        Guard.Against.Null(jwtSettings, message: "JwtOptions not found.");

        // Извлечение ключа, издателя, аудитории и времени истечения из настроек JWT
        var key = Guard.Against.NullOrEmpty(jwtSettings["Secret"], message: "'Secret' not found or empty.");
        var issuer = Guard.Against.NullOrEmpty(jwtSettings["Issuer"], message: "'Issuer' not found or empty.");
        var audience = Guard.Against.NullOrEmpty(jwtSettings["Audience"], message: "'Audience' not found or empty.");
        var expirMinutes = Guard.Against.NullOrEmpty(jwtSettings["expiryInMinutes"], message: "'expiryInMinutes' not found or empty.");

        // Создание симметричного ключа безопасности и установка подписи
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        // Формирование claims для включения в токен
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, userId),
            new (ClaimTypes.NameIdentifier, userId),
            new (ClaimTypes.Name, userName!),
            new Claim("UserId", userId)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        // Создание JWT токена с Claims, audience, утверждений и времени истечения
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expirMinutes)),
            signingCredentials: signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
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
