
using Alba;
using HelpDesk.Api.Status;

namespace HelpDesk.Tests.Status;
public class GettingTheStatus
{

    /*GET {{HelpDesk.Api_HostAddress}}/status */

    [Fact]
    [Trait("Category", "SystemTest")]
    [Trait("Feature", "Status")]
    public async Task CanGetTheStatus()
    {
        // Given
        var host = await AlbaHost.For<Program>(
            options =>
            {

            }

            ); // this "starts up your API"
        var expectedResponse = new StatusResponseModel
        {
            State = StatusState.Good,
            BacklogInfo = new Backlog
            {
                NumberOfIssuesInProcess = 99,
                NumberOfIssuesNotChecked = 22
            },
            EmergencyContact = new EmergencyContactInfo
            {
                Name = "Jeff",
                EmailAddress = "jeff@company.com",
                PhoneNumber = "555-1212"
            }
        };


        // When
        var actualResponse = await host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();
        });

        Assert.NotNull(actualResponse);

        var deserializedResponse = await actualResponse.ReadAsJsonAsync<StatusResponseModel>();

        Assert.Equal(expectedResponse, deserializedResponse);
    }
}

// this is unused - I am using a test double above with nsubstitute
public class DummySupportLookup : ILookupEmergencyContacts
{
    public async Task<EmergencyContactInfo> GetCurrentContactAsync()
    {
        return new EmergencyContactInfo
        {
            Name = "Jeff",
            EmailAddress = "jeff@company.com",
            PhoneNumber = "555-1212"
        };
    }
}