using ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;

namespace ISTUDIO.Contracts.Features.UserManagement;

/// <summary>
/// Модель для создания пользователя.
/// </summary>
public class CreateUserVM : IMapWith<CreateUserCommand>
{
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    [Required(ErrorMessage = "UserName is required.")]
    public string UserName { get; set; }

    /// <summary>
    /// Номер телефона пользователя.
    /// </summary>
    [Required(ErrorMessage = "PhoneNumber is required.")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    /// <summary>
    /// Пользователь согласен с политикой конфиденциальности.
    /// </summary>
    public bool HasAgreedToPrivacyPolicy { get; set; }

    /// <summary>
    /// Пользователь согласен с пользовательским соглашением.
    /// </summary>
    public bool ConsentToTheUserAgreement { get; set; }

    /// <summary>
    /// Список ролей пользователя.
    /// </summary>
    public List<string> Roles { get; set; }

    /// <summary>
    /// Конфигурация маппинга между CreateUserVM и CreateUserCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserVM, CreateUserCommand>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
    }
}