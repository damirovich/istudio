using ISTUDIO.Application.Features.Authentication.Commands.AuthUsers;

namespace ISTUDIO.Contracts.Features.Authentication.Authorizations;

/// <summary>
/// Модель запроса для аутентификации пользователя
/// </summary>
public class LoginVM : IMapWith<AuthUserCommand>
{
    /// <summary>
    /// Логин пользователя (Имя пользователя)
    /// </summary>
    [Required]
    public string UserName { get; set; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// Метод маппинга LoginVM в команду AuthUserCommand
    /// </summary>
    /// <param name="profile">Профиль AutoMapper</param>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginVM, AuthUserCommand>()
            .ForMember(u => u.UserName, x => x.MapFrom(c => c.UserName))
            .ForMember(u => u.Password, x => x.MapFrom(c => c.Password));
    }
}
