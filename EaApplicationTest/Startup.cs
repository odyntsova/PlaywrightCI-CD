using EaFramework.Config;
using EaFramework.Driver;
using EaTestAutomation.Fixture;
using EaTestAutomation.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace EaTestAutomation;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSingleton(ConfigReader.ReadConfig())
            .AddScoped<IPlaywrightDriver, PlaywrightDriver>()
            .AddScoped<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>()
            .AddScoped<IProductPage, ProductPage>()
            .AddScoped<IProductListPage, ProductListPage>()
            .AddScoped<ITestFixtureBased, TestFixtureBased>();
        
    }

}