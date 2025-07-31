using Microsoft.Playwright;
using System.Threading.Tasks;

public class AssetPage
{
    private readonly IPage _page;

    public AssetPage(IPage page)
    {
        _page = page;
    }

    public async Task GoToAssetPage()
    {
        await _page.Locator("div.dashboard.small-box.bg-teal div.inner p:has-text(\"Assets\")").ClickAsync();
        await _page.WaitForSelectorAsync("div.th-inner.sortable.both.asc:has-text(\"Asset Name\")");
    }

    public async Task CreateNewAssetWithInformation(string modelName, string status)
    {
        await _page.ClickAsync("a.dropdown-toggle:has-text(\"Create New\")");
        await _page.WaitForSelectorAsync("ul.dropdown-menu li a:has-text(\"Asset\")");
        await _page.ClickAsync("ul.dropdown-menu li a:has-text(\"Asset\")");
        await _page.WaitForSelectorAsync("li.breadcrumb-item.active:has-text(\"Create New\")");
        await _page.WaitForSelectorAsync("span.select2-selection__placeholder.needsclick:has-text(\"Select a Model\")");
        await _page.ClickAsync("span.select2-selection__placeholder.needsclick:has-text(\"Select a Model\")");
        await _page.FillAsync("input.select2-search__field", modelName);
        await _page.WaitForSelectorAsync("div:has-text(\"Laptops - Macbook Pro 13\")");
        await _page.ClickAsync("div:has-text(\"Laptops - Macbook Pro 13\")");
        await _page.ClickAsync("span.select2-selection__rendered.needsclick:has-text(\"Select Status\")");
        await _page.FillAsync("input.select2-search__field", status);
        await _page.Keyboard.PressAsync("Enter");
        await _page.ClickAsync("span#select2-assigned_user_select-container:has-text(\"Select a User\")");
        await _page.WaitForSelectorAsync("ul[role='listbox'] >> li");
        await _page.ClickAsync("ul[role='listbox'] >> li >> nth=0");
        await _page.ClickAsync("button[type='submit']");
    }       
    

}