using ISTUDIO.Application.Features.UserManagement.Commands.UpdatePassword;

namespace ISTUDIO.Contracts.Features.UserManagement;

public class UpdatePasswordVM : IMapWith<UpdatePasswordCommand>
{
    [Required]
    public string? UserId { get; set; }
    [Required]
    public string? OldPassword { get; set; }
    [Required]
    public string? NewPassword { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdatePasswordVM, UpdatePasswordCommand>();
    }

}