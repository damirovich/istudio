using ISTUDIO.Application.Features.Banners.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.UI.Features.Banners.Queries;
using ResModel = BannerDTO;
public class UIGetBannersByIdQuery : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int BannerId { get; set; }
    public class Handler : IRequestHandler<UIGetBannersByIdQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIGetBannersByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Banners/GetBannersById?id={request.BannerId}");
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


