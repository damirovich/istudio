namespace ISTUDIO.Web.UI.Features.Products.Commands;

using ISTUDIO.Contracts.Features.Products;
using ResModel = Result;
public class UIUpdateProductQuantityCommand : IRequest<ResponseAPI<ResModel>>
{
    public UpdateProductQuantityVM ProductQuantityUpdate { get; set; }

    public class Handler : IRequestHandler<UIUpdateProductQuantityCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;

        public Handler(APIHttpClient httpClient) => _apiClient = httpClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIUpdateProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PutJsonAsync<ResModel, UpdateProductQuantityVM>(request.ProductQuantityUpdate, $"Products/UpdateProductQuantity");

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
