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
            //SlowMo = 10 // Add a delay between actions
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
    
    [TestMethod]
    public async Task LoginAndSeeEmployees()
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
        
        await _page.GetByText("Employees").ClickAsync();
    }
    
    [TestMethod]
    public async Task LoginAndAddEmployee()
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
        
        await _page.GetByText("Employees").ClickAsync();
        
        await _page.GetByText("Add Employee").ClickAsync();
        
        await Expect(_page.GetByRole(AriaRole.Button, new() { Name = "Create New Employee" })).ToBeVisibleAsync();
        
        await _page.GetByRole(AriaRole.Textbox, new() { Name = "Firstname" }).FillAsync("Pelle");
        await _page.GetByRole(AriaRole.Textbox, new() { Name = "Lastname" }).FillAsync("Kanin");
        await _page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync("abc@email.com");
        await _page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync("abc123");
        await _page.GetByRole(AriaRole.Radio, new() { Name = "USER" }).ClickAsync();

        await _page.GetByText("Create New Employee").ClickAsync();
        
    }
    
    [TestMethod]
    public async Task LoginAndChangeIssueToOpenThenClosedAndBackToNew()
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
        
        //ENTER ISSUES PAGE
        await _page.GetByText("Issues").ClickAsync();
        
        #region Change STATUS to OPEN
        //CLICKS ON EDIT BUTTON FOR FIRST ISSUE
        await _page.Locator("button.subjectEditButton").First.ClickAsync();
        
        //CHANGE STATUS ON ISSUE TO OPEN
        await _page.Locator("select.stateSelect").SelectOptionAsync("OPEN");

        await _page.Locator("button.stateUpdateButton").ClickAsync();
        #endregion
        
        #region Change STATUS to NEW
        //CLICKS ON EDIT BUTTON FOR FIRST ISSUE
        await _page.Locator("button.subjectEditButton").First.ClickAsync();
        
        //CHANGE STATUS ON ISSUE TO CLOSED
        await _page.Locator("select.stateSelect").SelectOptionAsync("CLOSED");

        await _page.Locator("button.stateUpdateButton").ClickAsync();
        #endregion

        #region Change STATUS to NEW
        //CLICKS ON EDIT BUTTON FOR FIRST ISSUE
        await _page.Locator("button.subjectEditButton").First.ClickAsync();
        
        //CHANGE STATUS ON ISSUE TO NEW
        await _page.Locator("select.stateSelect").SelectOptionAsync("NEW");

        await _page.Locator("button.stateUpdateButton").ClickAsync();
        #endregion
        await _page.GetByRole(AriaRole.Button, new() { Name = "Logout" }).ClickAsync();

    }
    
    [TestMethod]
    public async Task RegisterUser_HandlesSuccessOrFailure()
    {
        string? alertText = null;

        // Lyssna på alerten
        _page.Dialog += async (_, dialog) =>
        {
            alertText = dialog.Message;
            await dialog.AcceptAsync();
        };

        await _page.GotoAsync("http://localhost:3000/");
        await _page.GetByText("Register").ClickAsync();

        // Fyll i formuläret
        await _page.Locator("input[name='email']").FillAsync("user@email.com");
        await _page.Locator("input[name='password']").FillAsync("abc123");
        await _page.Locator("input[name='username']").FillAsync("CoolUser");
        await _page.Locator("input[name='company']").FillAsync("OpenAI");

        // Klicka på "Skapa Konto"
        await _page.GetByRole(AriaRole.Button, new() { Name = "Skapa Konto" }).ClickAsync();

        // Vänta en liten stund så redirect hinner ske om det var lyckat
        await _page.WaitForTimeoutAsync(1000);

        // Verifiera att alert dök upp
        Assert.IsNotNull(alertText, "Ingen alert visades efter registrering.");

        if (alertText.StartsWith("Du har lyckats registrera dig!"))
        {
            // Det var en lyckad registrering
            StringAssert.Contains(_page.Url, "/login");
        }
        else
        {
            // Det var ett fel – visa feltext och stanna på samma sida
            Console.WriteLine("Registreringen misslyckades med meddelande:\n" + alertText);
        
            // Kontrollera att URL INTE innehåller "/login" om registreringen misslyckades
            Assert.IsFalse(_page.Url.Contains("/login"), "Redirected to login despite registration failure.");
        }
    }

}
