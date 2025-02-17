using System.Reflection;

namespace ISTUDIO.BankPaymentStatusCheckerService.ServiceStart;

public static class MappersExtensions
{
    public static void AddCustomAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            // Добавляем профили, если они существуют в указанных сборках
            mc.AddProfile(new AssemblyMappingProfile(typeof(Program).Assembly));

            // Проверяем наличие сборки для IAppDbContext и добавляем профиль
            var appDbContextAssembly = typeof(IAppDbContext).Assembly;
            if (appDbContextAssembly != null)
            {
                mc.AddProfile(new AssemblyMappingProfile(appDbContextAssembly));
            }

            // Загружаем сборку "ISTUDIO.Contracts" и добавляем профиль, если она существует
            //var contractsAssembly = Assembly.Load("ISTUDIO.Contracts");
            //if (contractsAssembly != null)
            //{
            //    mc.AddProfile(new AssemblyMappingProfile(contractsAssembly));
            //}
        });

        // Создаем и регистрируем IMapper как singleton
        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

    }
}