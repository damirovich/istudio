namespace ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;

using ResModel = Result;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResModel>
{
    private readonly IIdentityService _identityService;
    public CreateUserCommandHandler(IIdentityService identityService) =>
                            (_identityService) = (identityService);

    public async Task<ResModel> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _identityService.CreateUserAsync(command.UserName!, command.PhoneNumber!, command.Email!, command.Password!, 
                                                                command.HasAgreedToPrivacyPolicy, command.ConsentToTheUserAgreement);

            if (!result.Result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Result.Errors);
                throw new Exception($"Unable to create {command.PhoneNumber}.{Environment.NewLine}{errors}");
            }

            var addUserToRole = await _identityService.AddToRolesAsync(result.UserId, command.Roles!);
            if (!addUserToRole.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Result.Errors);
                throw new Exception($"Unable to add {command.PhoneNumber} to assigned role/s.{Environment.NewLine}{errors}");
            }
            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}