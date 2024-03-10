using ISTUDIO.Application.Features.ModelsDTO;

namespace ISTUDIO.Application.Common.Interfaces;

public interface IAppUserService
{
    Task<List<AppUsersDTO>> GetUsersListAsync();
    Task<AppUsersDTO> GetUserDetailsByUserIdAsync(string userId);
    Task<AppUsersDTO> GetUserDetailsByUserNameAsync(string userName);
    Task<(bool, string UserId)> GetUserExistsByPhoneNumber(string phoneNumber);
    Task<AppUsersDTO> GetUserDetailsByPhoneNumber(string phoneNumber);
}
