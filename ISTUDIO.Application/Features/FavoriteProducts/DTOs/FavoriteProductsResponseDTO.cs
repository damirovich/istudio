using ISTUDIO.Application.Features.Products.DTOs;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.FavoriteProducts.DTOs;

public class FavoriteProductsResponseDTO : IMapWith<FavoriteProductsEntity>
{
    public string UserId { get; set; }

    public IList<ProductsResponseDTO> Products { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FavoriteProductsEntity, FavoriteProductsResponseDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
    }
}
