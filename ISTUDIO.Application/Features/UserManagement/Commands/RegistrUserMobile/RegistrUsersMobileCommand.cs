
using ISTUDIO.Application.Features.UserManagement.DTOs;

namespace ISTUDIO.Application.Features.UserManagement.Commands.RegistrUserMobile;

using ISTUDIO.Application.Common.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

using ResModel = UserRegistrResponseDTO;
public class RegistrUsersMobileCommand : IRequest<ResModel>
{
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public int OTPCode { get; set; }

    public List<string>? Roles { get; set; } = new List<string>();

    public class Handler : IRequestHandler<RegistrUsersMobileCommand, ResModel>
    {
        public readonly IIdentityService _identityService;
        private readonly IAppUserService _appUserService;
        public Handler(IIdentityService identityService, IAppUserService appUserService) => 
            (_identityService, _appUserService )= (identityService, appUserService);

        public async Task<ResModel> Handle(RegistrUsersMobileCommand command, CancellationToken cancellationToken)
        {
            // Проверяем, существует ли пользователь с этим номером телефона
            var (userExists, userId) = await _appUserService.GetUserExistsByPhoneNumber(command.PhoneNumber);
            if (userExists) 
            {
                // Если пользователь существует, обновляем его одноразовый пароль
                var updateResult = await _identityService.UpdateUserOTP(userId, command.OTPCode);
                if (!updateResult.Result.Succeeded)
                {
                    var errors = string.Join(Environment.NewLine, updateResult.Result.Errors);
                    throw new BadRequestException($"Unable to update OTP Code for {command.PhoneNumber}.{Environment.NewLine}{errors}");
                }
                return new ResModel { PhoneNumber = command.PhoneNumber, UserId = updateResult.UserId };
            }

            // Если пользователя нет с этим номером телефона, создаем нового пользователя
            var creationResult = await _identityService.CreateUserMoblieAsync(command.PhoneNumber!, command.PhoneNumber, command.OTPCode);
            if (!creationResult.Result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, creationResult.Result.Errors);
                throw new BadRequestException($"Unable to create user with phone number {command.PhoneNumber}.{Environment.NewLine}{errors}");
            }

            // Добавляем роль для пользователя
            var addUserToRoleResult = await _identityService.AddToRolesAsync(creationResult.UserId, command.Roles!);
            if (!addUserToRoleResult.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, creationResult.Result.Errors);
                throw new BadRequestException($"Unable to add {command.PhoneNumber} to assigned role/s.{Environment.NewLine}{errors}");
            }

            return new ResModel { PhoneNumber = command.PhoneNumber, UserId = creationResult.UserId };
        }
    }
}
