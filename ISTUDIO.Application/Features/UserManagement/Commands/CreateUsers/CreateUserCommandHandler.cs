namespace ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;
using ISTUDIO.Domain.EntityModel;

using ResModel = Result;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResModel>
{
    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _appDbContext;
    public CreateUserCommandHandler(IIdentityService identityService, IAppDbContext appDbContext) =>
                            (_identityService, _appDbContext) = (identityService, appDbContext);

    public async Task<ResModel> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _identityService.CreateUserAsync(command.UserName!, command.Email!, command.Password!);

            if (!result.Result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Result.Errors);
                throw new Exception($"Unable to create {command.UserName}.{Environment.NewLine}{errors}");
            }

            await _appDbContext.SaveChangesAsync(cancellationToken);

            var addUserToRole = await _identityService.AddToRolesAsync(result.UserId, command.Roles!);
            if (!addUserToRole.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Result.Errors);
                throw new Exception($"Unable to add {command.UserName} to assigned role/s.{Environment.NewLine}{errors}");
            }
            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}