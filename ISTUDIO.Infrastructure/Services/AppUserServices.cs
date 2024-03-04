using Ardalis.GuardClauses;
using ISTUDIO.Domain.Models;
using ISTUDIO.Infrastructure.AppDbContext;
using ISTUDIO.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace ISTUDIO.Infrastructure.Services;

public class AppUserServices : IAppUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public AppUserServices(UserManager<AppUser> userManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<UserDetails> GetUserDetailsByEmailAsync(string email)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            throw new Application.Common.Exceptions.NotFoundException("User not found");
        }
        var roles = await _userManager.GetRolesAsync(user);

        return new UserDetails() { UserId = user.Id!, FullName = user.FullName!, UserName = user.UserName!, Email = user.Email!, Roles = roles };
    }

    public async Task<UserDetails> GetUserDetailsByUserIdAsync(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new Application.Common.Exceptions.NotFoundException("User not found");
        }
        var roles = await _userManager.GetRolesAsync(user);

        return new UserDetails() { UserId = user.Id!, FullName = user.FullName!, UserName = user.UserName!, Email = user.Email!, Roles = roles };
    }

    public async Task<UserDetails> GetUserDetailsByUserNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null)
        {
            throw new Application.Common.Exceptions.NotFoundException("User not found");
        }
        var roles = await _userManager.GetRolesAsync(user);

        return new UserDetails()
        {
            UserId = user.Id!,
            FullName = user.FullName!,
            UserName = user.UserName!,
            Email = user.Email!,
            RefreshToken = user.RefreshToken,
            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
            Roles = roles
        };
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task UpdateUserProfile(string userId, string fullName)
    {
        var entity = await _dbContext.AppUsers.FindAsync(new object[] { userId! });

        Guard.Against.NotFound(userId!, entity);

        entity.FullName = fullName;

        await _dbContext.SaveChangesAsync();
    }
}
