namespace ISTUDIO.Domain.Models;

public class UserSessions
{
    public string? UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
