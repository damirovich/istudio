using ISTUDIO.Application.Features.Discounts.Commands.EditDiscounts;

namespace ISTUDIO.Contracts.Features.Discounts;

public class EditDiscountVM : IMapWith<EditDiscountsCommand>
{
    public int Id { get; set; }
    public decimal PercenTage { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditDiscountVM, EditDiscountsCommand>();
    }
}
