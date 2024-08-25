namespace ISTUDIO.Web.UI.Features.Magazine.Commands;

using ISTUDIO.Contracts.Features.Magazines;
using ResModel = Result;
public class UICreateMagazinesCommand : IRequest<ResponseAPI<ResModel>>
{
    public CreateMagazineVM Magazine { get; set; }

    public class Handler : IRequestHandler<UICreateMagazinesCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _httpClient;

        public Handler(APIHttpClient httpClient) => _httpClient = httpClient;

        public async Task<ResponseAPI<ResModel>> Handle(UICreateMagazinesCommand command, CancellationToken cancellationToken)
        {
            var res = await _httpClient.PostJsonAsync<ResModel, CreateMagazineVM>(command.Magazine, $"Magazines/CreateMagazine");

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