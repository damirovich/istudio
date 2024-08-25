namespace ISTUDIO.Web.UI.Features.Products.Commands;

using ISTUDIO.Contracts.Features.Products;
using ResModel = Result;
public class UIAddPhotosProductsCommand : IRequest<ResponseAPI<ResModel>>
{
    public AddPhotosProductsVM PhotosProducts { get; set; }
    public class Handler : IRequestHandler<UIAddPhotosProductsCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIAddPhotosProductsCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PostJsonAsync<ResModel, AddPhotosProductsVM>(request.PhotosProducts, $"ProductImages/AddProductPhotos");

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
