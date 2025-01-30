using ISTUDIO.Application.Features.ModelsDTO;
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Products.DTOs;

public class ProductsResponseDTO : IMapWith<ProductsEntity>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string Description { get; set; }
    public ICollection<ProductImagesDTO> Images { get; set; }
    public ProductDiscountDTO ProductDiscount { get; set; }
    public ProductCashbacksDTO ProductCashbacks { get; set; }
    public int ProductCategory { get; set; }
    public bool IsActive { get; set; }
    public decimal CashbackSum { get; set; }
    public decimal MaxBonusPercent { get; set; }
    public decimal MaxBonusSum { get; set; } // Максимальная сумма бонуса
    public MagazineDTO?  ProductMagazine { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductsEntity, ProductsResponseDTO>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
             .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
             .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
             .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
             .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
             .ForMember(dest => dest.ProductDiscount, opt => opt.MapFrom(src => src.Discount))
             .ForMember(dest=> dest.ProductCashbacks, opt=>opt.MapFrom(src=>src.Cashback))
             .ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.CategoryId))
             .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
             .ForMember(dest => dest.MaxBonusPercent, opt => opt.MapFrom(src =>
                                                         src.ProductCashbacks
                                                        .Where(pc => pc.ProductId == src.Id && pc.MaxBonusPercent != null)
                                                        .DefaultIfEmpty()
                                                        .Max(pc => pc.MaxBonusPercent)))
              .ForMember(dest => dest.CashbackSum, opt => opt.MapFrom(src =>
                                                        src.Cashback != null && src.Cashback.IsActive
                                                        ? (src.Price * src.Cashback.CashbackPercent) / 100
                                                        : 0)) // Рассчитать сумму кэшбэка только если активен
              .ForMember(dest => dest.MaxBonusSum, opt => opt.MapFrom(src =>
                                                        src.ProductCashbacks.Any()
                                                        ? (src.Price * src.ProductCashbacks.Max(pc => pc.MaxBonusPercent)) / 100
                                                        : 0)) // Рассчитать максимальную сумму бонуса который пользователь может использовать 
             .ForMember(dest => dest.ProductMagazine, opt => opt.MapFrom(src => src.Magazine));

    }
}
