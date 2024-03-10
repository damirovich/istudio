using ISTUDIO.Application.Features.UserManagement.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.UserManagement.Queries;
using ResModel = UsersDetailsResponseDTO;
public class GetUserByPhoneNumberQuery : IRequest<ResModel>
{
    [Required]
    public string PhoneNumber { get; set; }

    public class Handler : IRequestHandler<GetUserByPhoneNumberQuery, ResModel>
    {
        private readonly IAppUserService _appUserService;
        public Handler(IAppUserService appUserService) => _appUserService = appUserService;
        public async Task<ResModel> Handle(GetUserByPhoneNumberQuery query, CancellationToken cancellationToken)
        {
            var result = await _appUserService.GetUserDetailsByPhoneNumber(query.PhoneNumber);

            return new ResModel { AppUsers = result };
        }
    }
}