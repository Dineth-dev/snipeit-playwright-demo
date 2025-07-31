using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Threading.Tasks;

[TestFixture]
public class LoginTests : TestSetup
{
    [Test]
    public async Task Login_ShouldSucceed_WithValidCredentials()
    {
        var loginPage = new LoginPage(page);
        await loginPage.GoToLoginPage();
        await loginPage.Login("admin", "password");
        Assert.That(await page.IsVisibleAsync("h1:has-text(\"Dashboard\")"), Is.True);
    }
}