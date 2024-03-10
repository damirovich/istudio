namespace ISTUDIO.Application.Features.UserManagement.Commands.UpdatePassword;
using ResModel = Result;
public class UpdatePasswordCommand : IRequest<ResModel>
{
    public string? UserId { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}
public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, ResModel>
{
    private readonly IIdentityService _identityService;
    public UpdatePasswordCommandHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task<ResModel> Handle(UpdatePasswordCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _identityService.UpdatePasswordAsync(command.UserId!, command.OldPassword!, command.NewPassword!);

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors);

                throw new BadRequestException($"Unable to update Password  {Environment.NewLine}{errors}");
            }

            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}
