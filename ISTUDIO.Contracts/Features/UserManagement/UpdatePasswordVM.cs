using ISTUDIO.Application.Features.UserManagement.Commands.UpdatePassword;

namespace ISTUDIO.Contracts.Features.UserManagement;

/// <summary>
/// Модель для обновления пароля пользователя.
/// </summary>
public class UpdatePasswordVM : IMapWith<UpdatePasswordCommand>
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    [Required(ErrorMessage = "UserId is required.")]
    public string UserId { get; set; }

    /// <summary>
    /// Текущий пароль пользователя.
    /// </summary>
    [Required(ErrorMessage = "OldPassword is required.")]
    public string OldPassword { get; set; }

    /// <summary>
    /// Новый пароль пользователя.
    /// </summary>
    [Required(ErrorMessage = "NewPassword is required.")]
    public string NewPassword { get; set; }

    /// <summary>
    /// Конфигурация маппинга между UpdatePasswordVM и UpdatePasswordCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdatePasswordVM, UpdatePasswordCommand>();
    }
}