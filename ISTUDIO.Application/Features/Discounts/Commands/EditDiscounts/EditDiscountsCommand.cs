
using ISTUDIO.Domain.EntityModel;

namespace ISTUDIO.Application.Features.Discounts.Commands.EditDiscounts;

public class EditDiscountsCommand : IMapWith<DiscountEntity> , IRequest<Result>
{
    public int Id { get; set; }
    public decimal PercenTage { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditDiscountsCommand, DiscountEntity>();
    }
}
