using ISTUDIO.Application.Features.ShoppingCarts.Commands.EditShoppinCarts;

namespace ISTUDIO.Contracts.Features.ShoppingsCarts;

public class ChangeQuantyProductCartVM : IMapWith<ChangeQuantyProductCartCommand>
{
    public int CartId { get; set; }
    public int QuantyProduct { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ChangeQuantyProductCartVM, ChangeQuantyProductCartCommand>();
    }
}
