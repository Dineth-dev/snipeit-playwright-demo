using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

public class TestSetup
{
    protected IPlaywright playwright;
    protected IBrowser browser;
    protected IBrowserContext context;
    protected IPage page;

    [SetUp]
    public async Task Setup()
    {
        playwright = await Playwright.CreateAsync();
        browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        context = await browser.NewContextAsync();
        page = await context.NewPageAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await browser.CloseAsync();
        playwright.Dispose();
    }
}