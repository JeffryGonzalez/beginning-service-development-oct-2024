
using Alba;
using HelpDesk.Api.Issues;

namespace HelpDesk.Tests.Issues;
public class UserOpeningAnIssue
{

    [Fact]
    public async Task UserCanOpenAnIssue()
    {
        var host = await AlbaHost.For<Program>();
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

    }
}
