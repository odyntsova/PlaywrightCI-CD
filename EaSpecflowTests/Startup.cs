using EaFramework.Config;
using EaFramework.Driver;
using EaSpecflowTests.Pages;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace EaSpecflowTests;

public class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        services
            .AddSingleton(ConfigReader.ReadConfig())
            .AddScoped<IPlaywrightDriver, PlaywrightDriver>()
            .AddScoped<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>()
            .AddScoped<IProductPage, ProductPage>()
            .AddScoped<IProductListPage, ProductListPage>();
         
        return services;
    }
}