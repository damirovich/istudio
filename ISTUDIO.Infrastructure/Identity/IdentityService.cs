using ISTUDIO.Application.Common.Exceptions;
using ISTUDIO.Infrastructure.AppDbContext;
using Microsoft.AspNetCore.Identity;

namespace ISTUDIO.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;    
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ApplicationDbContext _appDbContext;
    public IdentityService(
         UserManager<AppUser> userManager,
         SignInManager<AppUser> signInManager,
         ApplicationDbContext appDbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _appDbContext = appDbContext;
    }


    // Добавляет указанного пользователя к указанным ролям.
    
    public async Task<Result> AddToRolesAsync(string userId, List<string> roles)
    {
        try
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return Result.Failure(new List<string> { "User not found" });
            }

            var errors = new List<string>();

            foreach (var roleName in roles)
            {
                var role = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
                if (role == null)
                {
                    errors.Add($"Role '{roleName}' not found");
                    continue;
                }

                var userRole = new IdentityUserRole<string> { UserId = userId, RoleId = role.Id };
                _appDbContext.UserRoles.Add(userRole);
                await _appDbContext.SaveChangesAsync();
            }

            return errors.Any() ? Result.Failure(errors) : Result.Success();
        }
        catch (Exception ex)
        {
            // Здесь вы можете выполнить логирование исключения или любые другие необходимые действия
            return Result.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }

    //Проверяет, принадлежит ли пользователь определенной роли
    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    //Пытается аутентифицировать пользователя на основе предоставленного имени пользователя и пароля.
    public async Task<bool> AuthenticateAsync(string userName, string password)
    {
        var user = await _appDbContext.AppUsers.FirstOrDefaultAsync(u => u.UserName == userName);

        // Если пользователь не найден, возвращаем false
        if (user == null)
        {
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, true, false);

        return result.Succeeded;
    }

    //Создание нового пользователя
    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string phoneNumber, string email, string password,
                                                                      bool hasAgreedToPrivacyPolicy, bool consentToTheUserAgreement)
    {
        var user = new AppUser
        {
            UserName = userName,
            Email = email,
            PhoneNumber = phoneNumber,
            LockoutEnabled = true,
            HasAgreedToPrivacyPolicy = hasAgreedToPrivacyPolicy,
            ConsentToTheUserAgreement = consentToTheUserAgreement
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }
    //Регистрация пользователя
    public async Task<(Result Result, string UserId)> CreateUserMoblieAsync(string userName, string phoneNumber, int codeOTP,
                                                                            bool hasAgreedToPrivacyPolicy, bool consentToTheUserAgreement)
    {
        var user = new AppUser
        {
            UserName = userName,
            PhoneNumber = phoneNumber,
            CodeOTP = codeOTP,
            LockoutEnabled = true, 
            HasAgreedToPrivacyPolicy = hasAgreedToPrivacyPolicy,
            ConsentToTheUserAgreement = consentToTheUserAgreement
        };
        
        _appDbContext.Add(user);
        await _appDbContext.SaveChangesAsync();

        return (Result.Success(), user.Id);
        
    }

    //Удаляет пользователя по его идентификатору Id.
    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(AppUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }


    // Обновляет пароль пользователя.
    public async Task<Result> UpdatePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new NotFoundException("User not found");

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        return result.ToApplicationResult();
    }


    //Обновляет информацию о токене обновления и времени его истечения для пользователя.
    public async Task<Result> UpdateTokenUsers(string userId, string refreshToken, DateTime refreshTokenTime)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
            throw new NotFoundException("User not found");
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = refreshTokenTime;

        _appDbContext.Users.Update(user);
        //var result = await _userManager.UpdateAsync(user);
        await _appDbContext.SaveChangesAsync();

        return Result.Success(); //result.ToApplicationResult();
    }
    // Обновление одноразового пароля 
    public async Task<(Result Result, string UserId)> UpdateUserOTP(string userId, int codeOTP) 
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
            throw new NotFoundException("User not found");
        user.CodeOTP = codeOTP;

        _appDbContext.Update(user);
        await _appDbContext.SaveChangesAsync();
        return (Result.Success(), user.Id);
    }
    public async Task<Result> UpdateUserProfile(string userId, string phoneNumber, string email)
    {
        try
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            user.UserName = phoneNumber;
            user.PhoneNumber = phoneNumber;
            user.Email = email;

            await _appDbContext.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            // Здесь вы можете выполнить логирование исключения или любые другие необходимые действия
            return Result.Failure(new[] { $"An error occurred while updating user profile. {ex.Message}" });
        }
    }

    public async Task<Result> UpdateUserPhotoProfile(string photoUrl, string userId)
    {
        try
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            user.PhotoUsersUrl = photoUrl;

            await _appDbContext.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            // Здесь вы можете выполнить логирование исключения или любые другие необходимые действия
            return Result.Failure(new[] { $"An error occurred while updating user profile Photo. {ex.Message}" });
        }
    }

    public async Task<List<string>> GetUserPermissionsAsync(string userId)
    {
        var userRoles = await _appDbContext.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId)
            .ToListAsync();

        var permissions = await _appDbContext.RolePermissions
            .Where(rp => userRoles.Contains(rp.RoleId))
            .Select(rp => rp.Permission.Name)
            .Distinct()
            .ToListAsync();

        return permissions;
    }

}
