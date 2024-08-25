using ISTUDIO.Application.Features.Orders.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.UI.Features.Orders.Queries;
using ResModel = PaginatedList<OrderResponseDTO>;
public class UIGetSearchOrdersQuery : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public string SearchTerm { get; set; }
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public int PageSize { get; set; }

    public class Handler : IRequestHandler<UIGetSearchOrdersQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;

        public Handler(APIHttpClient apiClient) => _apiClient = apiClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIGetSearchOrdersQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Orders/GetSearchOrders?pageNumber={request.PageNumber}&pageSize={request.PageSize}&searchTerm={request.SearchTerm}");
            
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
