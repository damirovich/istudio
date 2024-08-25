namespace ISTUDIO.Web.UI.Features.Magazine.Commands;

using ISTUDIO.Contracts.Features.Magazines;
using ResModel = Result;
public class UIEditMagazinesCommand : IRequest<ResponseAPI<ResModel>>
{
    public EditMagazineVM EditMagazine { get; set; }

    public class Handler : IRequestHandler<UIEditMagazinesCommand, ResponseAPI<ResModel>>
    {
        private readonly APIHttpClient _httpClient;

        public Handler(APIHttpClient httpClient) => _httpClient = httpClient;

        public async Task<ResponseAPI<ResModel>> Handle(UIEditMagazinesCommand command, CancellationToken cancellationToken)
        {
            var res = await _httpClient.PutJsonAsync<ResModel, EditMagazineVM>(command.EditMagazine, $"Magazines/EditMagazine");

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
