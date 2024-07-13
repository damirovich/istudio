namespace ISTUDIO.Web.UI.Features.Customers.Commands;
using System.ComponentModel.DataAnnotations;
using ResModel = Result;
public class UIDeleteCustomersCommand : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public int CustomerId { get; set; }

    public class Handler : IRequestHandler<UIDeleteCustomersCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;

        public Handler(APIHttpClient apiClient) => _apiClient = apiClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIDeleteCustomersCommand command, CancellationToken cancellationToken)
        {
            var res = await _apiClient.DeleteJsonAsync<ResModel>($"Customers/DeleteCustomers?Id={command.CustomerId}");
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
