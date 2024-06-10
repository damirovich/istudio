namespace ISTUDIO.Web.UI.Features.Orders.Commands;
using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class UIDeleteOrdersCommand : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int OrderId { get; set; }
    public class Handler : IRequestHandler<UIDeleteOrdersCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient apiClient) => _apiClient = apiClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIDeleteOrdersCommand command, CancellationToken cancellationToken)
        {
            var res = await _apiClient.DeleteJsonAsync<ResModel>($"Orders/DeleteOrdes?orderId={command.OrderId}");
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