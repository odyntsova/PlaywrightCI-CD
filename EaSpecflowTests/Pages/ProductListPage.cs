using EaFramework.Driver;
using Microsoft.Playwright;

namespace EaSpecflowTests.Pages;

public interface IProductListPage
{
    Task CreateProductAsync();
    Task ClickProductFormList(string name);
    Task<bool> IsProductCreatedAsync(string product);
    ILocator IsProductCreated(string product);
}

public class ProductListPage : IProductListPage
{
    private readonly IPage _page;
    public ProductListPage(IPlaywrightDriver playwrightDriver) => _page = playwrightDriver.Page.Result;

    private ILocator _linkProductList => _page.GetByRole(AriaRole.Link, new() { Name = "Product" });
    private ILocator _linkCreate => _page.GetByRole(AriaRole.Link, new() { Name = "Create" });
    
    public async  Task CreateProductAsync()
    {
        await _linkProductList.ClickAsync();
        await _linkCreate.ClickAsync();
    }

    public async Task ClickProductFormList(string name)
    {
       await _page.GetByRole(AriaRole.Row, new() { Name = name })
            .GetByRole(AriaRole.Link, new() { Name = "Details" }).ClickAsync();
    }

    public async Task<bool> IsProductCreatedAsync(string product)
    {
        return await _page.IsVisibleAsync("#Name");
         
    }
    
    public ILocator IsProductCreated(string product)
    {
        return _page.GetByText(product, new () {Exact = true});


    }

}