namespace ISTUDIO.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result> AddToRolesAsync(string userId, List<string> roles);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<bool> AuthenticateAsync(string username, string password);
    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string phoneNumber, string email, string password);
    Task<(Result Result, string UserId)> CreateUserMoblieAsync(string userName, string PhoneNumber, int codeOTP);
    Task<Result> DeleteUserAsync(string userId);
    Task<Result> UpdatePasswordAsync(string userId, string oldPassword, string newPassword);
    Task<Result> UpdateTokenUsers(string userId, string refreshToken, DateTime refreshTokenTime);
    Task<(Result Result, string UserId)> UpdateUserOTP(string userId, int codeOTP);

    Task<Result> UpdateUserProfile(string userId, string phoneNumber, string email);
}
