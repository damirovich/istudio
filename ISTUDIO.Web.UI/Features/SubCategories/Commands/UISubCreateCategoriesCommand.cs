namespace ISTUDIO.Web.UI.Features.SubCategories.Commands;


using ISTUDIO.Contracts.Features.SubCategories;
using ResModel = Result;
public class UISubCreateCategoriesCommand : IRequest<ResponseAPI<ResModel>>
{
    public CreateSubCategoriesVM Categories { get; set; }
    
    public class Handler : IRequestHandler<UISubCreateCategoriesCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UISubCreateCategoriesCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PostJsonAsync<ResModel, CreateSubCategoriesVM>(request.Categories, $"SubCategories/CreateSubCategories");

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
