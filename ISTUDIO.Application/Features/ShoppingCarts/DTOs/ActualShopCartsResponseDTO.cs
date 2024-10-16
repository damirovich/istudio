using ISTUDIO.Application.Features.ModelsDTO;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.ShoppingCarts.DTOs;

public class ActualShopCartsResponseDTO : IMapWith<ShoppingCartEntity>
{
    public string UserPhoneNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalQuantyProduct { get; set; }
    public DateTime? DateCreated { get; set; }
    public MagazineDTO? Magazines { get; set; }
    public IList<ProductsShoppinDTO> Products { get; set; }     
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ShoppingCartEntity, ActualShopCartsResponseDTO>()
          .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
          .ForMember(dest => dest.TotalQuantyProduct, opt => opt.MapFrom(src => src.Products.Sum(p => p.ShoppingCarts.FirstOrDefault().QuantyProduct)))
          .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Products.Sum(p => p.Price * p.ShoppingCarts.FirstOrDefault().QuantyProduct)))
          .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.CreateDate))
          .ForMember(dest => dest.Magazines, opt => opt.MapFrom(src => src.Products.Select(m => m.Magazine)));
    }
}
