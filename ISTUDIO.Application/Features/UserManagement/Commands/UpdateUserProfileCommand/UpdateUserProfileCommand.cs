namespace ISTUDIO.Application.Features.UserManagement.Commands.UpdateUserProfileCommand;

public class UpdateUserProfileCommand : IRequest
{
    public string? UserId { get; set; }
    public string? FullName { get; set; }
}
public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand>
{
    private readonly IAppUserService _userService;

    public UpdateUserProfileCommandHandler(IAppUserService userService)
    {
        _userService = userService;
    }
    public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        await _userService.UpdateUserProfile(request.UserId!, request.FullName!);
    }
}
