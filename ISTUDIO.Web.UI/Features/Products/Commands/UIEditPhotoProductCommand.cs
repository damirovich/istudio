namespace ISTUDIO.Web.UI.Features.Products.Commands;

using ISTUDIO.Contracts.Features.Products;
using ResModel = Result;
public class UIEditPhotoProductCommand : IRequest<ResponseAPI<ResModel>>
{
    public EditPhotoProductVM ProductPhoto { get; set; }

    public class Handler : IRequestHandler<UIEditPhotoProductCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIEditPhotoProductCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PutJsonAsync<ResModel, EditPhotoProductVM>(request.ProductPhoto, $"ProductImages/EditProductPhotos");

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