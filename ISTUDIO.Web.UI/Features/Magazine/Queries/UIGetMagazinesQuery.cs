using ISTUDIO.Application.Features.Magazines.DTOs;


namespace ISTUDIO.Web.UI.Features.Magazine.Queries;
using ResModel = PaginatedList<MagazinesDTO>;
public class UIGetMagazinesQuery : IRequest<ResponseAPI<ResModel>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public class Handler : IRequestHandler<UIGetMagazinesQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIGetMagazinesQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Magazines/GetMagazinesList?pageNumber={request.PageNumber}&pageSize={request.PageSize}");
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
