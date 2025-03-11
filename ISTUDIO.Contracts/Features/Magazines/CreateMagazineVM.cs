using ISTUDIO.Application.Features.Magazines.Commands.CreateMagazines;

namespace ISTUDIO.Contracts.Features.Magazines;

/// <summary>
/// Модель для создания магазина.
/// </summary>
public class CreateMagazineVM : IMapWith<CreateMagazinesCommand>
{
    /// <summary>
    /// Название магазина.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание магазина.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Адрес магазина.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Контактный номер телефона магазина.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Логотип магазина в формате Base64.
    /// </summary>
    public string PhotoLogoBase64 { get; set; }

    /// <summary>
    /// Идентификатор пользователя (владельца магазина).
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Конфигурация маппинга между CreateMagazineVM и CreateMagazinesCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateMagazineVM, CreateMagazinesCommand>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
               .ForMember(dest => dest.PhotoLogoURL, opt => opt.MapFrom(src => Convert.FromBase64String(src.PhotoLogoBase64)))
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}