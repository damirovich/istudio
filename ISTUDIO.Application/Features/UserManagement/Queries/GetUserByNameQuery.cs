using ISTUDIO.Application.Features.UserManagement.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.UserManagement.Queries;

using ResModel = UsersDetailsResponseDTO;
public class GetUserByNameQuery : IRequest<ResModel>
{
    [Required]
    public string UserName { get; set; }
    public class Handler : IRequestHandler<GetUserByNameQuery, ResModel>
    {
        private readonly IAppUserService _appUserService;
        public Handler(IAppUserService appUserService) => _appUserService = appUserService;
        public async Task<ResModel> Handle(GetUserByNameQuery query, CancellationToken cancellationToken)
        {
            var result = await _appUserService.GetUserDetailsByUserNameAsync(query.UserName);

            return new ResModel { AppUsers = result };
        }
    }
}
