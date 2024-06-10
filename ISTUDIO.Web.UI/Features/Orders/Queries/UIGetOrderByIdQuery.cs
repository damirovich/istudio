using ISTUDIO.Application.Features.Orders.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.UI.Features.Orders.Queries;
using ResModel = OrderResponseDTO;
public class UIGetOrderByIdQuery : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int OrderId { get; set; }

    public class Handler : IRequestHandler<UIGetOrderByIdQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient)
        {
            _apiClient = httpClient;
        }

        public async Task<ResponseAPI<ResModel>> Handle(UIGetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Orders/GetOrdersById?orderId={request.OrderId}");
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
