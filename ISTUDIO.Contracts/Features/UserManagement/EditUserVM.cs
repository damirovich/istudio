using ISTUDIO.Application.Features.UserManagement.Commands.EditUserProfile;

namespace ISTUDIO.Contracts.Features.UserManagement;

/// <summary>
/// Модель для редактирования профиля пользователя.
/// </summary>
public class EditUserVM : IMapWith<EditUserProfileCommand>
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    [Required(ErrorMessage = "UserId is required.")]
    public string UserId { get; set; }

    /// <summary>
    /// Новый номер телефона пользователя.
    /// </summary>
    [Required(ErrorMessage = "PhoneNumber is required.")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Новый адрес электронной почты пользователя.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }

    /// <summary>
    /// Конфигурация маппинга между EditUserVM и EditUserProfileCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditUserVM, EditUserProfileCommand>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}