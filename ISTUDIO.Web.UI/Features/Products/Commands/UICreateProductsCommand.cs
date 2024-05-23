namespace ISTUDIO.Web.UI.Features.Products.Commands;

using ISTUDIO.Contracts.Features.Products;

using ResModel = Result;
public class UICreateProductsCommand : IRequest<ResponseAPI<ResModel>>
{
    public CreateProductsVM Products { get; set; }

    public class Handler : IRequestHandler<UICreateProductsCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UICreateProductsCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PostJsonAsync<ResModel, CreateProductsVM>(request.Products, $"Products/CreateProducts");

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
