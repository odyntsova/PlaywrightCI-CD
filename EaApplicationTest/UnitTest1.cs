
using AutoFixture.Xunit2;
using EaFramework.Config;
using EaFramework.Driver;
using EaTestAutomation.Models;
using EaTestAutomation.Pages;
using Microsoft.Playwright;

namespace EaTestAutomation;

// public class CreateProductTest : PageTest
// {
//     [Test]
//     public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
//     {
//         await Page.GotoAsync("https://playwright.dev");
//
//         // Expect a title "to contain" a substring.
//         await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
//
//         // create a locator
//         var getStarted = Page.Locator("text=Get Started");
//
//         // Expect an attribute "to be strictly equal" to the value.
//         await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");
//
//         // Click the get started link.
//         await getStarted.ClickAsync();
//
//         // Expects the URL to contain intro.
//         await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
//     }
// }


public class Tests
{
    private readonly IPlaywrightDriver _playwrightDriver;
    private readonly TestSettings _testSettings;
    private readonly IProductListPage _productListPage;
    private readonly IProductPage _productPage;


    public Tests(IPlaywrightDriver playwrightDriver, TestSettings testSettings, IProductListPage productListPage, IProductPage productPage)
    {
        
        _playwrightDriver = playwrightDriver;
        _testSettings = testSettings;
        _productListPage = productListPage;
        _productPage = productPage;
    }
    
    [Fact]
    public async Task Test1()
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync("http://eaapp.somee.com");
        await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Login" }).ClickAsync();
        await page.GetByLabel("UserName").FillAsync("admin");
        await page.GetByLabel("Password").FillAsync("password");
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Employee List" }).ClickAsync();
    }
    
    // [Fact]
    // public async Task LoginTest()
    // {
    //     var page = await _playwrightDriver.Page;
    //     await page.GotoAsync(_testSettings.ApplicationUrl);
    //     await page.ClickAsync("text = Login");
    //     await page.GetByLabel("User Name").FillAsync("admin");
    //     await page.GetByLabel("Password").FillAsync("password");
    //     
    //     await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Sign In" }).ClickAsync();
    //     await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "View Employees" }).ClickAsync();
    //     
    // }
    
    // [Theory]
    // [InlineData("Speacker", "Gaming speaker", 2000, "2")]
    // [InlineData("USB", "USB 3.0", 300, "3")]
    // [InlineData("Webcam", "Camera", 4000, "2")]
    // [InlineData("Wires", "Wires for life",1000, "2")]
    //
    // public async Task Test2(string name, string description, decimal price, string product)
    // {
    //     var page = await _playwrightDriver.Page;
    //     await page.GotoAsync("http://localhost:8000/");
    //     
    //     ProductListPage productListPage = new ProductListPage(page);
    //     ProductPage productPage = new ProductPage(page);
    //     
    //     await productListPage.CreateProductAsync();
    //     await productPage.CreateProduct(name, description, price, product);
    //     await productPage.ClickCreate();
    //
    //     await productListPage.ClickProductFormList(name);
    //     
    //     var element = productListPage.IsProductCreated(name);
    //     await Assertions.Expect(element).ToBeVisibleAsync();
    // }
    
    // [Fact]
    //
    // public async Task TestWithConcreteType()
    // {
    //     var product = new Product()
    //     {
    //         Name = "Test",
    //         Description = "Test product",
    //         Price = 100,
    //         ProductType = ProductType.CPU
    //     };
    //     
    //     var page = await _playwrightDriver.Page;
    //     await page.GotoAsync("http://localhost:8000/");
    //     
    //     ProductListPage productListPage = new ProductListPage(page);
    //     ProductPage productPage = new ProductPage(page);
    //     
    //     await productListPage.CreateProductAsync();
    //     await productPage.CreateProduct(product);
    //     await productPage.ClickCreate();
    //
    //     await productListPage.ClickProductFormList(product.Name);
    //     
    //     var element = productListPage.IsProductCreated(product.Name);
    //     await Assertions.Expect(element).ToBeVisibleAsync();
    // }
    
    [Theory, AutoData]
    
    public async Task TestWithAutoFixture(Product product)
    {
        
        var page = await _playwrightDriver.Page;
        await page.GotoAsync("http://localhost:5001/");
        
        
        await _productListPage.CreateProductAsync();
        await _productPage.CreateProduct(product);
        await _productPage.ClickCreate();
    
        await _productListPage.ClickProductFormList(product.Name);
        
        var element = _productListPage.IsProductCreated(product.Name);
        await Assertions.Expect(element).ToBeVisibleAsync();
    }
    
}