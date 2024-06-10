namespace ISTUDIO.Application.Features.ModelsDTO;

public class AppUsersDTO
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string? UserPhoneNumber { get; set; }
    public string Email { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public IList<string> Roles { get; set; }
}
