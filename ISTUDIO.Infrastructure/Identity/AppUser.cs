
using Microsoft.AspNetCore.Identity;

namespace ISTUDIO.Infrastructure.Identity;

public class AppUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public int? CodeOTP { get; set; }
    //public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? PhotoUsersUrl { get; set; }
    public bool? HasAgreedToPrivacyPolicy { get; set; }
    public bool? ConsentToTheUserAgreement { get; set; }
}
