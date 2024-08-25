namespace ISTUDIO.Web.UI.Features.Products.Commands;

using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class UIDeletePhotosProductCommand : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int ProductPhotoId { get; set; }

    public class Handler : IRequestHandler<UIDeletePhotosProductCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;

        public Handler(APIHttpClient apiClient) => _apiClient = apiClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIDeletePhotosProductCommand command, CancellationToken cancellationToken)
        {
            var res = await _apiClient.DeleteJsonAsync<ResModel>($"ProductImages/DeleteProductPhotos?IdPhotoProduct={command.ProductPhotoId}");
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
