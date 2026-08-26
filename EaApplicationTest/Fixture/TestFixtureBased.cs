using System.IO.Enumeration;
using EaFramework.Config;
using EaFramework.Driver;
using Microsoft.Playwright;

namespace EaTestAutomation.Fixture;

public interface ITestFixtureBased
{
    Task NavigateToUrl();
    Task TakeScreenshotAsync(string fileName);
}

public class TestFixtureBased : ITestFixtureBased
{
    private readonly IPlaywrightDriver _playwrightDriver;
    private readonly TestSettings _testSettings;
    private Task<IPage> _page;

    public TestFixtureBased(IPlaywrightDriver playwrightDriver, TestSettings testSettings)
    {
        _playwrightDriver = playwrightDriver;
        _testSettings = testSettings;
        _page = playwrightDriver.Page;
    }

    public async Task NavigateToUrl()
    {
        await (await _page).GotoAsync(_testSettings.ApplicationUrl);
    }

    public async Task TakeScreenshotAsync(string fileName)
    {
        await (await _page).ScreenshotAsync(new PageScreenshotOptions() {Path = fileName, FullPage =  true});
    }
}