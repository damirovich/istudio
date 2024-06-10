using ISTUDIO.Application.Features.Orders.Commands.EditOrders.UpdateStatusOrders;

namespace ISTUDIO.Contracts.Features.Orders;

public class UpdateStatusOrdersVM : IMapWith<UpdateStatusOrdersCommand>
{
    public int OrderId { get; set; }
    public string OrderStatus { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateStatusOrdersVM, UpdateStatusOrdersCommand>();
    }
}
