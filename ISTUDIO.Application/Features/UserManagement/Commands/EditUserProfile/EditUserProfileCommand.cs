namespace ISTUDIO.Application.Features.UserManagement.Commands.EditUserProfile;

public class EditUserProfileCommand : IRequest<Result>
{
    public string? UserId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

}

