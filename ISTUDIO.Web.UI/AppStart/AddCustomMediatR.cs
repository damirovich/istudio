using System.Reflection;

namespace ISTUDIO.Web.UI.AppStart;

public static class MediatRExtensions
{
    public static void AddCustomMediatR(this MediatRServiceConfiguration services)
                    => services.RegisterServicesFromAssemblies(
                            typeof(Program).Assembly,
                            Assembly.Load("ISTUDIO.Web.UI")

        );
}
