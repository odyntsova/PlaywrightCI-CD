
using AutoFixture.Xunit2;
using EaFramework.Config;
using EaFramework.Driver;
using EaTestAutomation.Fixture;
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


public class Tests1
{
  
    private readonly IProductListPage _productListPage;
    private readonly IProductPage _productPage;
    private readonly ITestFixtureBased _testFixtureBased;


    public Tests1(ITestFixtureBased testFixtureBased, IProductListPage productListPage, IProductPage productPage)
    {
        
       
        _testFixtureBased = testFixtureBased;
        _productListPage = productListPage;
        _productPage = productPage;
    }
    
    [Theory, AutoData]
    
    public async Task TestWithAutoFixture(Product product)
    {
    
        await _testFixtureBased.NavigateToUrl();
        
        
        await _productListPage.CreateProductAsync();
        await _productPage.CreateProduct(product);
        await _productPage.ClickCreate();
    
        await _productListPage.ClickProductFormList(product.Name);
        
        var element = _productListPage.IsProductCreated(product.Name);
        await Assertions.Expect(element).ToBeVisibleAsync();
    }
    
}