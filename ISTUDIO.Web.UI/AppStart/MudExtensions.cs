using MudBlazor.Services;
using MudBlazor;

namespace ISTUDIO.Web.UI.AppStart;

public static class MudExtensions
{
    public static void AddCustomMud(this MudServicesConfiguration services)
    {
        services.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
        services.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopEnd;
    }
}
