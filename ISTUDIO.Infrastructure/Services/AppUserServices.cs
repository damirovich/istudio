using Ardalis.GuardClauses;
using ISTUDIO.Application.Features.ModelsDTO;
using ISTUDIO.Application.Features.UserManagement.DTOs;
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

    public async Task<List<AppUsersDTO>> GetUsersListAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        var userList = new List<AppUsersDTO>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            userList.Add(new AppUsersDTO
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserPhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Roles = roles.ToList()
            });
        }

        return userList;
    }
    public async Task<AppUsersDTO> GetUserDetailsByUserIdAsync(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new Application.Common.Exceptions.NotFoundException("User not found");
        }
        var roles = await _userManager.GetRolesAsync(user);

        return new AppUsersDTO() { UserId = user.Id!, UserName = user.UserName!, UserPhoneNumber = user.PhoneNumber!, Email = user.Email!, Roles = roles };
    }
    public async Task<AppUsersDTO> GetUserDetailsByUserNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null)
        {
            throw new Application.Common.Exceptions.NotFoundException("User not found");
        }
        var roles = await _userManager.GetRolesAsync(user);

        return new AppUsersDTO()
        {
            UserId = user.Id!,
            UserName = user.UserName!,
            UserPhoneNumber = user.PhoneNumber!,
            Email = user.Email!,
            RefreshToken = user.RefreshToken,
            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
            Roles = roles
        };
    }
    public async Task<(bool, string UserId)> GetUserExistsByPhoneNumber(string phoneNumber)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        if(user == null) 
            return (false, string.Empty);
        return (true, user.Id);
    }

    public async Task<AppUsersDTO> GetUserDetailsByPhoneNumber(string phoneNumber)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        if (user == null)
        {
            throw new Application.Common.Exceptions.NotFoundException("User not found");
        }
        var roles = await _userManager.GetRolesAsync(user);

        return new AppUsersDTO()
        {
            UserId = user.Id!,
            UserName = user.UserName!,
            UserPhoneNumber = user.PhoneNumber!,
            Email = user.Email!,
            RefreshToken = user.RefreshToken,
            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
            Roles = roles
        };
    }
    public async Task<MobileUsersDTO> GetMobileDataAsync(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
        {
            throw new Application.Common.Exceptions.NotFoundException("User not found");
        }
        var roles = await _userManager.GetRolesAsync(user);

        return new MobileUsersDTO()
        {
            UserId = user.Id!,
            UserPhoneNumber = user.PhoneNumber!,
            UserPhotoURL = user.PhotoUsersUrl!
            
        };
    }
}
