namespace ISTUDIO.Application.Features.Authentication.Commands.CreateUsers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.CreateUserAsync(request.FullName!, request.UserName!, request.Email!, request.Password!);

        if (!result.Result.Succeeded)
        {
            var errors = string.Join(Environment.NewLine, result.Result.Errors);

            throw new Exception($"Unable to create {request.UserName}.{Environment.NewLine}{errors}");
        }

        var addUserToRole = await _identityService.AddToRolesAsync(result.UserId, request.Roles!);
        if (addUserToRole == null)
        {
            var errors = string.Join(Environment.NewLine, result.Result.Errors);
            throw new Exception($"Unable to add {request.UserName} to assigned role/s.{Environment.NewLine}{errors}");
        }
        return result.UserId;
    }
}