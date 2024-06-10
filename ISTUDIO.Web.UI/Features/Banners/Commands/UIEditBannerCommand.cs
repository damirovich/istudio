namespace ISTUDIO.Web.UI.Features.Banners.Commands;

using ISTUDIO.Contracts.Features.Banners;
using ResModel = Result;
public class UIEditBannerCommand : IRequest<ResponseAPI<ResModel>>
{
    public EditBannerVM Banners {  get; set; }
    public class Handler : IRequestHandler<UIEditBannerCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIEditBannerCommand command, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PutJsonAsync<ResModel, EditBannerVM>(command.Banners, $"Banners/EditBanner");

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
