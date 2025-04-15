namespace ShopTester.Tests;

using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;


[TestClass]
public class LoginTest : PageTest
{
    private IPlaywright _playwright;
    private IBrowser _browser;
    private IBrowserContext _browserContext;
    private IPage _page;

    [TestInitialize]
    public async Task Setup()
    {
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
            //SlowMo = 100 // Add a delay between actions
        });
        _browserContext = await _browser.NewContextAsync();
        _page = await _browserContext.NewPageAsync();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await _browserContext.CloseAsync();
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    public async Task GivenIAmOnTheHomePage(string url)
    {
        // Go to the specified URL
        await _page.GotoAsync(url);
    }

    [TestMethod]
    public async Task Login()
    {
        
        await _page.GotoAsync("http://localhost:3000/");
        // Click the login link in nav.
        await _page.GetByText("Login").ClickAsync();

        // Fill out the login form and submit.
        await _page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync("m@email.com");
        await _page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).ClickAsync();
        await _page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync("abc123");
        await _page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        // Expect the page to have a welcome message and a logout button.
        
        await Expect(_page.GetByRole(AriaRole.Button, new() { Name = "Logout" })).ToBeVisibleAsync();
    }
    
    [TestMethod]
    public async Task LoginAndSeeIssues()
    {
        
        await _page.GotoAsync("http://localhost:3000/");
        // Click the login link in nav.
        await _page.GetByText("Login").ClickAsync();

        // Fill out the login form and submit.
        await _page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync("m@email.com");
        await _page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).ClickAsync();
        await _page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync("abc123");
        await _page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        // Expect the page to have a logout button.
        await Expect(_page.GetByRole(AriaRole.Button, new() { Name = "Logout" })).ToBeVisibleAsync();
        
        await _page.GetByText("Issues").ClickAsync();
    }
}
