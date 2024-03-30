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
            var result = await _identityService.UpdateUserProfile(command.UserId!, command.PhoneNumber!, command.Email!);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new BadRequestException($"Unable to edit {command.PhoneNumber}.{Environment.NewLine}{errors}");
            }

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { $" Errors {ex.Message} {ex.InnerException?.Message}"});
        }
    }
}