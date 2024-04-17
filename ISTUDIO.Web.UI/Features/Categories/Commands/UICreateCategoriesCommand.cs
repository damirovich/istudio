namespace ISTUDIO.Web.UI.Features.Categories.Commands;

using ISTUDIO.Contracts.Features.Categories;
using Microsoft.AspNetCore.Http;
using ResModel = Result;
public class UICreateCategoriesCommand : IRequest<ResponseAPI<ResModel>>
{
    public CreateCategoriesVM Categories { get; set; }
    
    public class Handler : IRequestHandler<UICreateCategoriesCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UICreateCategoriesCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PostJsonAsync<ResModel, CreateCategoriesVM>(request.Categories, $"Categories/CreateCategories");

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
