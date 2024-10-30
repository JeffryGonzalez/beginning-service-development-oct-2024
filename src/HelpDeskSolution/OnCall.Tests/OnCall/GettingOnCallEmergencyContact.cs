
using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using OnCall.Api.HelpDesk;

namespace OnCall.Tests.OnCall;
public class GettingOnCallEmergencyContact
{

    [Fact]
    [Trait("Category", "SystemTest")]
    public async Task ReturnsCorrectResponseDuringBusinessHours()
    {
        using var host = await AlbaHost.For<Program>(options =>
        {
            options.ConfigureTestServices(services =>
            {
                var openClock = Substitute.For<IProvideTheBusinessClock>();
                openClock.CurrentlyOpen().Returns(true);
                services.AddScoped(sp => openClock);
            });
        });

        var expectedResponse = new EmergencyContactInfo()
        {
            Name = "Bob Smith",
            PhoneNumber = "555-1212",
            EmailAddress = "bob@company.com"
        };

        var response = await host.Scenario(api =>
         {
             api.Get.Url("/on-call");
             api.StatusCodeShouldBeOk();
         });

        var body = await response.ReadAsJsonAsync<EmergencyContactInfo>();

        Assert.NotNull(body);

        Assert.Equal(expectedResponse, body);
    }

    [Fact]
    [Trait("Category", "SystemTest")]
    public async Task ReturnsCorrectResponseOutsideBusinessHours()
    {
        using var host = await AlbaHost.For<Program>(options =>
        {
            options.ConfigureTestServices(services =>
            {
                var openClock = Substitute.For<IProvideTheBusinessClock>();
                openClock.CurrentlyOpen().Returns(false);
                services.AddScoped(sp => openClock);
            });
        });

        var expectedResponse = new EmergencyContactInfo()
        {
            Name = "Support Pros",
            PhoneNumber = "888 GET-HELP",
            EmailAddress = "help@fake-support.com"
        };

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/on-call");
            api.StatusCodeShouldBeOk();
        });

        var body = await response.ReadAsJsonAsync<EmergencyContactInfo>();

        Assert.NotNull(body);

        Assert.Equal(expectedResponse, body);
    }
}
