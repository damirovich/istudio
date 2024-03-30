using ISTUDIO.Application.Features.ShoppingCarts.Commands.CreateShoppingCarts;

namespace ISTUDIO.Contracts.Features.ShoppingsCarts;

public class AddProductCartsVM : IMapWith<AddProductToCartsCommand>
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public int ProductId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddProductCartsVM, AddProductToCartsCommand>();
    }
}
