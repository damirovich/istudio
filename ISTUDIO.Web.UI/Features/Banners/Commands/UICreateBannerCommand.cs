namespace ISTUDIO.Web.UI.Features.Banners.Commands;

using ISTUDIO.Contracts.Features.Banners;

using ResModel = Result;
public class UICreateBannerCommand : IRequest<ResponseAPI<ResModel>>
{
    public CreateBannerVM Banners { get; set; }

    public class Handler : IRequestHandler<UICreateBannerCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UICreateBannerCommand command, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PostJsonAsync<ResModel, CreateBannerVM>(command.Banners, $"Banners/CreateBanner");

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
