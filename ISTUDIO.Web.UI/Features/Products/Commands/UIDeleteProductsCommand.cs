namespace ISTUDIO.Web.UI.Features.Products.Commands;


using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class UIDeleteProductsCommand : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int ProductId { get; set; }

    public class Handler : IRequestHandler<UIDeleteProductsCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;

        public Handler(APIHttpClient apiClient) => _apiClient = apiClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIDeleteProductsCommand command, CancellationToken cancellationToken)
        {
            var res = await _apiClient.DeleteJsonAsync<ResModel>($"Products/DeleteProducts?Id={command.ProductId}");
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
