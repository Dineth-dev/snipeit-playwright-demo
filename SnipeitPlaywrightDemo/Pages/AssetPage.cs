using Microsoft.Playwright;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations.Schema;
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
        await _page.WaitForSelectorAsync("h1:has-text(\"Dashboard\")");
        await _page.Locator("div.dashboard.small-box.bg-teal div.inner p:has-text(\"Assets\")").ClickAsync();
        await _page.WaitForSelectorAsync("div.th-inner.sortable.both.asc:has-text(\"Asset Name\")");
    }

    public async Task FindAsset(string assetTag)
    {
        await _page.FillAsync("input.form-control.search-input", assetTag);
        await _page.Keyboard.PressAsync("Enter");
    }

    public async Task<(string assetTag, string user)> CreateNewAssetWithInformation(string modelName, string status)
    {
        await _page.ClickAsync("a.dropdown-toggle:has-text(\"Create New\")");
        await _page.WaitForSelectorAsync("ul.dropdown-menu li a:has-text(\"Asset\")");
        await _page.ClickAsync("ul.dropdown-menu li a:has-text(\"Asset\")");
        await _page.WaitForSelectorAsync("li.breadcrumb-item.active:has-text(\"Create New\")");
        var assetTag = await _page.Locator("input#asset_tag").GetAttributeAsync("value");
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
        var user = await _page.Locator("span#select2-assigned_user_select-container").GetAttributeAsync("title");
        await _page.ClickAsync("button[type='submit']:has-text(\"Save\")");
        return (assetTag ?? string.Empty, user ?? string.Empty);
    }
    public async Task GoToAssetDetails(string assetTag)
    {
        await _page.ClickAsync($"mark:has-text(\"{assetTag}\")");
    }

    public async Task<string> GetAssetDetails()
    {
        string assetTagDetails = await _page.Locator("span.js-copy-assettag").InnerTextAsync();
        return assetTagDetails;
    }

    public async Task GotoAssetHistory()
    {
        await _page.ClickAsync("a[href='#history']");
    }

    public async Task RedirectToHomePage()
    {
        await _page.ClickAsync("img.navbar-brand-img");
    }

    public async Task <(ILocator  rowMatchingAssetTag, ILocator  rowMatchingModelName, string userNameListed)> getAssetHistoryDetails(string assetTag, string modelName)
    {
        await _page.WaitForSelectorAsync("table#assetHistory");
        await Task.Delay(5000);
        var table = _page.Locator("table#assetHistory");
        var rows = table.Locator("tbody tr");
        var rowMatchingAssetTag = rows.Filter(new LocatorFilterOptions
        {
            HasText = assetTag
        });
        var rowMatchingModelName = rows.Filter(new LocatorFilterOptions
        {
            HasText = modelName
        });
        string userNameListed = await _page.Locator("a[data-original-title='user']").InnerTextAsync();
        return (rowMatchingAssetTag, rowMatchingModelName, userNameListed);
    }

    public string ParseUsername(string user)
    {
        var userDetails = user.Split(',');
        var firstNamePart = userDetails[1].Split('(');
        string firstName = firstNamePart[0].Trim();
        string lastName = userDetails[0].Trim();
        user = lastName + ' ' + firstName;
        return user;
    }
}