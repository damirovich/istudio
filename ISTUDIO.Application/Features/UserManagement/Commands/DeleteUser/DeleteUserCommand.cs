namespace ISTUDIO.Application.Features.UserManagement.Commands.DeleteUser;

using ResModel = Result;
public class DeleteUserCommand : IRequest<ResModel>
{
    public string UserId { get; set; }

    public class Handler : IRequestHandler<DeleteUserCommand, ResModel>
    {
        private readonly IIdentityService _identityService;
        public Handler(IIdentityService identityService) => _identityService = identityService;
        public async Task<ResModel> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _identityService.DeleteUserAsync(command.UserId);

                if (!result.Succeeded)
                {
                    var errors = string.Join(Environment.NewLine, result.Errors);
                    throw new BadRequestException($"Unable to delete.{Environment.NewLine}{errors}");
                }
                return ResModel.Success();
            }
            catch (Exception ex)
            {
                return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}