using Microsoft.Playwright;
using System.Threading.Tasks;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task GoToLoginPage()
    {
        await _page.GotoAsync("https://demo.snipeitapp.com/login");
    }

    public async Task Login(string username, string password)
    {
        await _page.FillAsync("#username", username);
        await _page.FillAsync("#password", password);
        await _page.ClickAsync("button[type='submit']");
        await _page.WaitForSelectorAsync("h1:has-text(\"Dashboard\")");
    }
} 