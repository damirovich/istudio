
namespace ISTUDIO.Domain.EntityModel;

public class BannerEntity
{
    public int Id { get; set; }
    public string PhotoUrl { get; set; }
    public int Status { get; set; }
    public int? CategoryId { get; set; }
    public int? DiscountId { get; set; }
    public int? ProductId { get; set; }

    public CategoryEntity? Categories { get; set; }
    public DiscountEntity? Discounts { get; set; }
    public ProductsEntity Products { get; set; }
}
