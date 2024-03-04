using ISTUDIO.Domain.Models;

namespace ISTUDIO.Application.Common.Interfaces;

public interface IAppUserService
{
    Task<string?> GetUserNameAsync(string userId);
    Task<UserDetails> GetUserDetailsByUserIdAsync(string userId);
    Task<UserDetails> GetUserDetailsByUserNameAsync(string userName);
    Task<UserDetails> GetUserDetailsByEmailAsync(string userName);

    Task UpdateUserProfile(string userId, string fullName);
}
