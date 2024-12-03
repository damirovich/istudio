namespace ISTUDIO.Web.Api.Mobile.Services.Models;

public class ApiClientSettings
{
    public string BaseAddress { get; set; }
}
public class ApiClientsSettings
{
    public ApiClientSettings FreedomPay { get; set; }
    public ApiClientSettings BakaiPay { get; set; }
}
