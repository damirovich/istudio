using ISTUDIO.Application.Features.Magazines.DTOs;
using System.ComponentModel.DataAnnotations;


namespace ISTUDIO.Web.UI.Features.Products.Queries;
using ResModel = PaginatedList<MagazinesDTO>;
public class UIGetMagazineListQuery : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public int PageSize { get; set; }
    public class Handler : IRequestHandler<UIGetMagazineListQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient)
        {
            _apiClient = httpClient;
        }

        public async Task<ResponseAPI<ResModel>> Handle(UIGetMagazineListQuery request, CancellationToken cancellationToken)
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
