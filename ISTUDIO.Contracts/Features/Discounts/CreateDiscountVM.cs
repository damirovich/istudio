using ISTUDIO.Application.Features.Discounts.Commands.CreateDiscounts;

namespace ISTUDIO.Contracts.Features.Discounts;

public class CreateDiscountVM : IMapWith<CreateDiscountsCommand>
{
    public decimal PercenTage { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateDiscountVM, CreateDiscountsCommand>();
    }
}
