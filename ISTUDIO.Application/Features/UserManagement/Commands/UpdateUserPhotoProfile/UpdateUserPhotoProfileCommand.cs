
namespace ISTUDIO.Application.Features.UserManagement.Commands.UpdateUserPhotoProfile;
using ResModel = Result;
public class UpdateUserPhotoProfileCommand : IRequest<ResModel>
{
    public string UserId { get; set; }
    public string PhotoUrl { get; set; }
    public class Handler : IRequestHandler<UpdateUserPhotoProfileCommand, ResModel>
    {
        private readonly IIdentityService _identityService;
        public Handler(IIdentityService identityService) =>
                                (_identityService) = (identityService);
        public async Task<ResModel> Handle(UpdateUserPhotoProfileCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _identityService.UpdateUserPhotoProfile(command.PhotoUrl, command.UserId);

                if (!result.Succeeded)
                {
                    var errors = string.Join(Environment.NewLine, result.Errors);
                    throw new Exception($"Unable to update Photo User {command.UserId}.{Environment.NewLine}{errors}");
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
