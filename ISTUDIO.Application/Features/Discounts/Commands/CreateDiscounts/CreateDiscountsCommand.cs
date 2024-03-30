using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Discounts.Commands.CreateDiscounts;

public class CreateDiscountsCommand : IRequest<Result>, IMapWith<DiscountEntity>
{
    public decimal PercenTage { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateDiscountsCommand, DiscountEntity>();
    }
}
