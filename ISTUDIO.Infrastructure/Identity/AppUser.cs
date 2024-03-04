
using Microsoft.AspNetCore.Identity;

namespace ISTUDIO.Infrastructure.Identity;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? PIN { get; set; }
    public string? SeriesDocument { get; set; }
    public string? NumDocument { get; set; }
    public string? Address { get; set; } 
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public ICollection<FamilyUserEntity> FamilyUsers { get; set; } = new List<FamilyUserEntity>();
    public ICollection<UserImagesEntity> UserImages { get; set; } = new List<UserImagesEntity>();
  
}
