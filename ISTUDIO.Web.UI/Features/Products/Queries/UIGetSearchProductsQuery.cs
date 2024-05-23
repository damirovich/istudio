using ISTUDIO.Application.Features.Products.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.UI.Features.Products.Queries;
using ResModel = PaginatedList<ProductsResponseDTO>;
public class UIGetSearchProductsQuery : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public string SearchTerm { get; set; }
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public int PageSize { get; set; }

    public class Handler : IRequestHandler<UIGetSearchProductsQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient)
        {
            _apiClient = httpClient;
        }

        public async Task<ResponseAPI<ResModel>> Handle(UIGetSearchProductsQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Products/GetSearchProducts?pageNumber={request.PageNumber}&pageSize={request.PageSize}&searchTerm={request.SearchTerm}");
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
