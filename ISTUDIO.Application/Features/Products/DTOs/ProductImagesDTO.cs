using ISTUDIO.Application.Features.Products.Commands.CreateProducts;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Products.DTOs;

public class ProductImagesDTO : IMapWith<ProductImagesEntity>
{
    public int? Id { get; set; }
    public string? Url { get; set; }
    public string? Name { get; set; }
    public string? ContentType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductImagesEntity, ProductImagesDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.ContentType));

        profile.CreateMap<ProductImagesDTO, ProductImagesEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Ensure Id mapping if it's needed
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.ContentType));

    }
}

