namespace ISTUDIO.Web.UI.Features.Orders.Commands;

using ISTUDIO.Contracts.Features.Orders;
using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class UIUpdateStatusOrdersCommand : IRequest<ResponseAPI<ResModel>>
{
   [Required]
    public UpdateStatusOrdersVM UpStatusOrders { get; set; }  

    public class Handler : IRequestHandler<UIUpdateStatusOrdersCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient apiClient) => _apiClient = apiClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIUpdateStatusOrdersCommand command, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PutJsonAsync<ResModel, UpdateStatusOrdersVM>(command.UpStatusOrders, $"Orders/UpdateStatusOrders");
            return res.IsSuccess() ?
            new()
            {
                Status = true,
                StatusMessage = res.GetMessage(),
                Data = res.Data
            }
            :
            new()
            {
                Status = false,
                StatusMessage = res.GetMessage()
            };
        }
    }

}
