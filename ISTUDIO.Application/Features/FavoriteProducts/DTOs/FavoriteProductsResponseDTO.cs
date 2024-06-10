using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.FavoriteProducts.DTOs;

public class FavoriteProductsResponseDTO : IMapWith<FavoriteProductsEntity>
{
    public int Id { get; set; }
    public string UserId { get; set; }

    public IList<ProductsFavoriteDTO> Products { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FavoriteProductsEntity, FavoriteProductsResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
    }
}
