
using ISTUDIO.Application.Features.UserManagement.DTOs;

namespace ISTUDIO.Application.Features.UserManagement.Queries;

using System.ComponentModel.DataAnnotations;
using ResModel = UsersDetailsResponseDTO;
public class GetUserByIdQuery : IRequest<ResModel>
{
    [Required]
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetUserByIdQuery, ResModel>
    {
        private readonly IAppUserService _appUserService;
        public Handler(IAppUserService appUserService) => _appUserService = appUserService;
        public async Task<ResModel> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _appUserService.GetUserDetailsByUserIdAsync(query.UserId);

            return new ResModel { AppUsers = result };
        }
    }
}
