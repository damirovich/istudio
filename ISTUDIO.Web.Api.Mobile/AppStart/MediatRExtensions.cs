using System.Reflection;

namespace ISTUDIO.Web.Api.Mobile.AppStart;


public static class MediatRExtensions
{
    public static void AddCustomMediatR(this MediatRServiceConfiguration services)
                 => services.RegisterServicesFromAssemblies(
                         typeof(Program).Assembly,
                         Assembly.Load("ISTUDIO.Application"),
                         Assembly.Load("ISTUDIO.Infrastructure")
     );
}
