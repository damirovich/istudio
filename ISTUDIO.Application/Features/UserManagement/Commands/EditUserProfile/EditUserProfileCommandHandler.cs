namespace ISTUDIO.Application.Features.UserManagement.Commands.EditUserProfile;

using ResModel = Result;
public class EditUserProfileCommandHandler : IRequestHandler<EditUserProfileCommand, ResModel>
{
    private readonly IIdentityService _identityService;

    public EditUserProfileCommandHandler(IIdentityService identityService) => _identityService = identityService;
    public async Task<ResModel> Handle(EditUserProfileCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _identityService.UpdateUserProfile(command.UserId!, command.FullName!, command.UserName!, command.Email!);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);

                throw new BadRequestException($"Unable to edit {command.UserName}.{Environment.NewLine}{errors}");
            }
            var editUserToRole = await _identityService.AddToRolesAsync(command.UserId, command.Roles!);
            if (editUserToRole == null)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new BadRequestException($"Unable to add {command.UserName} to assigned role/s.{Environment.NewLine}{errors}");
            }
            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}