
using ISTUDIO.Application.Features.Orders.Commands.EditOrders;

namespace ISTUDIO.Contracts.Features.Orders;

public class EditOrdersVM : IMapWith<UpdateStatusOrdersCommand>
{
    public int OrderId { get; set; }
    public string OrderStatus { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditOrdersVM, UpdateStatusOrdersCommand>();
    }
}
