using ISTUDIO.Application.Features.Magazines.Commands.EditMagazines;
using ISTUDIO.Application.Features.Magazines.DTOs;

namespace ISTUDIO.Contracts.Features.Magazines;

/// <summary>
/// Модель для редактирования информации о магазине.
/// </summary>
public class EditMagazineVM : IMapWith<EditMagazinesCommand>
{
    /// <summary>
    /// Уникальный идентификатор магазина.
    /// </summary>
    [Required(ErrorMessage = "Magazine ID is required.")]
    public int MagazineId { get; set; }

    /// <summary>
    /// Название магазина.
    /// </summary>
    [Required(ErrorMessage = "Magazine name is required.")]
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
    /// Конфигурация маппинга между EditMagazineVM и EditMagazinesCommand.
    /// </summary>
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditMagazineVM, EditMagazinesCommand>()
               .ForMember(dest => dest.MagazineId, opt => opt.MapFrom(src => src.MagazineId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
               .ForMember(dest => dest.PhotoLogoURL, opt => opt.MapFrom(src => Convert.FromBase64String(src.PhotoLogoBase64)));

        // Маппинг из DTO в VM для удобства работы с данными в UI
        profile.CreateMap<MagazinesDTO, EditMagazineVM>()
               .ForMember(dest => dest.MagazineId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
               .ForMember(dest => dest.PhotoLogoBase64, opt => opt.MapFrom(src => src.PhotoLogoURL));
    }
}