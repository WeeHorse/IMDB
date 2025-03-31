namespace E2ETesting.Steps;

using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;


[Binding]
public class ImdbSteps
{
    
    // SETUP:
    
    private IPlaywright _playwright;
    private IBrowser _browser;
    private IBrowserContext _context;
    private IPage _page;

    [BeforeScenario]
    public async Task Setup()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 0 });
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();
    }

    [AfterScenario]
    public async Task Teardown()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
    
    // STEPS:
    
    [Given(@"I am on the IMDB homepage")]
    public async Task GivenIAmOnTheImdbHomepage()
    {
        await _page.GotoAsync("https://imdb.com");
    }

    [Given(@"I see the cookie prompt")]
    public async Task GivenISeeTheCookiePrompt()
    {
        //await _page.WaitForTimeoutAsync(3000);
        await _page.WaitForSelectorAsync("[data-testid='consent-banner']");
        var el = await _page.QuerySelectorAsync("*:has-text('Select Your Preferences')");
        Assert.NotNull(el);
    }

    [When(@"I click on Accept")]
    public async Task WhenIClickOnAccept()
    {
        await _page.ClickAsync("[data-testid='accept-button']");
    }

    [Then(@"the cookie prompt is gone")]
    public async Task ThenTheCookiePromptIsGone()
    {
        await _page.WaitForTimeoutAsync(1000); // replace this with a while loop please
        var el = await _page.QuerySelectorAsync("*:has-text('Select Your Preferences')");
        Assert.Null(el);
    }
}