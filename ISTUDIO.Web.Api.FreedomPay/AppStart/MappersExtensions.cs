using ISTUDIO.Application.Common.Mappings;
using System.Reflection;

namespace ISTUDIO.Web.Api.FreedomPay.AppStart;

public static class MappersExtensions
{
    public static void AddCustomAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AssemblyMappingProfile(typeof(Program).Assembly));
            mc.AddProfile(new AssemblyMappingProfile(typeof(IAppDbContext).Assembly));
            mc.AddProfile(new AssemblyMappingProfile(Assembly.Load("ISTUDIO.Contracts")));
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}
