namespace ISTUDIO.Web.UI.Features.Categories.Commands;

using ISTUDIO.Contracts.Features.Categories;
using ResModel = Result;
public class UIEditCategoriesCommand : IRequest<ResponseAPI<ResModel>>
{
    public EditCategoriesVM Categories { get; set; }

    public class Handler : IRequestHandler<UIEditCategoriesCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIEditCategoriesCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PutJsonAsync<ResModel, EditCategoriesVM>(request.Categories, $"Categories/EditCategories");

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
