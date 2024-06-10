namespace ISTUDIO.Web.UI.Features.Banners.Commands;

using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class UIDeleteBannerCommand : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int BannerId { get; set; }
    public class Handler : IRequestHandler<UIDeleteBannerCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient apiClient) => _apiClient = apiClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIDeleteBannerCommand command, CancellationToken cancellationToken)
        {
            var res = await _apiClient.DeleteJsonAsync<ResModel>($"Banners/DeleteBanner?id={command.BannerId}");
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
