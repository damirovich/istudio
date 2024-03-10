using ISTUDIO.Application.Features.UserManagement.DTOs;

namespace ISTUDIO.Application.Features.UserManagement.Queries;
using ResModel = UsersListResponseDTO;
public class GetUserListQuery : IRequest<ResModel>
{
    public class Handler : IRequestHandler<GetUserListQuery, ResModel>
    {
        private readonly IAppUserService _appUserService;

        public Handler(IAppUserService appUserService) =>
                       (_appUserService) = (appUserService);
        public async Task<ResModel> Handle(GetUserListQuery query, CancellationToken cancellationToken)
        {
            var users = await _appUserService.GetUsersListAsync();

            return new ResModel
            {
                AppUsers = users
            };
        }
    }
}
