using ISTUDIO.Application.Features.Orders.DTOs;
using ISTUDIO.Contracts.Features.Orders;
using System.ComponentModel.DataAnnotations;

namespace ISTUDIO.Web.UI.Features.Orders.Commands;
using ResModel = CreateOrderResponseDTO;
public class UICreateOrdersCommand : IRequest<ResponseAPI<ResModel>>
{
    [Required]
    public CreateOrdersVM Orders { get; set; }
    public class Handler : IRequestHandler<UICreateOrdersCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _apiClient;
        public Handler(APIHttpClient apiClient) => _apiClient = apiClient;

        public async Task<ResponseAPI<ResModel>> Handle(UICreateOrdersCommand command, CancellationToken cancellationToken)
        {
            var res = await _apiClient.PostJsonAsync<ResModel, CreateOrdersVM>(command.Orders, $"Orders/CreateOrders");
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
