using ISTUDIO.Application.Common.Exceptions;
using ISTUDIO.Infrastructure.AppDbContext;
using Microsoft.AspNetCore.Identity;

namespace ISTUDIO.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ApplicationDbContext _appDbContext;
    public IdentityService(
     UserManager<AppUser> userManager,
     RoleManager<IdentityRole> roleManager,
     SignInManager<AppUser> signInManager,
     ApplicationDbContext appDbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _appDbContext = appDbContext;
    }


    // Добавляет указанного пользователя к указанным ролям.
    
    public async Task<Result> AddToRolesAsync(string userId, List<string> roles)
    {
        // Получаем пользователя по его идентификатору
        var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return Result.Failure(new List<string> { "User not found" });
        }

        var errors = new List<string>();

        foreach (var roleName in roles)
        {
            // Проверяем существует ли роль
            var role = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                errors.Add($"Role '{roleName}' not found");
                continue;
            }
            // Добавляем пользователя к роли
            var userRole = new IdentityUserRole<string> { UserId = userId, RoleId = role.Id };
            _appDbContext.UserRoles.Add(userRole);
            await _appDbContext.SaveChangesAsync();
        }

        return errors.Any() ? Result.Failure(errors) : Result.Success();
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
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, true, false);

        return result.Succeeded;
    }

    //Создание нового пользователя
    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string email, string password)
    {
        var user = new AppUser
        {
            UserName = userName,
            Email = email,
            LockoutEnabled = true
        };

        //var result = await _userManager.CreateAsync(user, password);
        _appDbContext.Add(user);
        await _appDbContext.SaveChangesAsync();

        return (Result.Success(), user.Id);
    }
    //Регистрация пользователя
    public async Task<(Result Result, string UserId)> CreateUserMoblieAsync(string userName, string phoneNumber, int codeOTP)
    {
        var user = new AppUser
        {
            UserName = userName,
            PhoneNumber = phoneNumber,
            CodeOTP = codeOTP,
            LockoutEnabled = true
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
    public async Task<Result> UpdateUserProfile(string userId, string userName, string email)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
            throw new NotFoundException("User not found");

        user.UserName = userName;
        user.Email = email;

        var result = await _userManager.UpdateAsync(user);

        return result.ToApplicationResult();
    }
}
