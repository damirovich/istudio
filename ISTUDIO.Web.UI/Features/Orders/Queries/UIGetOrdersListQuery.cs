using ISTUDIO.Application.Features.Orders.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.UI.Features.Orders.Queries;
using ResModel = PaginatedList<OrderResponseDTO>;
public class UIGetOrdersListQuery : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public int PageSize { get; set; }

    public class Handler : IRequestHandler<UIGetOrdersListQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient)
        {
            _apiClient = httpClient;
        }

        public async Task<ResponseAPI<ResModel>> Handle(UIGetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Orders/GetOrdersList?PageNumber={request.PageNumber}&PageSize={request.PageSize}");
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
