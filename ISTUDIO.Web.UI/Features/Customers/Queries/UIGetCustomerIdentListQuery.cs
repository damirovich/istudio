using ISTUDIO.Application.Features.Customers.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.UI.Features.Customers.Queries;
using ResModel = PaginatedList<GroupedCustomerImagesResponseDTO>;
public class UIGetCustomerIdentListQuery : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public int PageSize { get; set; }
    public class Handler : IRequestHandler<UIGetCustomerIdentListQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIGetCustomerIdentListQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Customers/GetCustomerIdentListQuery?pageNumber={request.PageNumber}&pageSize={request.PageSize}");
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
