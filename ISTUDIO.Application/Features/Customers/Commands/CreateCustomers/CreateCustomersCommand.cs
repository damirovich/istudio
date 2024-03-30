using ISTUDIO.Application.Features.ModelsDTO;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Customers.Commands.CreateCustomers;

public class CreateCustomersCommand : IRequest<Result>, IMapWith<CustomersEntity>
{
    public string PIN { get; set; }
    public string? FullName { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Sex { get; set; }
    public string? Nationality { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? SeriesNumDocument { get; set; }
    public DateTime? DateOfExpiry { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? Authority { get; set; }
    public DateTime? DateOfIssue { get; set; }
    public string? Ethnicity { get; set; }
    public string? Address { get; set; }
    public string? UserId { get; set; }
    public ICollection<CustomerImagesDTO> CustomerImages { get; set; } = new List<CustomerImagesDTO>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCustomersCommand, CustomersEntity>()
            .ForMember(dest => dest.PIN, opt => opt.MapFrom(src => src.PIN))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.Patronymic))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.SeriesNumDocument, opt => opt.MapFrom(src => src.SeriesNumDocument))
            .ForMember(dest => dest.DateOfExpiry, opt => opt.MapFrom(src => src.DateOfExpiry))
            .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.PlaceOfBirth))
            .ForMember(dest => dest.Authority, opt => opt.MapFrom(src => src.Authority))
            .ForMember(dest => dest.DateOfIssue, opt => opt.MapFrom(src => src.DateOfIssue))
            .ForMember(dest => dest.Ethnicity, opt => opt.MapFrom(src => src.Ethnicity))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CustomerImages, opt => opt.MapFrom(src => src.CustomerImages))
            .ForMember(dest => dest.FamilyCustomers, opt => opt.Ignore());
    }
}