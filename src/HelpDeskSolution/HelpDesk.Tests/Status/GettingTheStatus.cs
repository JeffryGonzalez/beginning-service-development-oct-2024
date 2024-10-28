
using Alba;

namespace HelpDesk.Tests.Status;
public class GettingTheStatus
{
    /*GET {{HelpDesk.Api_HostAddress}}/status */

    [Fact]
    public async Task Works()
    {
        var host = await AlbaHost.For<Program>();

        await host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();
        });
    }
}
