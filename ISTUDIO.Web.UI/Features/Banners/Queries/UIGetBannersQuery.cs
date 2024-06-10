using ISTUDIO.Application.Features.Banners.DTOs;

namespace ISTUDIO.Web.UI.Features.Banners.Queries;

using ResModel = BannerListResponseDTO;
public class UIGetBannersQuery : IRequest<ResponseAPI<ResModel>>
{
    public class Handler : IRequestHandler<UIGetBannersQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIGetBannersQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Banners/GetBannersList");
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
