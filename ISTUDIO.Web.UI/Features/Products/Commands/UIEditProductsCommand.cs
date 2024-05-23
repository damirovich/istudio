namespace ISTUDIO.Web.UI.Features.Products.Commands;

using ISTUDIO.Contracts.Features.Categories;
using ISTUDIO.Contracts.Features.Products;

using ResModel = Result;
public class UIEditProductsCommand : IRequest<ResponseAPI<ResModel>>
{
    public EditProductsVM Products {  get; set; }

    public class Handler : IRequestHandler<UIEditProductsCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;
        public async Task<ResponseAPI<ResModel>> Handle(UIEditProductsCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PutJsonAsync<ResModel, EditProductsVM>(request.Products, $"Products/EditProducts");

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
