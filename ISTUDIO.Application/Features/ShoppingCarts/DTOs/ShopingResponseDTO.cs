using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.ShoppingCarts.DTOs;

public class ShopingResponseDTO// : IMapWith<ShoppingCartEntity>
{
    public string UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalQuantyProduct { get; set; }
    public IList<ShoppingMagazineDTO> MagazineProducts { get; set; } // Изменяем на связку продукта с журналом

    //public void Mapping(Profile profile)
    //{
    //    profile.CreateMap<ShoppingCartEntity, ShopingResponseDTO>()
    //      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
    //      .ForMember(dest => dest.TotalQuantyProduct, opt => opt.MapFrom(src => src.Products.Sum(p => p.ShoppingCarts.FirstOrDefault().QuantyProduct)))
    //      .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Products.Sum(p => p.Price * p.ShoppingCarts.FirstOrDefault().QuantyProduct)))
    //      .ForMember(dest => dest.MagazineProducts, opt => opt.MapFrom(src => src.Products));
    //}
}
