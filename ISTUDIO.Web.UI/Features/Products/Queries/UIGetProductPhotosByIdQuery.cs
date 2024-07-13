using ISTUDIO.Application.Features.Products.DTOs;

namespace ISTUDIO.Web.UI.Features.Products.Queries;
using ResModel = List<ProductImagesDTO>;
public class UIGetProductPhotosByIdQuery : IRequest<ResponseAPI<ResModel>>
{
    public int ProductId { get; set; }

    public class Handler : IRequestHandler<UIGetProductPhotosByIdQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIGetProductPhotosByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"ProductImages/GetProductPhotosById?productId={request.ProductId}");
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
