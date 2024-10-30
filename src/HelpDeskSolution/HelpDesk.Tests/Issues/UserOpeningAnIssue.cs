
using Alba;
using HelpDesk.Api.Issues;

namespace HelpDesk.Tests.Issues;
public class UserOpeningAnIssue : IAsyncLifetime
{
    private IAlbaHost host = null!;
    public async Task InitializeAsync()
    {
        host = await AlbaHost.For<Program>();
    }


    [Fact]
    [Trait("Category", "SystemTest")]
    [Trait("Feature", "Status")]
    public async Task ValidationsApplied()
    {

        var issueToPost = new IssueCreateModel { Description = "" };



        var response = await host.Scenario(api =>
        {

            api.Post.Json(issueToPost).ToUrl("/user/software/3e933fca-8191-4ebe-b064-45f4c1cb91da/issues");
            api.StatusCodeShouldBe(400);
        });
    }

    [Fact]
    public async Task UserCanOpenAnIssue()
    {

        var issueToPost = new IssueCreateModel { Description = "Won't Start" };



        var response = await host.Scenario(api =>
        {

            api.Post.Json(issueToPost).ToUrl("/user/software/3e933fca-8191-4ebe-b064-45f4c1cb91da/issues");
            api.StatusCodeShouldBeOk();
        });

        Assert.NotNull(response);

        var returnedBody = await response.ReadAsJsonAsync<IssueResponseModel>();

        Assert.NotNull(returnedBody);

        Assert.Equal(issueToPost.Description, returnedBody.Description);
        Assert.Equal(IssueStatus.Pending, IssueStatus.Pending);

        var lookupUrl = $"/user/software/3e933fca-8191-4ebe-b064-45f4c1cb91da/issues/{returnedBody.Id}";
        var lookupResponse = await host.Scenario(api =>
        {
            api.Get.Url(lookupUrl);
            api.StatusCodeShouldBeOk();
        });

        Assert.NotNull(lookupResponse);
        var lookupBody = await lookupResponse.ReadAsJsonAsync<IssueResponseModel>();

        Assert.NotNull(lookupBody);
        Assert.Equal(returnedBody, lookupBody);

    }


    public async Task DisposeAsync()
    {
        await host.DisposeAsync();
    }
}
