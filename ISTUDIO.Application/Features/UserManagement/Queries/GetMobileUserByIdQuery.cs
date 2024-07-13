using ISTUDIO.Application.Features.UserManagement.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Application.Features.UserManagement.Queries;
using ResModel = MobileUsersDTO;

public class GetMobileUserByIdQuery : IRequest<ResModel>
{
    [Required]
    public string UserId { get; set; }

    public class Handler : IRequestHandler<GetMobileUserByIdQuery, ResModel>
    {
        private readonly IAppUserService _appUserService;
        public Handler(IAppUserService appUserService) => _appUserService = appUserService;
        public async Task<ResModel> Handle(GetMobileUserByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _appUserService.GetMobileDataAsync(query.UserId);

            return new ResModel
            {
                UserId = result.UserId,
                UserPhoneNumber = result.UserPhoneNumber,
                UserPhotoURL = result.UserPhotoURL
            };
        }
    }
}