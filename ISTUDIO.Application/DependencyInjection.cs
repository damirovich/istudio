using ISTUDIO.Application.Common.Behaviors;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ISTUDIO.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
           this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IRequestExceptionHandler<,,>),
            typeof(ExceptionBehavior<,,>));

        return services;
    }
}
