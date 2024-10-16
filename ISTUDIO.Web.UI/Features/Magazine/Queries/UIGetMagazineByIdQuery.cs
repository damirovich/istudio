using ISTUDIO.Application.Features.Magazines.DTOs;

namespace ISTUDIO.Web.UI.Features.Magazine.Queries;
using ResModel = MagazinesDTO;
public class UIGetMagazineByIdQuery : IRequest<ResponseAPI<ResModel>>
{
    public int MagazineId { get; set; }
    public class Handler : IRequestHandler<UIGetMagazineByIdQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIGetMagazineByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Magazines/GetMagazineById?id={request.MagazineId}");
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
