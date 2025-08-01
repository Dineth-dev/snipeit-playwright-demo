using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Threading.Tasks;

[TestFixture]
public class AssetTests : TestSetup
{
    [Test]
    public async Task NewAsset_ShouldBeCreated_WhenValidInformationIsAdded()
    {
        var loginPage = new LoginPage(page);
        var assetPage = new AssetPage(page);
        string modelName = "Macbook Pro";
        string statusName = "ready to deploy";
        await loginPage.GoToLoginPage();
        await loginPage.Login("admin", "password");
        await page.Locator("div.dashboard.small-box.bg-teal div.inner p:has-text(\"Assets\")").ClickAsync();
        await page.WaitForSelectorAsync("div.th-inner.sortable.both.asc:has-text(\"Asset Name\")");
        string assetTag = await assetPage.CreateNewAssetWithInformation(modelName, statusName);
        await assetPage.RedirectToHomePage();
        await assetPage.GoToAssetPage();
        await assetPage.FindAsset(assetTag);
        await page.WaitForSelectorAsync($"mark:has-text(\"{assetTag}\")");
        string assetTagListed = await page.Locator("mark").InnerTextAsync();
        Assert.That(assetTagListed, Is.EqualTo(assetTag), "Asset Tag Not Found");
        await assetPage.GoToAssetDetails(assetTag);
        await page.WaitForSelectorAsync("span.hidden-xs.hidden-sm:has-text(\"History\")");
        string assetTagDetails = await assetPage.GetAssetDetails();
        Assert.That(assetTagDetails, Is.EqualTo(assetTag), "Assert Tag is not on Asset Details Page");
        var link= await page.WaitForSelectorAsync($"a:text(\"{modelName}\")");
        Assert.That(await link.IsVisibleAsync(), Is.True, "Assert Model Name is not on Asset Details Page");
        await assetPage.GotoAssetHistory();
        await assetPage.checkIfAssetDetailsAreInTable(assetTag, modelName);
    }

}