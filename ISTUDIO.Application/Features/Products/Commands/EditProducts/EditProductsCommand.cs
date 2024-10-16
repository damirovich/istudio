using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Products.Commands.EditProducts;

public class EditProductsCommand : IRequest<Result>, IMapWith<ProductsEntity>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string Description { get; set; }
    public int? CategoryId { get; set; }
    public int? DiscountId { get; set; }
    public int? MagazineId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditProductsCommand, ProductsEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.MagazineId, opt => opt.MapFrom(src => src.MagazineId))
            .ForMember(dest => dest.DiscountId, opt => opt.MapFrom(src => src.DiscountId));           
    }
}
