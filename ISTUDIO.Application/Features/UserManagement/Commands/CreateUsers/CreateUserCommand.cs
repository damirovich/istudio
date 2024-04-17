using ISTUDIO.Application.Features.UserManagement.DTOs;

namespace ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;

public class CreateUserCommand : IRequest<Result>
{

    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public List<string>? Roles { get; set; } = new List<string>() { "MobileUser" };
    // Изображения пользователя
    public List<UserImageDTO> Images { get; set; } = new List<UserImageDTO>();

}
