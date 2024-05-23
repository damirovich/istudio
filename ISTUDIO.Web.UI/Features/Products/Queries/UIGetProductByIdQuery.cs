using ISTUDIO.Application.Features.Products.DTOs;

namespace ISTUDIO.Web.UI.Features.Products.Queries;
using ResModel = ProductsResponseDTO;
public class UIGetProductByIdQuery : IRequest<ResponseAPI<ResModel>>
{
    public int ProductId { get; set; }

    public class Handler : IRequestHandler<UIGetProductByIdQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIGetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Products/GetProductsById?productId={request.ProductId}");
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
