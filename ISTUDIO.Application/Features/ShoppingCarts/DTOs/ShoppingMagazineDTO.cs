using ISTUDIO.Application.Features.ModelsDTO;

namespace ISTUDIO.Application.Features.ShoppingCarts.DTOs;

public class ShoppingMagazineDTO 
{
    public MagazineDTO Magazine { get; set; }
    public  List<ProductsShoppinDTO> Products { get; set; }

}
