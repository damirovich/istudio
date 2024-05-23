using ISTUDIO.Application.Features.Categories.DTOs;


namespace ISTUDIO.Web.UI.Features.SubCategories.Queries;
using ResModel = CategoryResponseDTO;
public class UIGetSubCategoriesByIdQuery : IRequest<ResponseAPI<ResModel>>
{
    public int CategoryId { get; set; }
    public class Handler : IRequestHandler<UIGetSubCategoriesByIdQuery, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIGetSubCategoriesByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.GetJsonAsync<ResModel>($"Categories/GetCategoriesById?categoryId={request.CategoryId}");
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