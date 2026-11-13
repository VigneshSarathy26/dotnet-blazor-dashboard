namespace DotNetBlazorDashboard.Tests;

using System.Threading.Tasks;
using Microsoft.Playwright;
using Xunit;

public class PlaywrightE2ETests
{
    [Fact]
    public async Task BlazorApp_PlaywrightBrowser_CanInitialize()
    {
        // Playwright test verifying browser lifecycle and readiness for Blazor UI testing
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        var page = await browser.NewPageAsync();
        Xunit.Assert.NotNull(page);

        // When running against local or Docker host:
        // await page.GotoAsync("http://localhost:5000");
        // var title = await page.TitleAsync();
        // Xunit.Assert.Contains("Dashboard", title);
    }
}

