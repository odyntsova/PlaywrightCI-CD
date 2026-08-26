using EaFramework.Config;
using PlaywriteTests.Driver;
using Microsoft.Playwright;

namespace EaFramework.Driver;

public class PlaywrightDriver : IDisposable, IPlaywrightDriver
{

    private readonly AsyncTask<IBrowser> _browser;
    private readonly TestSettings _testSettings;
    private readonly IPlaywrightDriverInitializer _playwrightDriverInitializer;
    private readonly AsyncTask<IBrowserContext> _brawserContext;
    private readonly AsyncTask<IPage> _page;
    private bool _isDisposed;

    public PlaywrightDriver(TestSettings testSettings, IPlaywrightDriverInitializer playwrightDriverInitializer)
    {
        _testSettings = testSettings;
        _playwrightDriverInitializer = playwrightDriverInitializer;
        _browser = new AsyncTask<IBrowser>(InitializePlaywrightAsync);
        _brawserContext = new AsyncTask<IBrowserContext>(CreateBrowserContext);
        _page = new AsyncTask<IPage>(CreatePageAsync);
    }

    public Task<IBrowser> Browser => _browser.Value;
    public Task<IBrowserContext> BrowserContext => _brawserContext.Value;
    public Task<IPage> Page => _page.Value;


    private async Task<IBrowser> InitializePlaywrightAsync()
    {

        return _testSettings.DriverType switch
        {
            DriverType.Chromium => await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings),
            DriverType.Chrome => await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings),
            DriverType.Firefox => await _playwrightDriverInitializer.GetFirefoxDriverAsync(_testSettings),
            DriverType.WebKit => await _playwrightDriverInitializer.GetWebKitDriverAsync(_testSettings),
            _ => await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings)
        };

    }

    private async Task<IBrowserContext> CreateBrowserContext()
    {
        return await (await _browser).NewContextAsync();

    }

    private async Task<IPage> CreatePageAsync()
    {
        return await (await _brawserContext).NewPageAsync();
    }

    // private async Task<IBrowser> GetBrowserAsync(TestSettings testSettings)
    // {
    //     var playwrightDriver = await Playwright.CreateAsync();
    //     var browserOption = new BrowserTypeLaunchOptions
    //     {
    //         Headless = testSettings.Headless,
    //         Devtools = testSettings.Devtools,
    //         SlowMo = testSettings.SlowMo
    //     };
    //
    //     return testSettings.DriverType switch
    //     {
    //         DriverType.Chromium => await playwrightDriver.Chromium.LaunchAsync(browserOption),
    //         DriverType.Chrome => await playwrightDriver["chromium"].LaunchAsync(browserOption),
    //         DriverType.Firefox => await playwrightDriver.Firefox.LaunchAsync(browserOption),
    //         DriverType.WebKit => await playwrightDriver.Webkit.LaunchAsync(browserOption),
    //         _ => await playwrightDriver.Chromium.LaunchAsync(browserOption)
    //     };
    // }
    

    public void Dispose()
    {
        if (!_isDisposed) return;
        {
            if (_browser.IsValueCreated)
                Task.Run(async () =>
                {
                    await (await Browser).CloseAsync();
                    await (await Browser).DisposeAsync();
                });
            
            _isDisposed = true;
        }
    }
    
}

