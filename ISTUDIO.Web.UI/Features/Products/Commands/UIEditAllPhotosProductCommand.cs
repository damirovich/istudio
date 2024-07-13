namespace ISTUDIO.Web.UI.Features.Products.Commands;

using ISTUDIO.Contracts.Features.Products;
using ResModel = Result;
public class UIEditAllPhotosProductCommand : IRequest<ResponseAPI<ResModel>>
{
    public EditAllPhotosProductVM ProductImages { get; set; }

    public class Handler : IRequestHandler<UIEditAllPhotosProductCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIEditAllPhotosProductCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PutJsonAsync<ResModel, EditAllPhotosProductVM>(request.ProductImages, $"ProductImages/EditAllProductPhotos");

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
