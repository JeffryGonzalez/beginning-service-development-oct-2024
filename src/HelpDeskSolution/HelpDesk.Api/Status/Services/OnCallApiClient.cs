
namespace HelpDesk.Api.Status.Services;

// Typed Http Clients
public class OnCallApiClient : ILookupEmergencyContacts
{
    private HttpClient _client;

    public OnCallApiClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<EmergencyContactInfo> GetCurrentContactAsync()
    {
        var response = await _client.GetAsync("/on-call");

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<EmergencyContactInfo>();

        return body!;
    }
}
