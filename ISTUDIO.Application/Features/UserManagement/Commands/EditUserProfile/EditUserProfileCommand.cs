namespace ISTUDIO.Application.Features.UserManagement.Commands.EditUserProfile;

public class EditUserProfileCommand : IRequest<Result>
{
    public string? UserId { get; set; }

    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
   
    public List<string>? Roles { get; set; } = new List<string>() { "MobileUser" };
}

