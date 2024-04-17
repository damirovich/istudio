using ISTUDIO.Application.Features.Categories.DTOs;

namespace ISTUDIO.Web.UI.Features.Categories.Queries;

using ResModel = CategoriesListResponseDTO;
public class UIGetCategoriesQuery : IRequest<ResponseAPI<ResModel>>
{
    public class Handler : IRequestHandler<UIGetCategoriesQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIGetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Categories/GetCategoriesList");
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
