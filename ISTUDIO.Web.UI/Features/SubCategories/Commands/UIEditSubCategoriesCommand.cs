namespace ISTUDIO.Web.UI.Features.SubCategories.Commands;

using ISTUDIO.Contracts.Features.SubCategories;
using ResModel = Result;
public class UIEditSubCategoriesCommand : IRequest<ResponseAPI<ResModel>>
{
    public EditSubCategoriesVM Categories { get; set; }

    public class Handler : IRequestHandler<UIEditSubCategoriesCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIEditSubCategoriesCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PutJsonAsync<ResModel, EditSubCategoriesVM>(request.Categories, $"Categories/EditCategories");

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
