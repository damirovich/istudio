using ISTUDIO.Domain.EntityModel;
using System.Text.Json.Serialization;

namespace ISTUDIO.Application.Features.ShoppingCarts.DTOs;

public class ShopingResponseDTO : IMapWith<ShoppingCartEntity>
{
    public string UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalQuantyProduct { get; set; }
    public IList<ProductsShoppinDTO> Products { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ShoppingCartEntity, ShopingResponseDTO>()
          .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
          .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
          .ForMember(dest => dest.TotalQuantyProduct, opt => opt.MapFrom(src => src.Products.Sum(p => p.ShoppingCarts.FirstOrDefault().QuantyProduct)))
          .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Products.Sum(p => p.Price * p.ShoppingCarts.FirstOrDefault().QuantyProduct)));
    }
}
